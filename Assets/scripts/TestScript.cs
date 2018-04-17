using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestScript : MonoBehaviour {
	
	// Use this for initialization
	public AudioSource audio;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		//Debug.Log ("Clicked");
		audio.Play ();

	}
}
