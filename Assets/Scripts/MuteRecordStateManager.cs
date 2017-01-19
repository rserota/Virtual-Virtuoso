using UnityEngine;
using System.Collections;

public class MuteRecordStateManager : MonoBehaviour {
	public bool muted;
	public bool scheduledToMute;
	public bool recording;

	// Use this for initialization
	void Start () {
		muted = false;
		scheduledToMute = false;
		recording = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
