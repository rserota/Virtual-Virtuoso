using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MuteRecordStateManager : MonoBehaviour {
	private bool _muted;
	public bool muted {
		get { return _muted; }
		set {
			_muted = value;

			if ( _muted == true ) {
				lights[2].GetComponent<MeshRenderer>().material.color = new Color(1f,1f,1f,.6f);
				recording = false;
				scheduledToMute = true;
			}
			else { 
				lights[2].GetComponent<MeshRenderer>().material.color = Color.green;
				scheduledToMute = false;
			}
		}
	}

	private bool _scheduledToMute;
	public bool scheduledToMute {
		get { return _scheduledToMute; }
		set {
			_scheduledToMute = value;

			if ( _scheduledToMute == true ) {
				lights[1].GetComponent<MeshRenderer>().material.color = Color.yellow;
			}
			else {
				lights[1].GetComponent<MeshRenderer>().material.color = new Color(1f,1f,1f,.6f);
			}
		}
	}

	private bool _recording;
	public bool recording {
		get { return _recording; }
		set {
			_recording = value;

			if ( _recording == true ) {
				lights[0].GetComponent<MeshRenderer>().material.color = Color.red;
				muted = false;
			}
			else {
				lights[0].GetComponent<MeshRenderer>().material.color = new Color(1f,1f,1f,.6f);
			}
		}
	}
	public List<GameObject> lights;

	// Use this for initialization
	void Start () {

		foreach ( Transform child in gameObject.transform ) {
			lights.Add(child.gameObject);
		}
		//print("lights? " + lights.Count);
		lights[2].GetComponent<MeshRenderer>().material.color = Color.green;
		lights[1].GetComponent<MeshRenderer>().material.color = Color.yellow;
		lights[0].GetComponent<MeshRenderer>().material.color = Color.red;

		muted = false;
		scheduledToMute = false;
		recording = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
