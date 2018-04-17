using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHp : MonoBehaviour {


	// Use this for initialization
	public int hp;
	void Start () {
		hp = 3;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		hp--;
		if (hp == 0) {
			Destroy (gameObject);
		}
	}
}
