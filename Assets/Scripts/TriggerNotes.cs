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
	public string frettedScaleDegree;
	public float timeLastPlayed;
	public GameObject muteRecordStopLight;
	private MuteRecordStateManager mrsm;
	public List<Note>[] noteArray;
	public TimeKeeper timeKeeper;
	

	public void payAttention(int tick){
		//print(tick);
		if ( mrsm.muted == false ) {
			foreach (Note note in noteArray[tick]) {
				//print(item);
				foreach (var entry in notesDict){
					if (entry.Value.isPlaying){
						entry.Value.Stop();
					}
				}
				note.audioSource.Play();
			}
		}
	}
	void Awake () {
		timeKeeper = GameObject.Find("TimeKeeper").GetComponent<TimeKeeper>();
		noteArray = new List<Note>[(12 * timeKeeper.beatsPerBar * timeKeeper.barsPerLoop)+1];

		for (int i = 0; i < noteArray.Length; i++){
			noteArray[i] = new List<Note>();
		}

		timeKeeper.eachTick += payAttention;
	}

	void Start () {
		frettedScaleDegree = "i";
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

		// p is for 'pop', as opposed to the regular bass strum
		notesDict.Add("ip", notesDict["BassSlapC4"]);
		notesDict.Add("iip", notesDict["BassSlapD4"]);
		notesDict.Add("iiip", notesDict["BassSlapE4"]);
		notesDict.Add("ivp", notesDict["BassSlapF4"]);
		notesDict.Add("vp", notesDict["BassSlapG4"]);
		notesDict.Add("vip", notesDict["BassSlapA4"]);
		notesDict.Add("viip", notesDict["BassSlapB4"]);
		notesDict.Add("viiip", notesDict["BassSlapC5"]);

		// capitals denote that the note  is raised an octave
		// capitals here have nothing to do with major/minor. confusing notation, perhaps
		notesDict.Add("Ip", notesDict["BassSlapC5"]);
		notesDict.Add("IIp", notesDict["BassSlapD5"]);
		notesDict.Add("IIIp", notesDict["BassSlapE5"]);
		notesDict.Add("IVp", notesDict["BassSlapF5"]);
		notesDict.Add("Vp", notesDict["BassSlapG5"]);
		notesDict.Add("VIp", notesDict["BassSlapA5"]);
		notesDict.Add("VIIp", notesDict["BassSlapB5"]);
		notesDict.Add("VIIIp", notesDict["BassSlapC6"]);

		notesDict.Add("i", notesDict["BassStrumC4"]);
		notesDict.Add("ii", notesDict["BassStrumD4"]);
		notesDict.Add("iii", notesDict["BassStrumE4"]);
		notesDict.Add("iv", notesDict["BassStrumF4"]);
		notesDict.Add("v", notesDict["BassStrumG4"]);
		notesDict.Add("vi", notesDict["BassStrumA4"]);
		notesDict.Add("vii", notesDict["BassStrumB4"]);
		notesDict.Add("viii", notesDict["BassStrumC5"]);

		// capitals denote that the note  is raised an octave
		notesDict.Add("I", notesDict["BassStrumC5"]);
		notesDict.Add("II", notesDict["BassStrumD5"]);
		notesDict.Add("III", notesDict["BassStrumE5"]);
		notesDict.Add("IV", notesDict["BassStrumF5"]);
		notesDict.Add("V", notesDict["BassStrumG5"]);
		notesDict.Add("VI", notesDict["BassStrumA5"]);
		notesDict.Add("VII", notesDict["BassStrumB5"]);
		notesDict.Add("VIII", notesDict["BassStrumC6"]);

	

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
			Hand hand = other.GetComponent<Hand>();
			//print("=-=-=-=-=-=");
			//print("fretted scale degree " + frettedScaleDegree);
			//print("timekeeper tickinloop " + timeKeeper.tickInLoop);
			string maybeP = "";
			if ( hand.currentHandState == "Idle" ) {
				maybeP = "";
			}
			else if ( hand.currentHandState == "Fist" ) {
				maybeP = "p";
			}
			// Monophonic instruments are hard to make, and possible not even desired?
			foreach (KeyValuePair<string, AudioSource> entry in notesDict ) {
				if ( entry.Value.isPlaying ) {
					entry.Value.Stop();
				}
			}
			


			notesDict[frettedScaleDegree + maybeP].Play();
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			timeLastPlayed = Time.time;
			if ( mrsm.recording == true ) {
				noteArray[timeKeeper.tickInLoop].Add(new Note("bass", timeKeeper.tickInLoop, notesDict[frettedScaleDegree + maybeP].clip.name, notesDict[frettedScaleDegree + maybeP], 1f));
			}
			//print(frettedScaleDegree);


			//print("hand!");
		}
	}
}
