using UnityEngine;
using System.Collections;


public class TriggerNotes : MonoBehaviour {
	public AudioSource bassSlap;
	public string FrettedPitch;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		print("hand?");
		if ( other.tag == "hand" ) {
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			bassSlap.Play();
			print("hand!");
		}
	}
}
