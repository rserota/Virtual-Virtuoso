using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCrashNotes : MonoBehaviour {

	public AudioSource crashAudioClip;
	public Color baseColor;
	public Renderer meshRenderer;
	// it's not an absolute note, but a relative degree on any scale
	public float timeLastPlayed;
	public GameObject muteRecordStopLight;
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

				timeLastPlayed = Time.time;
				note.audioSource.Play();
			}
		}
	}
	// Use this for initialization
	void Awake () {
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

		crashAudioClip = gameObject.GetComponent<AudioSource>();

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
		print("hand?");
		if ( other.tag.Contains("hand") && mrsm.muted == false ) {

			crashAudioClip.Play();
			Hand hand = other.GetComponent<Hand>();
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			timeLastPlayed = Time.time;
			if ( mrsm.recording == true ) {
				noteArray[timeKeeper.tickInLoop].Add(new Note("crash", timeKeeper.tickInLoop, crashAudioClip.clip.name, crashAudioClip, 1f));
			}
			//print(frettedScaleDegree);


			//print("hand!");
		}
	}

	void OnDestroy(){
		print("destroyed!");
		timeKeeper.eachTick -= payAttention;
	}


}
