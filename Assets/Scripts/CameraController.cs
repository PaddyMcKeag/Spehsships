using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject objectToTrack;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - objectToTrack.transform.position;
	}
	
	// Runs after all code in 'Update' has been executed
	void LateUpdate () {
		positionCameraAtTrackedObject();
	}

	private void positionCameraAtTrackedObject(){
		transform.position = objectToTrack.transform.position + offset;
	}
}
