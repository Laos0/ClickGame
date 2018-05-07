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
                MainGameManager.Instance.currentBlock.multiplier--;
                StopAllCoroutines();

            }
        }
    }

    void activateMultiplier()
    {
        timerActive = true;
        StartCoroutine(activeTimer());
    }

    IEnumerator activeTimer()
    {
        while(timerActive)
        {
            timer--;
            timerText.text = timer.ToString();
            MainGameManager.Instance.currentBlock.multiplier++;
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
