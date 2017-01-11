﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeKeeper : MonoBehaviour {

	public static Note[] noteArray;
	public int bpm;
	private int beatLen;
	private int tickLen;
	private int currentTick;
	private int prevTick;
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
		noteArray = new Note[(beatsPerBar * barsPerLoop)+1];
		beatLen = 60000 / bpm; // 60,000 milliseconds per minute
		tickLen = beatLen / 12;
		print (beatLen);
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
			print (noteArray);

			noteArray[beatInLoop] = new Note ("hat", beatInLoop, "");

		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		//print (Time.time * 1000);
		float timeElapsed = (Time.time + startTime) * 1000;

		prevTick    = currentTick;
		currentTick = ((int)timeElapsed / tickLen) -3 ;
		if (prevTick != currentTick) {
			//print("current tick" + currentTick);
			if ( currentTick < 1 )  {
				currentBeat = 0;
			}
			else {
				currentBeat = ((currentTick-1) / 12) + 1;
			}
			//print("current beat" + currentBeat);
			print("=-=-=-=-=-=-=-=-=-=-=");
//			print (beatsPerBar);
//			print ((currentBeat % beatsPerBar) + 1);

			beatInLoop = (currentBeat % (beatsPerBar * barsPerLoop)) + 1;
			beatInBar = (currentBeat % beatsPerBar) + 1;
			barInLoop = ((beatInLoop-1) / beatsPerBar) + 1;
			print ("beat in bar" + beatInBar);
			//print ("beat in loop" + beatInLoop);
			//print ( ((beatInLoop-1) / beatsPerBar) + 1);
			//print(noteArray[beatInLoop] == null);
			if (noteArray [beatInLoop] != null) {
				audioSource.Play ();
			}
			//print ("=-=-=-=-=-=-=");
//			noteArray [beatInLoop].play ();
			print(barInLoop);
			string maybeSpace;
			if (beatInBar < 10){ maybeSpace = " ";}
			else { maybeSpace = "";}
			hudText.text = barInLoop + " - " + maybeSpace + beatInBar;
		}


	}
}
