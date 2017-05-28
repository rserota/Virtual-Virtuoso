using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
	public SteamVR_TrackedObject wand;
	public SteamVR_Controller.Device device;

	private string instrumentToBeSpawned;

	private Transform handModel;
	private Animator anima;
	public Text hudText;
	public Text tagText;
	public GameObject bassPrefab;
	public GameObject drumPrefab;
	public GameObject keysPrefab;
	public GameObject clockPrefab;
	public GameObject lutePrefab;
	private SteamVR_LaserPointer laserPointer;
	public string whichHandIsThis;
	private string prevHandState;
	public string currentHandState;

	public GameObject head;

	public GameObject heldObject;
	public GameObject whatIAmPointingAt;

	public GameObject instrumentMenu;
 

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
	public void whatAmIPointingAt(object sender, PointerEventArgs e){
		tagText.text = e.target.tag;
		whatIAmPointingAt = e.target.gameObject;
		if ( e.target.tag.Contains("highlightable") ) {
			e.target.GetComponentInChildren<Highlight>().renderer.enabled = true;
		}
	}

	public void iAmPointingAtNothing(object sender, PointerEventArgs e){
		whatIAmPointingAt = null;
		instrumentToBeSpawned = null;
		tagText.text = "NOTHING";
		print("POINTER OUT");
		if ( e.target.tag.Contains("highlightable") ) {
			e.target.GetComponentInChildren<Highlight>().renderer.enabled = false;
		}
	}
	void Start () {
		print((int)wand.index);
		device = SteamVR_Controller.Input((int)wand.index);
		laserPointer.PointerIn += whatAmIPointingAt;
		laserPointer.PointerOut += iAmPointingAtNothing;
			
	}


	void Update () {
		//print(transform.position);
		device = SteamVR_Controller.Input((int)wand.index);
		//print(device.GetHairTrigger());
		//print(device.velocity);
		SetHandState();
		AnimateHandStateChange();
		StateChangeOneShotEffects();
		if (currentHandState != "Laser"){
			if (device.GetHairTriggerUp()){
				DropHeldObject();
			}

		}

		else if ( currentHandState == "Laser" ) {
			if ( whatIAmPointingAt != null ) { // we're pointing at SOMETHING
				if ( whatIAmPointingAt.tag.Contains("instrument") ) { // we're pointing at an instrument
					if ( device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Axis0) ){
						MuteRecordStateManager mrsm = whatIAmPointingAt.GetComponentInChildren<MuteRecordStateManager>();

						if ( device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y < -.5f ) {
							mrsm.muted = !mrsm.muted;
						}
						else if ( device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y > .5f ) {
							mrsm.recording = !mrsm.recording;
						}
						else {
							mrsm.scheduledToMute = !mrsm.scheduledToMute;
						}
					}
					else if ( device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip) ) {
						Destroy(whatIAmPointingAt);
						whatIAmPointingAt = null;
					}
				}
				else if ( whatIAmPointingAt.tag.Contains("spawner") ) { // we're pointing at an instrument spawner
					if ( device.GetHairTriggerUp() ) {
						if ( whatIAmPointingAt.tag.Contains("bass") ) {
							Vector3 newPos = transform.position;
							newPos.y += .2f;
							Instantiate(bassPrefab, newPos, Quaternion.identity);
						}
						else if ( whatIAmPointingAt.tag.Contains("drum") ) {
							Vector3 newPos = transform.position;
							newPos.y += .2f;
							Instantiate(drumPrefab, newPos, Quaternion.identity);
						}
						else if ( whatIAmPointingAt.tag.Contains("keys") ) {
							Vector3 newPos = transform.position;
							newPos.y += .2f;
							Instantiate(keysPrefab, newPos, Quaternion.identity);
						}
						else if ( whatIAmPointingAt.tag.Contains("clock") ) {
							Vector3 newPos = transform.position;
							newPos.y += .2f;
							Instantiate(clockPrefab, newPos, Quaternion.identity);
						}
						else if ( whatIAmPointingAt.tag.Contains("lute") ) {
							Vector3 newPos = transform.position;
							newPos.y += .2f;
							Instantiate(lutePrefab, newPos, Quaternion.identity);
						}
					}

				}
			}
			else { // we're pointing at nothing
				if ( device.GetHairTriggerDown() ) {
					Vector3 newPos = head.transform.position + (head.transform.forward * 2f);
					instrumentMenu.transform.position = newPos;
					instrumentMenu.transform.rotation = new Quaternion( 0.0f, head.transform.rotation.y, 0.0f, head.transform.rotation.w );
					
					foreach (Transform child in instrumentMenu.transform){
						child.gameObject.SetActive(true);
					}

				}	
			}
			if ( device.GetHairTriggerUp() ) {
				// while in laser mode, dismiss the instrument menu on triggerUp, regardless of what we're pointing at. 

				foreach (Transform child in instrumentMenu.transform){
					child.gameObject.SetActive(false);
				}
			}
		}



	}

	
	
	void OnTriggerStay(Collider other){
		//print("Stay!");
		if (currentHandState != "Laser"){
			if (device.GetHairTriggerDown()){
				PickUpObject(other);
			}	
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
