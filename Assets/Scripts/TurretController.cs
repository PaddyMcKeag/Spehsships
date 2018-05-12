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


	//initial force applied to bullets of mass 1
	public float bulletVelocity;

	//controls how long the bullet lasts
	public float bulletLifetime;

	//controls how far out the bullet spawns
	public float barrelLength;

	//fire rate of the turret, (gap between shots in seconds
	public float fireRate;
	//variable used to store the time when the next shot is allowed
	private float nextFire = 0.0F;

	//location for bullet spawn, public so we can adjust for different turrets
	private Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		bulletSpawn = this.transform;
	}
	
	// Update is called once per frame
	//Time.time is time in seconds since the start of the game, nextFire is time at which next shot is allowed, fireRate is the gap between shots
	void Update () {

		//aim
		Vector3 aimAngle = calculateAimAngle ();
		aimTurret (aimAngle);

		//fire
		if (Input.GetMouseButtonDown(0) && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			fireTurret(aimAngle);
		}
	}

	void aimTurret (Vector3 aimAngle) {
		float rotationZ =Mathf.Atan2(aimAngle.y, aimAngle.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + turretSpriteOffset);
	}

	/*
		Calculates the rotation for the turret's aim. 
		The turret should aim along a line parallel with the line between the centre of the ship and the mouse, rather than at the actual mouse; this allows the bullets of all of the ships turrets to be fired in parallel with each other.
		This method takes the parent ship's position, compares it with the mouse position and normalises it so that the rotation vector is simply a direction with no magnitude.
		The vector is converted into an angle to allow a rotation in degrees.
	*/
	Vector3 calculateAimAngle (){
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ship.transform.position;
		difference.Normalize();
		return difference;
	}

	void fireTurret(Vector3 aimAngle){
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position + aimAngle*barrelLength, 
			bulletSpawn.rotation);

		// Add initial velocity to the bullet
		bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, bulletVelocity));
		// Add velocity of ship
		bullet.GetComponent<Rigidbody2D> ().velocity += ship.GetComponent<Rigidbody2D> ().velocity;

		// Destroy the bullet after 4 seconds
		Destroy(bullet, bulletLifetime);
	}
}
