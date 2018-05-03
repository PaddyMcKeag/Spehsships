using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
	//used to calibrate the rotation, such that the top of the sprite (the barrel of the gun) is the point that aims at the mouse, by default it's the side of the sprite
	public float turretSpriteOffset = -90.0f;
	//the parent ship of this turret
	public GameObject ship;
	//premade bullet prefab ready to spawn
	public GameObject bulletPrefab;
	//location for bullet spawn, public so we can adjust for different turrets
	public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		aimTurret ();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			fireTurret();
		}
	}

	void aimTurret () {
		float rotationZ = calculateAimAngle ();
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + turretSpriteOffset);
	}

	/*
		Calculates the rotation for the turret's aim. 
		The turret should aim along a line parallel with the line between the centre of the ship and the mouse, rather than at the actual mouse; this allows the bullets of all of the ships turrets to be fired in parallel with each other.
		This method takes the parent ship's position, compares it with the mouse position and normalises it so that the rotation vector is simply a direction with no magnitude.
		The vector is converted into an angle to allow a rotation in degrees.
	*/
	float calculateAimAngle (){
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ship.transform.position;
		difference.Normalize();
		return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
	}

	void fireTurret()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position, 
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 100));

		// Destroy the bullet after 4 seconds
		Destroy(bullet, 4.0f);
	}
}
