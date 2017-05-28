using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMetronomeNotes : MonoBehaviour {

	public AudioSource[] audioClips;
	public Color baseColor;
	public Renderer meshRenderer;
	// it's not an absolute note, but a relative degree on any scale
	public float timeLastPlayed;
	public GameObject muteRecordStopLight;
	private MuteRecordStateManager mrsm;
	public List<Note>[] noteArray;

	public Dictionary<string, AudioSource> notesDict;
	public TimeKeeper timeKeeper;

	private int currentBeat;
	private int prevBeat;
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
			print(timeKeeper.prevBeat);
			print(timeKeeper.currentBeat);
			print(timeKeeper.prevBeat != timeKeeper.currentBeat);
			if ( timeKeeper.prevBeat != timeKeeper.currentBeat ) {
				notesDict["hatClosed"].Play();
				timeLastPlayed = Time.time;
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

		audioClips = gameObject.GetComponents<AudioSource>();

		notesDict = new Dictionary<string, AudioSource>();
		//print(audioClips[0].clip);
		for (int i = 0; i < audioClips.Length; i++){
			//print(audioClips[i].clip.name);
	
			notesDict.Add(audioClips[i].clip.name, audioClips[i]);
		}
	

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
	}
	void OnDestroy(){
		print("destroyed!");
		timeKeeper.eachTick -= payAttention;
	}


}
