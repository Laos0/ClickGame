using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeClicks : MonoBehaviour {

    public GameObject mGM;

	// Use this for initialization
	void Start () {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Upgrade Clicks - " + MainGameManager.clickValue + " \n" + "Cost - " + MainGameManager.costToUpgradeClick;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void upgradeClicks ()
    {
        // Check if the player has enough currency
        if (MainGameManager.costToUpgradeClick <= mGM.GetComponent<MainGameManager>().getCurrency()) {
            // Increase the click value
            MainGameManager.clickValue += MainGameManager.clickValue * (int)MainGameManager.clickUpgradeRate;
            // Subtract the player's funds
            mGM.GetComponent<MainGameManager>().subToCurrency(MainGameManager.costToUpgradeClick);
            // Increase the cost to purchase the upgrade
            MainGameManager.costToUpgradeClick += MainGameManager.costToUpgradeClick * MainGameManager.clickCostIncreaseRate;

            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Upgrade Clicks - " + MainGameManager.clickValue + " \n" + "Cost - " + MainGameManager.costToUpgradeClick;

        }
    }
}
