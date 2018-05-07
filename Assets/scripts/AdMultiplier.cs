using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdMultiplier : MonoBehaviour {

    int timer = 30;
    bool timerActive = false;

    public Text timerText;

    void update()
    {
        if(timerActive)
        {
            if (timer == 0)
            {
                timerActive = false;
                timer = 30;
                StopAllCoroutines();
            }
        }
    }

    void activateMultiplier()
    {
        timerActive = true;
        MainGameManager.Instance.currentBlock.multiplier = 2;
        StartCoroutine(activeTimer());
    }

    IEnumerator activeTimer()
    {
        while(timerActive)
        {
            timer--;
            timerText.text = timer.ToString();
            yield return new WaitForSeconds(1);

            if(timer == 0)
            {
                timerActive = false;
                timer = 30;
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("CLICK");
        activateMultiplier();
    }
}
