using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TestScript : MonoBehaviour {
	
	// Use this for initialization
	public AudioSource audio;
	public Text scoreTxt;
	public int counter;

	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
        //Debug.Log ("Clicked");
        SoundManager.Instance.playBlockSound();
		counter++;
		scoreTxt.text = "" + counter;

	}
}
