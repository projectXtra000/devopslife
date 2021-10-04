using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text score;
    public Text strike;
    public Image strike1;
    public Image strike2;
    public Image strike3;
    public Sprite spriteFail;

    public int currentScore = 0;
    
    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        score.text = currentScore.ToString();
    }

    public void UpdateStrikes(int strikes)
    {
        strike.text = strikes.ToString();
        if (strikes == 1)
        {
            strike1.sprite = spriteFail;
        }
        else if (strikes == 2)
        {
            strike2.sprite = spriteFail;
        }
        else if (strikes == 3)
        {
            strike3.sprite = spriteFail;
        }
    }

    public void ClearScore()
    {
        currentScore = 0;
        score.text = "0";
    }
}
