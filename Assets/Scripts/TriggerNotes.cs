using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerNotes : MonoBehaviour {
	public AudioSource bassSlap;
	public AudioSource[] audioClips;
	public Dictionary<string, AudioSource> notesDict;
	public Color baseColor;
	public Renderer meshRenderer;
	// it's not an absolute note, but a relative degree on any scale
	public string frettedScaleDegree = "i";
	public float timeLastPlayed;
	public GameObject muteRecordStopLight;
	private MuteRecordStateManager mrsm;
	public List<Note>[] noteArray;
	public TimeKeeper timeKeeper;
	void Awake () {
		timeKeeper = GameObject.Find("TimeKeeper").GetComponent<TimeKeeper>();
	}
	void Start () {
		//print(timeKeeper.noteArray);
		mrsm = muteRecordStopLight.GetComponent<MuteRecordStateManager>();
		meshRenderer = gameObject.GetComponent<Renderer>();

		audioClips = gameObject.GetComponents<AudioSource>();
		notesDict = new Dictionary<string, AudioSource>();
		//print(audioClips[0].clip);
		for (int i = 0; i < audioClips.Length; i++){
			//print(audioClips[i].clip.name);
	
			notesDict.Add(audioClips[i].clip.name, audioClips[i]);
		}

		notesDict.Add("i", notesDict["BassSlapC4"]);
		notesDict.Add("ii", notesDict["BassSlapD4"]);
		notesDict.Add("iii", notesDict["BassSlapE4"]);
		notesDict.Add("iv", notesDict["BassSlapF4"]);
		notesDict.Add("v", notesDict["BassSlapG4"]);
		notesDict.Add("vi", notesDict["BassSlapA4"]);
		notesDict.Add("vii", notesDict["BassSlapB4"]);
		notesDict.Add("viii", notesDict["BassSlapC5"]);
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
		if ( other.tag.Contains("hand") && mrsm.muted == false ) {
			print("=-=-=-=-=-=");
			print("fretted scale degree " + frettedScaleDegree);
			print("timekeeper tickinloop " + timeKeeper.tickInLoop);
			notesDict[frettedScaleDegree].Play();
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			timeLastPlayed = Time.time;
			timeKeeper.noteArray[timeKeeper.tickInLoop].Add(new Note("bass", timeKeeper.tickInLoop, notesDict[frettedScaleDegree].clip.name, notesDict[frettedScaleDegree]));
			//print(frettedScaleDegree);


			//print("hand!");
		}
	}
}
