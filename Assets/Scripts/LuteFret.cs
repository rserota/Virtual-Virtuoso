using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
public class LuteFret : MonoBehaviour {
	public TriggerLuteNotes[] triggerLuteNotes;

	public GameObject strumBoxI;
	public GameObject strumBoxIII;
	public GameObject strumBoxV;
	private Renderer[] strumBoxRenderers;
	private Renderer thisRenderer;
	public int scaleDegree;

	public Hand whichHandIsInMe;

	// Use this for initialization
	void Start () {
		//triggerLuteNotes = strumBox.GetComponents<TriggerLuteNotes>();
		strumBoxRenderers = new Renderer[3];
		strumBoxRenderers[0] = strumBoxI.GetComponent<Renderer>();
		strumBoxRenderers[1] = strumBoxIII.GetComponent<Renderer>();
		strumBoxRenderers[2] = strumBoxV.GetComponent<Renderer>();
		thisRenderer = gameObject.GetComponent<Renderer>();
		triggerLuteNotes = new TriggerLuteNotes[3];
		triggerLuteNotes[0] = strumBoxI.GetComponent<TriggerLuteNotes>();
		triggerLuteNotes[1] = strumBoxIII.GetComponent<TriggerLuteNotes>();
		triggerLuteNotes[2] = strumBoxV.GetComponent<TriggerLuteNotes>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		//print("hand?");
		if ( other.tag == "hand" ) {
			whichHandIsInMe = other.GetComponent<Hand>();
			whichHandIsInMe.device.TriggerHapticPulse();
			int modifiedScaleDegree = 0;
			if ( whichHandIsInMe.currentHandState == "Idle" ) {
				modifiedScaleDegree = scaleDegree;
			}
			else if ( whichHandIsInMe.currentHandState == "Fist" ) {
				modifiedScaleDegree = scaleDegree + 7;
			}
			//triggerNotes.frettedScaleDegree = modifiedScaleDegree;
			foreach ( TriggerLuteNotes luteString in triggerLuteNotes ) {
				luteString.frettedScaleDegree = modifiedScaleDegree + luteString.stringOffset;
				luteString.baseColor = thisRenderer.material.color;
				
				print(luteString.frettedScaleDegree);
			}
			foreach ( Renderer renderer in strumBoxRenderers ) {
				renderer.material.color = thisRenderer.material.color;
			}
			//print("hand!");
			//triggerNotes.baseColor = thisRenderer.material.color;
			//strumBoxRenderer.material.color = thisRenderer.material.color;
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
					//triggerNotes.frettedScaleDegree = scaleDegree.ToLower();
					//print("idle");
				}
				else if ( whichHandIsInMe.currentHandState == "Fist" ) {
					//triggerNotes.frettedScaleDegree = scaleDegree.ToUpper();
					//print("Fist");
				}
			}
			//print(scaleDegree);
		}
	}
}
