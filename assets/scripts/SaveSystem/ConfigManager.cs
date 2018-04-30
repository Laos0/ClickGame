using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;
using System.IO;

public class ConfigManager {

    static Configuration config;

    // Called when the game is started.  Loads the config file
    public static void loadConfig()
    {
        // Stop if the file has already been loaded
        if (config != null)
        {
            return;
        }

        // Set up the config variable
        config = new Configuration();

        // If the config file doesn't exist, create a new one
        if (!File.Exists("cfg/config.cfg"))
        {
            Debug.Log("Setting up a default config since no file was found!");
            config["Statistics"]["Points"].FloatValue = 0;
            config["Statistics"]["Shop Value"].FloatValue = 0;
            config["Statistics"]["Items Unlocked"].IntValue = 1;

            // Items Owned
            config["Items Owned"]["Item0"].IntValue = 0;
            config["Items Owned"]["Item1"].IntValue = 0;
            config["Items Owned"]["Item2"].IntValue = 0;

            // Prestige Value
            config["Prestige"]["Value"].FloatValue = 0.0f;

            saveFile();
        }
        else
        {

            // Load the config from the file
            config = Configuration.LoadFromFile("cfg/config.cfg");
        }
    }

    // Saves the config file
    public static void saveFile()
    {
        // Get a reference to the shop object
        Shop mShop = GameObject.FindWithTag("Shop").GetComponent<Shop>();

        // Get a reference to the game manager
        MainGameManager mGM = GameObject.FindWithTag("GM").GetComponent<MainGameManager>();

        // Save the items owned by the player
        for (int i = 0; i < 3; i++)
        {
            // Set the number of items owned in the config file
            config["Items Owned"]["Item" + i.ToString()].IntValue = mShop.getNumberOwned(i);
        }

        // Save the net value of the shop
        config["Statistics"]["Shop Value"].FloatValue = mShop.getShopValue();

        // Save the number of items the player has unlocked
        config["Statistics"]["Items Unlocked"].IntValue = mShop.getItemsUnlocked();

        config["Statistics"]["Points"].FloatValue = mGM.currency;



        // Save the config file
        config.SaveToFile("cfg/config.cfg");
    }

    // Return the stored number of points
    public static int getPoints()
    {
        return config["Statistics"]["Points"].IntValue;
    }

    // Return the number of items the player has already unlocked
    public static int getItems()
    {
        Debug.Log("Items: " + config["Statistics"]["Items Unlocked"].IntValue.ToString());
        return config["Statistics"]["Items Unlocked"].IntValue;
    }

    // Return the number of items owned of the type given by the index
    public static int getItemsOwned(int index)
    {
        return config["Items Owned"]["Item"+index.ToString()].IntValue;
    }

    public static float getShopValue()
    {
        return config["Statistics"]["Shop Value"].FloatValue;
    }


    // Reset the config file
    public static void reset()
    {
        // Reset the items owned
        for (int i = 0; i < 3; i++)
        {
            config["Items Owned"]["Item" + i.ToString()].IntValue = 0;
        }   
    }
}
