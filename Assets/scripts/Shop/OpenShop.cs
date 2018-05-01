using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour {

    public GameObject shopOb;
    
	// Use this for initialization
	void Start () {
        shopOb.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Close the shop
    public void hideShop()
    {
        shopOb.SetActive(false);
    }

    // Open the shop
    public void showShop()
    {
        shopOb.SetActive(true);
    }
}
