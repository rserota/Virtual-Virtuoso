using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerNotes : MonoBehaviour {
	public AudioSource bassSlap;
	public AudioSource[] audioClips;
	public Dictionary<string, AudioSource> notesDict;
	public Dictionary<string, AudioSource> activeScaleDict;
	public Color baseColor;
	public Renderer meshRenderer;
	// it's not an absolute note, but a relative degree on any scale
	public string frettedScaleDegree = "i";
	public float timeLastPlayed;
	public TimeKeeper timeKeeper;

	void Start () {
		meshRenderer = gameObject.GetComponent<Renderer>();

		audioClips = gameObject.GetComponents<AudioSource>();
		notesDict = new Dictionary<string, AudioSource>();
		//print(audioClips[0].clip);
		for (int i = 0; i < audioClips.Length; i++){
			//print(audioClips[i].clip.name);
	
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

		timeLastPlayed = -1f;
		//print(audioClips[0].time);
		baseColor = new Color(.8f, .8f, .8f);
	}
	
	// Update is called once per frame
	void Update () {
		if ( Time.time - timeLastPlayed < .55 ) {
			meshRenderer.material.color = Color.Lerp(Color.black, baseColor, (Time.time - timeLastPlayed) / .4f);
		}
	}

	void OnTriggerEnter(Collider other) {
		//print("hand?");
		if ( other.tag.Contains("hand") ) {
			activeScaleDict[frettedScaleDegree].Play();
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			timeLastPlayed = Time.time;
			//timeKeeper.
			//meshRenderer.material.color = Color.cyan;

			//print(frettedScaleDegree);


			//print("hand!");
		}
	}
}
