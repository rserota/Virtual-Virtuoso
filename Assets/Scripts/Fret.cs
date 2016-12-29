using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
public class Fret : MonoBehaviour {
	public Text hudText;
	public string pitch;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		print("hand?");
		if ( other.tag == "hand" ) {
			other.gameObject.GetComponent<Hand>().device.TriggerHapticPulse();
			hudText.text = pitch;
			print("hand!");
		}
	}
}
