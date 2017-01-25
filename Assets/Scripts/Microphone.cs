using UnityEngine;
using System.Collections;

public class Microphone : MonoBehaviour {
	private AudioSource mic;
	// Use this for initialization
	void Start () {
		mic = GetComponent<AudioSource>();
		string micString = UnityEngine.Microphone.devices[0];
		mic.clip = UnityEngine.Microphone.Start(micString, true, 10, 44100);
		while (!(UnityEngine.Microphone.GetPosition(micString) > 0)){
			
		}
		mic.Play();
		print(UnityEngine.Microphone.devices[0]);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
