using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
public class Fret : MonoBehaviour {
	public Text hudText;
	private TriggerNotes triggerNotes;
	public GameObject strumBox;
	private Renderer strumBoxRenderer;
	private Renderer thisRenderer;
	public string scaleDegree;

	public Hand whichHandIsInMe;

	// Use this for initialization
	void Start () {
		hudText = GameObject.Find("timingText").GetComponent<Text>();
		triggerNotes = strumBox.GetComponent<TriggerNotes>();
		strumBoxRenderer = strumBox.GetComponent<Renderer>();
		thisRenderer = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		//print("hand?");
		if ( other.tag == "hand" ) {
			whichHandIsInMe = other.GetComponent<Hand>();
			whichHandIsInMe.device.TriggerHapticPulse();
			hudText.text = scaleDegree;
			string modifiedScaleDegree = "";
			if ( whichHandIsInMe.currentHandState == "Idle" ) {
				modifiedScaleDegree = scaleDegree;
			}
			else if ( whichHandIsInMe.currentHandState == "Fist" ) {
				modifiedScaleDegree = scaleDegree.ToUpper();
			}
			triggerNotes.frettedScaleDegree = modifiedScaleDegree;
			//print("hand!");
			triggerNotes.baseColor = thisRenderer.material.color;
			strumBoxRenderer.material.color = thisRenderer.material.color;
		}
	}

	void OnTriggerExit(Collider other) {
		if ( other.tag.Contains("hand") ) {
			whichHandIsInMe = null;

/* 
			triggerNotes.noteArray[triggerNotes.timeKeeper.tickInLoop].Add(new Note(
				"bass", 
				triggerNotes.timeKeeper.tickInLoop, 
				triggerNotes.notesDict[triggerNotes.frettedScaleDegree + maybeP].clip.name, 
				triggerNotes.notesDict[triggerNotes.frettedScaleDegree + maybeP], 
				0f
			));
*/
		}
	}

	void OnTriggerStay(Collider other) {
		if ( other.tag.Contains("hand") ) {
			if ( whichHandIsInMe != null ) {
				//print("which hand is not null");
				if ( whichHandIsInMe.currentHandState == "Idle" ) {
					triggerNotes.frettedScaleDegree = scaleDegree.ToLower();
					//print("idle");
				}
				else if ( whichHandIsInMe.currentHandState == "Fist" ) {
					triggerNotes.frettedScaleDegree = scaleDegree.ToUpper();
					//print("Fist");
				}
			}
			//print(scaleDegree);
		}
	}
}
