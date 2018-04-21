using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
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
		player.AddTorque(playerRotationalForce);
	}

	/* 
		Calculates the rotational force of the player. Note that this flips the horizontal input so 
	  	that the player is rotated based on the key that they press, as Unity uses the "Left-Hand Coordinate System".
	*/
	private float calculateRotationalForce(){
		float rotationalForce = -Input.GetAxis("Horizontal") * rotationSpeed;
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
		return rotationalForce;
	}

	private void applyPlayerMovement(){
		float verticalForce = Input.GetAxis("Vertical");
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
		Vector2 playerForce = new Vector2(0, verticalForce);
		player.AddRelativeForce(playerForce * movementSpeed);
	}
	
}
