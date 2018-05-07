using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameButton : MonoBehaviour {

    public GameObject mShop;
    public GameObject mMGM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called by button press
    public void saveGame()
    {
        // Save the game
        ConfigManager.saveFile(mShop.GetComponent<Shop>(), mMGM.GetComponent<MainGameManager>());
    }
}
