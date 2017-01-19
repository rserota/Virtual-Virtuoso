using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
	public SteamVR_TrackedObject wand;
	public SteamVR_Controller.Device device;

	private Transform handModel;
	private Animator anima;
	public Text hudText;

	private SteamVR_LaserPointer laserPointer;
	public string whichHandIsThis;
	private string prevHandState;
	public string currentHandState;

	public GameObject heldObject;


	int Idle = Animator.StringToHash("Idle");
	int Point = Animator.StringToHash("Point");
	int GrabLarge = Animator.StringToHash("GrabLarge");
	int GrabSmall = Animator.StringToHash("GrabSmall");
	int GrabStickUp = Animator.StringToHash("GrabStickUp");
	int GrabStickFront = Animator.StringToHash("GrabStickFront");
	int ThumbUp = Animator.StringToHash("ThumbUp");
	int Fist = Animator.StringToHash("Fist");
	int Gun = Animator.StringToHash("Gun");
	int GunShoot = Animator.StringToHash("GunShoot");
	int PushButton = Animator.StringToHash("PushButton");
	int Spread = Animator.StringToHash("Spread");
	int MiddleFinger = Animator.StringToHash("MiddleFinger");
	int Peace = Animator.StringToHash("Peace");
	int OK = Animator.StringToHash("OK");
	int Phone = Animator.StringToHash("Phone");
	int Rock = Animator.StringToHash("Rock");
	int Natural = Animator.StringToHash("Natural");
	int Number3 = Animator.StringToHash("Number3");
	int Number4 = Animator.StringToHash("Number4");

	// Use this for initialization
	void Awake () {
		wand = GetComponent<SteamVR_TrackedObject>();

		if (transform.Find("vr_cartoon_hand_prefab_right")){
			handModel = transform.Find("vr_cartoon_hand_prefab_right");
			whichHandIsThis = "right";	
		}
		else if (transform.Find("vr_cartoon_hand_prefab_left")){
			handModel = transform.Find("vr_cartoon_hand_prefab_left");
			whichHandIsThis = "left";
		}

		anima = handModel.GetComponent<Animator>();

		laserPointer = GetComponent<SteamVR_LaserPointer>();
	}
	
	void Start () {
		//print((int)wand.index);
		device = SteamVR_Controller.Input((int)wand.index);

			
	}

		// Update is called once per frame
	void Update () {
		//print(transform.position);
		device = SteamVR_Controller.Input((int)wand.index);
		//print(device.GetHairTrigger());
		//print(device.velocity);
		SetHandState();
		AnimateHandStateChange();
		StateChangeOneShotEffects();
		if (device.GetHairTriggerUp()){
			DropHeldObject();
		}



	}

	
	
	void OnTriggerStay(Collider other){
		//print("Stay!");
		if (device.GetHairTriggerDown()){
			PickUpObject(other);
		}
	}

	void OnTriggerEnter(Collider other){
		if ( other.tag.Contains("pulse")){
			device.TriggerHapticPulse();
		}	
	}

	void SetHandState () {
		prevHandState = currentHandState;
		if ( currentHandState == "Laser" ) {
			if ( device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu) ){
				currentHandState = "Idle";
			}
		}
		else {
			
			if ( device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu) ){
				currentHandState = "Laser";
			}
			else if (device.GetHairTrigger()){
				currentHandState = "Fist";

			}
			else {
				currentHandState = "Idle";
			}
		}

	}

	void AnimateHandStateChange () {
		if (currentHandState != prevHandState){
			if (currentHandState == "Idle"){
				anima.SetTrigger(Idle);
			}
			if (currentHandState == "Fist"){
				anima.SetTrigger(Fist);
			}
			if (currentHandState == "Laser"){
				anima.SetTrigger(Gun);
			}
		}
		
	}

	void StateChangeOneShotEffects () {
		if (currentHandState != prevHandState){

			if (currentHandState == "Laser"){
				laserPointer.enabled = true;
				if ( laserPointer.holder != null ) {
					laserPointer.holder.SetActive(true);
				}
			}

			if (prevHandState == "Laser"){
				laserPointer.enabled = false;
				laserPointer.holder.SetActive(false);
			}
		}
	}

	void DropHeldObject () {
		if (heldObject != null){
			gameObject.GetComponent<FixedJoint>().connectedBody = null;
			Destroy(gameObject.GetComponent<FixedJoint>());
			//print("dropped!");
			Rigidbody rb = heldObject.GetComponent<Rigidbody>();
			if ( rb == null ) {
				rb = heldObject.transform.parent.GetComponent<Rigidbody>();
			}
			rb.velocity = device.velocity;
			rb.angularVelocity = device.angularVelocity;
			heldObject = null;
		}
	}
	void PickUpObject (Collider other) {
		if (heldObject == null && other.tag.Contains("grabbable")){
			//hudText.text = other.tag;
			//print("hi");
			heldObject = other.gameObject;
			FixedJoint handJoint = gameObject.AddComponent<FixedJoint>();
			Rigidbody otherBody = heldObject.GetComponent<Rigidbody>();
			if ( otherBody == null ) {
				otherBody = heldObject.transform.parent.GetComponent<Rigidbody>();
			}
			handJoint.connectedBody = otherBody;

			//print("Gotcha!");
		}
	}



}
