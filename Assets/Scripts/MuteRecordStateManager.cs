using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MuteRecordStateManager : MonoBehaviour {
	public bool muted;
	public bool scheduledToMute;

	private bool _recording;
	public bool recording {
		get { return _recording; }
		set {
			_recording = value;

			if ( _recording == true ) {
				lights[2].GetComponent<MeshRenderer>().material.color = Color.red;
			}
			else {
				lights[2].GetComponent<MeshRenderer>().material.color = new Color(1f,1f,1f,.6f);
			}
		}
	}
	public List<GameObject> lights;

	// Use this for initialization
	void Start () {

		foreach ( Transform child in gameObject.transform ) {
			lights.Add(child.gameObject);
		}
		print("lights? " + lights.Count);
		lights[0].GetComponent<MeshRenderer>().material.color = Color.green;
		lights[1].GetComponent<MeshRenderer>().material.color = Color.yellow;
		lights[2].GetComponent<MeshRenderer>().material.color = Color.red;

		muted = false;
		scheduledToMute = false;
		recording = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
