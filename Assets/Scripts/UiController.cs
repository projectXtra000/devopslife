using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text score;
    public Text strike;

    public int currentScore = 0;
    
    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        score.text = currentScore.ToString();
    }

    public void UpdateStrikes(int strikes)
    {
        strike.text = strikes.ToString();
    }

    public void ClearScore()
    {
        currentScore = 0;
        score.text = "0";
    }
}
