using UnityEngine;
using UnityEngine.UI;

public class EndController : MonoBehaviour
{

    public Animator animator;
    public Text score;
    public Text strikes;
    public Text coffee;
    public Text plants;
    public Text toilet;
    public Text dog;

    void Start()
    {
        score.text = "Score: "+ PlayerPrefs.GetInt("score");
        strikes.text = "Fails: " + PlayerPrefs.GetInt("strikes");
        coffee.text = "Coffees taken: " + PlayerPrefs.GetInt("coffees_taken");
        plants.text = "Watered plants: " + PlayerPrefs.GetInt("plants_watered");
        toilet.text = "Shits taken: " + PlayerPrefs.GetInt("shits_taken");
        dog.text = "Walks with the dog: " + PlayerPrefs.GetInt("dog_walks");

        animator.Play(PlayerPrefs.GetString("end_animation"));
    }
}
