using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {
	public MeshRenderer renderer;

	// Use this for initialization

	void Awake () {
		renderer =  gameObject.GetComponent<MeshRenderer>();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
