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
            config["Statistics"]["Click Value"].IntValue = 1;

            // Items Owned
            config["Items Owned"]["Item0"].IntValue = 0;
            config["Items Owned"]["Item1"].IntValue = 0;
            config["Items Owned"]["Item2"].IntValue = 0;

            // Prestige Value
            config["Prestige"]["Value"].FloatValue = 0.0f;
        }
        else
        {

            // Load the config from the file
            config = Configuration.LoadFromFile("cfg/config.cfg");
        }
    }

    // Saves the config file
    public static void saveFile(Shop mShop, MainGameManager mGM)
    {

        // Save the current time
        float timeAsSeconds = 0.0f;
        timeAsSeconds += System.DateTime.Now.Second;
        timeAsSeconds += System.DateTime.Now.Minute * 60;
        timeAsSeconds += System.DateTime.Now.Hour * 60 * 60;
        timeAsSeconds += System.DateTime.Now.DayOfYear * 60 * 60 * 24;

        // Save the time in seconds as a float value
        // Because sharpconfig can't save DateTime values, we need to convert it to a float
        config["Statistics"]["Time"].FloatValue = timeAsSeconds;
        
        // Save the block grid
        // BlockSaver.saveGrid(mGM.grid);

        // Save the items owned by the player
        for (int i = 0; i < 3; i++)
        {
            // Set the number of items owned in the config file
            config["Items Owned"]["Item" + i.ToString()].IntValue = mShop.getNumberOwned(i);
        }

        // Save the net value of the shop
        config["Statistics"]["Shop Value"].FloatValue = (float)mShop.getShopValue();

        // Save the number of items the player has unlocked
        config["Statistics"]["Items Unlocked"].IntValue = mShop.getItemsUnlocked();

        config["Statistics"]["Points"].FloatValue = mGM.currency;

        // Save the click value
        config["Statistics"]["Click Value"].IntValue = MainGameManager.clickValue;



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

    // Return the currency value
    public static float getCurrency() { return config["Statistics"]["Currency"].FloatValue; }

    public static float getTime() {
        // Get the current time
        float timeAsSeconds = 0.0f;
        timeAsSeconds += System.DateTime.Now.Second;
        timeAsSeconds += System.DateTime.Now.Minute * 60;
        timeAsSeconds += System.DateTime.Now.Hour * 60 * 60;
        timeAsSeconds += System.DateTime.Now.DayOfYear * 60 * 60 * 24;

        return config["Statistics"]["Time"].FloatValue - timeAsSeconds;
    }

    // return the config file
    public static Configuration getConfig()
    {
        return config;
    }
}
