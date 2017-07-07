using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerLuteNotes : MonoBehaviour {
	public AudioSource bassSlap;
	public AudioSource[] audioClips;
	public Dictionary<string, AudioSource> notesDict;
	public Color baseColor;
	public Renderer meshRenderer;
	// it's not an absolute note, but a relative degree on any scale
	public int frettedScaleDegree;

	public int stringOffset;
	public float timeLastPlayed;
	public GameObject muteRecordStopLight;

	public GameObject highlightCircle;
	private MuteRecordStateManager mrsm;
	public List<Note>[] noteArray;
	public TimeKeeper timeKeeper;
	

	public void payAttention(int tick){
		//print(tick);	
		if ( tick == 1 ) {

			if (mrsm.scheduledToMute == true){
				mrsm.muted = true;
			}
			else if (mrsm.scheduledToMute == false){
				mrsm.muted = false;
			}
		}

		if ( mrsm.muted == false ) {
			foreach (Note note in noteArray[tick]) {
				//print(item);
				foreach (var entry in notesDict){
					if (entry.Value.isPlaying){
						entry.Value.Stop();
					}
				}
				timeLastPlayed = Time.time;
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
		frettedScaleDegree = 0 + stringOffset;
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
		notesDict.Add(0.ToString(), notesDict["GuitarC3"]);
		notesDict.Add(1.ToString(), notesDict["GuitarD3"]);
		notesDict.Add(2.ToString(), notesDict["GuitarE3"]);
		notesDict.Add(3.ToString(), notesDict["GuitarF3"]);
		notesDict.Add(4.ToString(), notesDict["GuitarG3"]);
		notesDict.Add(5.ToString(), notesDict["GuitarA3"]);
		notesDict.Add(6.ToString(), notesDict["GuitarB3"]);
		notesDict.Add(7.ToString(), notesDict["GuitarC4"]);
		notesDict.Add(8.ToString(), notesDict["GuitarD4"]);
		notesDict.Add(9.ToString(), notesDict["GuitarE4"]);
		notesDict.Add(10.ToString(), notesDict["GuitarF4"]);
		notesDict.Add(11.ToString(), notesDict["GuitarG4"]);
		notesDict.Add(12.ToString(), notesDict["GuitarA4"]);
		notesDict.Add(13.ToString(), notesDict["GuitarB4"]);
		notesDict.Add(14.ToString(), notesDict["GuitarC5"]);
		notesDict.Add(15.ToString(), notesDict["GuitarD5"]);
		notesDict.Add(16.ToString(), notesDict["GuitarE5"]);
		notesDict.Add(17.ToString(), notesDict["GuitarF5"]);
		notesDict.Add(18.ToString(), notesDict["GuitarG5"]);



	

		timeLastPlayed = -1f;
		baseColor = meshRenderer.material.color;
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
			


			//notesDict[frettedScaleDegree + maybeP].Play();
			notesDict[frettedScaleDegree.ToString()].Play();

			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			timeLastPlayed = Time.time;
			if ( mrsm.recording == true ) {
				//noteArray[timeKeeper.tickInLoop].Add(new Note("lute", timeKeeper.tickInLoop, notesDict[frettedScaleDegree + maybeP].clip.name, notesDict[frettedScaleDegree + maybeP], 1f));
				noteArray[timeKeeper.tickInLoop].Add(new Note("lute", timeKeeper.tickInLoop, notesDict[frettedScaleDegree.ToString()].clip.name, notesDict[frettedScaleDegree.ToString()], 1f));
			}
			//print(frettedScaleDegree);


			//print("hand!");
		}
	}
	void OnDestroy(){
		//print("destroyed!");
		timeKeeper.eachTick -= payAttention;
	}
}
