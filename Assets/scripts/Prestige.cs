using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Prestige {

    static float prestigeMultiplier;

    // Reset progress and prestige
    public static void prestige(float netWorth)
    {
        // Reset the save file to initial conditions
        ConfigManager.reset();

        // Apply the multiplier change based on how much money the player has
        improveMultiplier(netWorth);

        // Reload the main scene
        SceneManager.LoadScene(1);
    }

    // Improve the multiplier based on how much money the player has
    static void improveMultiplier(float netWorth)
    {

        Debug.Log("Net Worth: " + netWorth.ToString());
        // Calculate a new value for the prestige multiplier
        float newValue = netWorth * 0.00001f;

        Debug.Log("New Value: " + newValue.ToString());

        // If the new value would be higher than the current value, then replace the current value with the new one
        prestigeMultiplier = Mathf.Max(newValue, prestigeMultiplier);
    }
}
