using UnityEngine;
using System.Collections;


public class TriggerNotes : MonoBehaviour {
	public AudioSource bassSlap;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		print("hand?");
		if ( other.tag == "hand" ) {
			bassSlap.Play();
			print("hand!");
		}
	}
}
