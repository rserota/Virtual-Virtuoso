using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeKeeper : MonoBehaviour {

	public List<Note>[] noteArray;
	public int bpm;
	private int beatLen;
	private int tickLen;
	private int currentTick;
	private int prevTick;
	public int tickInLoop {get; private set;}
	private int currentBeat;
	private int prevBeat;
	public int beatsPerBar;
	public int barsPerLoop;
	private int barInLoop;
	private int beatInLoop;
	private int beatInBar;
	public Text hudText;
	public AudioSource audioSource;

	private float startTime;
	void Awake () {
		noteArray = new List<Note>[(12 * beatsPerBar * barsPerLoop)+1];

		for (int i = 0; i < noteArray.Length; i++){
			noteArray[i] = new List<Note>();
		}
		beatLen = 60000 / bpm; // 60,000 milliseconds per minute
		tickLen = beatLen / 12;
		//print (beatLen);
		audioSource = GetComponent<AudioSource> ();

	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	void Update(){
		if (Input.GetKeyDown ("space")) {
			print (beatInLoop);
			audioSource.Play ();
			//print (noteArray);
			print("add a beat");
			noteArray[tickInLoop].Add(new Note ("hat", beatInLoop, "", audioSource));
			foreach (var item in noteArray){
				print(item.Count);
			}

		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		//print (Time.time * 1000);
		float timeElapsed = (Time.time + startTime) * 1000;

		prevTick    = currentTick;
		currentTick = ((int)timeElapsed / tickLen) -3 ;
		if (prevTick != currentTick) {
			//print("=-=-=-=-=-=-=-=-=-=-=");
			//print("current tick" + currentTick);
			//print(currentTick % (12 * beatsPerBar * barsPerLoop));
			if ( currentTick < 1 )  {
				currentBeat = 0;
				beatInLoop = 0;
				tickInLoop = 0;
			}
			else {
				tickInLoop = (currentTick % (12 * beatsPerBar * barsPerLoop)) + 1;
				currentBeat = ((currentTick-1) / 12) + 1;
				beatInLoop = ((currentBeat-1) % (beatsPerBar * barsPerLoop))+1;
			}
			//print("current beat" + currentBeat);

//			print (beatsPerBar);
//			print ((currentBeat % beatsPerBar) + 1);


			beatInBar = (currentBeat % beatsPerBar) + 1;
			barInLoop = ((beatInLoop-1) / beatsPerBar) + 1;
			//print ("beat in bar" + beatInBar);
			//print ("beat in loop" + beatInLoop);
			//print ( ((beatInLoop-1) / beatsPerBar) + 1);
			//print(noteArray[beatInLoop] == null);
			//print(noteArray[beatInLoop].Count);
			if ( tickInLoop > 0 ) {
				
			}
			foreach (Note item in noteArray[tickInLoop]) {
				//print(item);
				item.audioSource.Play();
			}
//			noteArray [beatInLoop].play ();
			//print(barInLoop);




			string maybeSpace;
			if (beatInBar < 10){ maybeSpace = " ";}
			else { maybeSpace = "";}
			hudText.text = barInLoop + " - " + maybeSpace + beatInBar;
		}



	}
}
