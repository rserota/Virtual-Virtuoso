using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darken : MonoBehaviour {
	public TextMesh textMesh;
	// Use this for initialization
	void Awake () {
		textMesh = GetComponent<TextMesh>();		
		print(textMesh);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
		print("word");
		textMesh.color = new Color(.1f,.1f,.1f,1f);
	}
	void OnTriggerExit (Collider other){
		textMesh.color = new Color(1f,1f,1f,1f);
	}
}
