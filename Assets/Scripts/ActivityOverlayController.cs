using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static ActivityController;

public class ActivityOverlayController : MonoBehaviour
{

    public Text title;
    public Slider sliderActivityTime;
    public Animator anim;

    public void Show(string message, int activityTime, string animation)
    {
        gameObject.SetActive(true);

        title.text = message;

        anim.Play(animation);

        sliderActivityTime.minValue = 0;
        sliderActivityTime.maxValue = activityTime;
        StartCoroutine(StartCoroutineCounter(activityTime));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator StartCoroutineCounter(int activityTime)
    {
        float countdown = 0f;
        while (countdown <= activityTime)
        {
            sliderActivityTime.value = countdown;
            yield return new WaitForSeconds(1);
            countdown++;
        }
        Hide();
    }
}
