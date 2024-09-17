using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float enginePower;
	public float maxVelocity;
	public float rotationSpeed;

	public GameObject mainEngine;
	public GameObject reverseEngines;
	public GameObject clockwiseEngines;
	public GameObject antiClockwiseEngines;


	private Rigidbody2D player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Use for physics calculations
	void FixedUpdate(){
		applyPlayerRotation();
		applyPlayerMovement();
	}

	private void applyPlayerRotation(){
		float playerRotationalForce = calculateRotationalForce();
		showRotationalEngines(playerRotationalForce);
		player.AddTorque(playerRotationalForce);
	}

	/* 
		Calculates the rotational force of the player. Note that this flips the horizontal input so 
	  	that the player is rotated based on the key that they press, as Unity uses the "Left-Hand Coordinate System".
	*/
	private float calculateRotationalForce(){
		float rotationalForce = -Input.GetAxis("Horizontal") * rotationSpeed;
		return rotationalForce;
	}

	private void showRotationalEngines(float rotationalForce){
		if (rotationalForce < 0) {
			clockwiseEngines.SetActive (true);
			antiClockwiseEngines.SetActive (false);
		} else if (rotationalForce > 0) {
			clockwiseEngines.SetActive (false);
			antiClockwiseEngines.SetActive (true);
		} else {
			clockwiseEngines.SetActive (false);
			antiClockwiseEngines.SetActive (false);
		}
	}

	private void showLinearEngines(float verticalForce){
		if (verticalForce > 0) {
			mainEngine.SetActive (true);
			reverseEngines.SetActive (false);
		} else if (verticalForce < 0) {
			mainEngine.SetActive (false);
			reverseEngines.SetActive (true);
		} else {
			mainEngine.SetActive (false);
			reverseEngines.SetActive (false);
		}
	}

	private void applyPlayerMovement(){
		float verticalForce = Input.GetAxis("Vertical");
		showLinearEngines(verticalForce);
		Vector2 playerForce = new Vector2(0, verticalForce);
		player.AddRelativeForce(playerForce * enginePower);
		player.velocity = Vector2.ClampMagnitude(player.velocity, maxVelocity);
	}
	
}
