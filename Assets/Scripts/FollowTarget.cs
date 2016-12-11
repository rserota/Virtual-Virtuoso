using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame


	void Update(){
		Vector3 newPos = target.transform.position;
		transform.position = newPos;
		//transform.localEulerAngles = new Vector3(0f, 0f, target.transform.localEulerAngles.z);
	}
}
