using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float rotationSpeed;

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
		float playerRotation = calculateHorizontalRotation();
		player.AddTorque(playerRotation);
	}

	/* 
		Calculates the horizontal rotation of the player. Note that this flips the horizontal input so 
	  	that the player is rotated based on the key that they press, as Unity uses the "Left-Hand Coordinate System".
	*/
	private float calculateHorizontalRotation(){
		return -Input.GetAxis("Horizontal") * rotationSpeed;
	}

	private void applyPlayerMovement(){
		float verticalMovement = Input.GetAxis("Vertical");
		Vector2 playerMovement = new Vector2(0, verticalMovement);
		player.AddRelativeForce(playerMovement * movementSpeed);
	}
	
}
