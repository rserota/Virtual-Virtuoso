using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPianoNotes : MonoBehaviour {

	public AudioSource pianoAudioClip;
	public Color baseColor;
	public Renderer meshRenderer;
	// it's not an absolute note, but a relative degree on any scale
	public float timeLastPlayed;
	public GameObject muteRecordStopLight;
	private MuteRecordStateManager mrsm;
	public List<Note>[] noteArray;

	public Dictionary<string, AudioSource> notesDict;
	private bool sustained;
	private float timeLastReleased;
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
				if ( note.velocity == 0f ) {
					sustained = false;
					timeLastReleased = Time.time;
				}
				else {
					sustained = true;
					pianoAudioClip.volume = 1f;
					note.audioSource.Play();
				}
			}
		}
	}
	// Use this for initialization
	void Awake () {
		sustained = false;
		timeKeeper = GameObject.Find("TimeKeeper").GetComponent<TimeKeeper>();
		noteArray = new List<Note>[(12 * timeKeeper.beatsPerBar * timeKeeper.barsPerLoop)+1];

		for (int i = 0; i < noteArray.Length; i++){
			noteArray[i] = new List<Note>();
		}

		timeKeeper.eachTick += payAttention;
	
	}
	

	void Start () {
		mrsm = muteRecordStopLight.GetComponent<MuteRecordStateManager>();
		meshRenderer = gameObject.GetComponent<Renderer>();

		pianoAudioClip = gameObject.GetComponent<AudioSource>();

		timeLastPlayed = -1f;
		baseColor = meshRenderer.material.color;

	}

	// Update is called once per frame
	void Update () {
		if ( sustained == false && Time.time - timeLastReleased < .55 ) {
			meshRenderer.material.color = Color.Lerp(Color.black, baseColor, (Time.time - timeLastReleased) / .4f);
			
		}
		if ( sustained == false && pianoAudioClip.volume > 0f ) {
			if ( pianoAudioClip.volume > .001f ) {
				pianoAudioClip.volume = Mathf.Lerp(1f, 0f, (Time.time-timeLastReleased) / .4f);
			}
			else if ( pianoAudioClip.volume <= .001f ) {
				pianoAudioClip.volume = 0f;
			}
		}
	}

	void OnTriggerEnter(Collider other) {

		if ( other.tag.Contains("hand") && mrsm.muted == false ) {
			sustained = true;
			pianoAudioClip.volume = 1f;
			pianoAudioClip.Play();
			Hand hand = other.GetComponent<Hand>();
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			meshRenderer.material.color = Color.black;
			if ( mrsm.recording == true ) {
				noteArray[timeKeeper.tickInLoop].Add(new Note("piano", timeKeeper.tickInLoop, pianoAudioClip.clip.name, pianoAudioClip, 1f));
			}
			//print(frettedScaleDegree);


			//print("hand!");
		}
	}

	void OnTriggerExit(Collider other) {
		if ( other.tag.Contains("hand") && mrsm.muted == false ) {
			sustained = false;
			//pianoAudioClip.Stop();
			timeLastReleased = Time.time;
			if (mrsm.recording == true ) {
				noteArray[timeKeeper.tickInLoop].Add(new Note("piano", timeKeeper.tickInLoop, pianoAudioClip.clip.name, pianoAudioClip, 0f));
			}
		}
	}
	void OnDestroy(){
		//print("destroyed!");
		timeKeeper.eachTick -= payAttention;
	}


}
