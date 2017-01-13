using System;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
	public string instrument;
	public int triggerBeat;
	public string pitch;
	public AudioSource audioSource;
	public Note (string instrument, int triggerBeat, string pitch, AudioSource audioSource){
		this.instrument = instrument;
		this.triggerBeat = triggerBeat;
		this.pitch = pitch;
		this.audioSource = audioSource;
	}
}

