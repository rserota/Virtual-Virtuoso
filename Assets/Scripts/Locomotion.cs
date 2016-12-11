using UnityEngine;
using System.Collections;

public class Locomotion : MonoBehaviour {
	private GameObject cameraRig;
	// Use this for initialization
	void Start () {
		cameraRig = GameObject.Find("[CameraRig]");
		print(cameraRig);
	
	}
	
	// Update is called once per frame
	void Update () {
		// print(transform.position - cameraRig.transform.position);
		Vector3 newVelocity = transform.position - cameraRig.transform.position;
		newVelocity.y = 0;
		//print(newVelocity);
		Rigidbody rigidBody = cameraRig.GetComponent<Rigidbody>();
		rigidBody.velocity = newVelocity;
	}
}
