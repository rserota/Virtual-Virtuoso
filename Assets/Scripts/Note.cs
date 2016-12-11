using System;
using System.Collections.Generic;

public class Note
{
	public string instrument;
	public int triggerBeat;
	public string pitch;
	public Note (string instrument, int triggerBeat, string pitch){
		this.instrument = instrument;
		this.triggerBeat = triggerBeat;
		this.pitch = pitch;
	}
}

