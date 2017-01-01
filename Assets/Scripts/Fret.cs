using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
public class Fret : MonoBehaviour {
	public Text hudText;
	private TriggerNotes triggerNotes;
	public GameObject strumBox;
	public string scaleDegree;
	// Use this for initialization
	void Start () {
		triggerNotes = strumBox.GetComponent<TriggerNotes>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		print("hand?");
		if ( other.tag == "hand" ) {
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			hudText.text = scaleDegree;
			triggerNotes.frettedScaleDegree = scaleDegree;
			//print("hand!");
		}
	}
}
