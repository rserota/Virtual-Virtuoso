using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sorting : MonoBehaviour {
	List<int> numbers = new List<int>{1,3,5,4};

	// Use this for initialization
	void Start () {
		var name = "hi";
		print(numbers);
		print(name);
		print(1>3);
		numbers.Sort(delegate(int x, int y) {
			return x - y;
		});
		foreach(int num in numbers){
			print(num);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print(Time.time);
	}
	void FixedUpdate(){
		//print(Time.time);
	}
}
