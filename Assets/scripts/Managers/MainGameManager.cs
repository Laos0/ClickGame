using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager> {

    protected MainGameManager() { } 

    private static bool created = false;

    public GameObject rootCanvas;
    public Text scoreTxt;

    private int currency;

    public bool isGameStart;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
            
        }
    }

    private void Start()
    {
        startCurrencyCounter();
    }

    public void startCurrencyCounter()
    {
        isGameStart = true;
        StartCoroutine(addToCurrency());
    }

    IEnumerator addToCurrency()
    {
        while (isGameStart)
        {
            // Debug.Log("OnCoroutine: " + (int)Time.time);
            addToCurrency(1);
            yield return new WaitForSeconds(1f);
        }
    }

    public void resetCurrency()
    {
        currency = 0;
    }


    // Everytime a block is hit, the currency is increased based off the "value" that block contains
    public void addToCurrency(int value)
    {
        currency += value;
        updateCurrencyUI();
    }


    // Update the currency on the UI 
    private void updateCurrencyUI()
    {
        if (scoreTxt != null)
        {
            scoreTxt.text = "" + currency;
        }
        else
        {
            Debug.Log("scoreTxt equals null");
        }
      
        //Debug.Log(currency);
    }

    // Each item has a value, and currency will be deducted based off the value of that item, once purchased.
    public void subToCurrency(int value)
    {
        currency -= value;
        if(currency < 0)
        {
            currency = 0;
        }
        updateCurrencyUI();
    }

    
    // Return the current currency value
    public int getCurrency () { return currency; }
}

