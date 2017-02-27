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
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			hudText.text = scaleDegree;
			triggerNotes.frettedScaleDegree = scaleDegree;
			//print("hand!");
			triggerNotes.baseColor = thisRenderer.material.color;
			strumBoxRenderer.material.color = thisRenderer.material.color;
		}
	}
}
