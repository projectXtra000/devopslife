using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TretaOverlayController : MonoBehaviour
{
    public GameController gameController;
    public Slider sliderTimeToSolve;
    public Slider sliderTretaProgress;

    private int buttonClick = 0;
    private int difficulty = 0;
    private Coroutine startCounter;


    public void Show(int tretaDifficulty = 0, int tretaTimeToSolve = 0)
    {
        gameObject.SetActive(true);

        sliderTimeToSolve.minValue = 0;
        sliderTimeToSolve.maxValue = tretaTimeToSolve;
        startCounter = StartCoroutine(StartCoroutineCounter(tretaTimeToSolve));

        sliderTretaProgress.minValue = 0;
        sliderTretaProgress.maxValue = tretaDifficulty;

        difficulty = tretaDifficulty;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator StartCoroutineCounter(int timeToSolve)
    {
        float countdown = 0f;
        while (countdown <= timeToSolve)
        {
            sliderTimeToSolve.value = countdown;
            yield return new WaitForSeconds(1);
            countdown++;
        }
        buttonClick = 0;
        sliderTretaProgress.value = 0;
        gameController.UpdateTretasFailed();
        gameController.StartTretaInterval();
        Hide();
        gameController.DevOpsLost();
    }

    public void CountButtonClick()
    {
        buttonClick++;
        sliderTretaProgress.value = buttonClick;
        if (buttonClick == difficulty)
        {
            if (startCounter != null)
            {
                StopCoroutine(startCounter);
            }
            buttonClick = 0;
            sliderTretaProgress.value = 0;
            gameController.StartTretaInterval();
            Hide();
            gameController.DevOpsWin();
        }
    }
}
