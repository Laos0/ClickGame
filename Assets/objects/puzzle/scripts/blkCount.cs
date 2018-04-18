using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blkCount : MonoBehaviour {

	// Use this for initialization

	public int count;

	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		count++;
	}
}
