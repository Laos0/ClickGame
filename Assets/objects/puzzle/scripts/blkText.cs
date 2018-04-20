using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blkText : MonoBehaviour {

	// Use this for initialization

	public int textCount;
	public GameObject blkCount;

	void Start () {
		textCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Get count from blkCount
		//textCount = blkCount.GetComponent().count;  <-- GetComponent is a template function. Need to do GetComponent<Component you need>().count;
	}
}
