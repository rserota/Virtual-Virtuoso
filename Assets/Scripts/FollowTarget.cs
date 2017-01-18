using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public GameObject target;
	public float hoverHeight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame


	void Update(){
		Vector3 newPos = target.transform.position;
		newPos.y += hoverHeight;
		transform.position = newPos;
		//transform.localEulerAngles = new Vector3(0f, 0f, target.transform.localEulerAngles.z);
	}
}
