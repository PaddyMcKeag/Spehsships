using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
	//used to calibrate the rotation, such that the top of the sprite (the barrel of the gun) is the point that aims at the mouse, by default it's the side of the sprite
	public float turretSpriteOffset = -90.0f;
	//the parent ship of this turret
	public GameObject ship;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		aimTurrets ();
	}

	void aimTurrets () {
		float rotationZ = getAimAngle ();
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + turretSpriteOffset);
	}

	float getAimAngle (){
		/*Gets the rotation for the turrets to aim, we dont actually want the turrets to aim at the mouse, we want them to aim along a line
		parallel with the line between the centre of the ship and the mouse, so that the bullets all fire parallel, so we take the parent ship
		position to compare with the mouse position, we then normalise so that our rotation vector is simply a direction with no magnitude */
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ship.transform.position;
		difference.Normalize();
		//turn our direction vector into an angle to rotate in degrees
		return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
	}
}
