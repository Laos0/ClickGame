using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour {

    public GameObject shopOb;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void hideShop()
    {
        shopOb.SetActive(false);
    }

    public void showShop()
    {
        shopOb.SetActive(true);
    }
}
