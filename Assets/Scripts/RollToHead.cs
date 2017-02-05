using UnityEngine;
using System.Collections;

public class RollToHead : MonoBehaviour {

	public GameObject head;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forceVector = head.transform.position - transform.position;
		//print(newVelocity);
		rb.AddForce(forceVector);
	}

}
