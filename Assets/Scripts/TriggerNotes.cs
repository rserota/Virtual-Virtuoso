using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerNotes : MonoBehaviour {
	public AudioSource bassSlap;
	public AudioSource[] audioClips;
	public Dictionary<string, AudioSource> notesDict;
	public Dictionary<string, AudioSource> activeScaleDict;

	// it's not an absolute note, but a relative degree on any scale
	public string frettedScaleDegree = "i";
	// Use this for initialization
	void Start () {
		audioClips = gameObject.GetComponents<AudioSource>();
		notesDict = new Dictionary<string, AudioSource>();
		print(audioClips[0].clip);
		print("HI");
		for (int i = 0; i < audioClips.Length; i++){
			print(audioClips[i].clip.name);
	
			notesDict.Add(audioClips[i].clip.name, audioClips[i]);
		}

		activeScaleDict = new Dictionary<string, AudioSource>();
		activeScaleDict.Add("i", notesDict["BassSlapC4"]);
		activeScaleDict.Add("ii", notesDict["BassSlapD4"]);
		activeScaleDict.Add("iii", notesDict["BassSlapE4"]);
		activeScaleDict.Add("iv", notesDict["BassSlapF4"]);
		activeScaleDict.Add("v", notesDict["BassSlapG4"]);
		activeScaleDict.Add("vi", notesDict["BassSlapA4"]);
		activeScaleDict.Add("vii", notesDict["BassSlapB4"]);
		activeScaleDict.Add("viii", notesDict["BassSlapC5"]);


		print(audioClips[0].time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		print("hand?");
		if ( other.tag == "hand" ) {
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			print(frettedScaleDegree);

			activeScaleDict[frettedScaleDegree].Play();
			//print("hand!");
		}
	}
}
