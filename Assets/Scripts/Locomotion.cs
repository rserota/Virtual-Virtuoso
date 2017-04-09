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
		string speedString = "woosh!";
		if ( Mathf.Abs(newVelocity.z) > .35f ) {
			if (newVelocity.z > .35f){
				newVelocity.z -= .35f;
			}
			else if (newVelocity.z < -.35f) {
				newVelocity.z += .35f;
			}
		}
		else {
			newVelocity.z = 0f;
		}
		if ( Mathf.Abs(newVelocity.x) > .35f ) {
			if (newVelocity.x > .35f){
				newVelocity.x -= .35f;
			}
			else if (newVelocity.x < -.35f) {
				newVelocity.x += .35f;
			}
		}
		else {
			newVelocity.x = 0f;
		}

		//print(newVelocity);
		print(newVelocity.magnitude);
		print(Vector3.ClampMagnitude(newVelocity,1f).magnitude);
		print("=-=-=-=-=-=-=");
		rigidBody.velocity = newVelocity;
		locomotionText.text = newVelocity.ToString();
	}
}
