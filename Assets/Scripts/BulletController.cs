using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {


	//explosion on hit
	//premade bullet prefab ready to spawn
	public GameObject boomPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D col) {
		Destroy (this.gameObject);
		explode ();
	}

	void explode(){
		var explosion = (GameObject)Instantiate (
			boomPrefab,
			this.transform.position,
			this.transform.rotation);
		Destroy(explosion, 0.1f);
	}
		
}
