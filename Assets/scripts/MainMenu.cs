using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
        // Start the currency counter
        MainGameManager.Instance.startCurrencyCounter();

        // Load the save file


        // Load in to the first scene
		SceneManager.LoadScene(1);
	}

	public void Quit(){
		Application.Quit ();
	}
}
