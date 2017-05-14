using UnityEngine;
using System.Collections;

public class Locomotion : MonoBehaviour {
	private GameObject cameraRig;
	private Rigidbody rigidBody;
	// Use this for initialization

	public UnityEngine.UI.Text locomotionText;
	void Start () {
		cameraRig = GameObject.Find("[CameraRig]");
		print(cameraRig);
		rigidBody = cameraRig.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		// print(transform.position - cameraRig.transform.position);
		Vector3 newVelocity = (transform.position - cameraRig.transform.position) * 2f;
		newVelocity.y = 0;
		float deadZoneSize = .4f;
		string speedString = "woosh!";
		if ( Mathf.Abs(newVelocity.z) > deadZoneSize ) {
			if (newVelocity.z > deadZoneSize){
				newVelocity.z -= deadZoneSize;
			}
			else if (newVelocity.z < -deadZoneSize) {
				newVelocity.z += deadZoneSize;
			}
		}
		else {
			newVelocity.z = 0f;
		}
		if ( Mathf.Abs(newVelocity.x) > deadZoneSize) {
			if (newVelocity.x > deadZoneSize){
				newVelocity.x -= deadZoneSize;
			}
			else if (newVelocity.x < -deadZoneSize) {
				newVelocity.x += deadZoneSize;
			}
		}
		else {
			newVelocity.x = 0f;
		}

		//print(newVelocity);
		//print(newVelocity.magnitude);
		//print(Vector3.ClampMagnitude(newVelocity,1f).magnitude);
		//print("=-=-=-=-=-=-=");
		rigidBody.velocity = newVelocity;
		locomotionText.text = newVelocity.ToString();
	}
}
