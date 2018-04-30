using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeButton : MonoBehaviour {

    public GameObject mShop;
    public GameObject mGameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Prestige
    //      Called by button press
    public void prestige()
    {
        // Delete the old game manager
        // A new one will be generated automatically when the scene is reloaded
        GameObject.Destroy(mGameManager);
        Prestige.prestige(mGameManager.GetComponent<MainGameManager>().currency + mShop.GetComponent<Shop>().getShopValue());
    }
}
