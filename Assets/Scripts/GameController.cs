using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static ActivityController;

public class GameController : MonoBehaviour
{

    public int NUMBEROFTRETASTOSOLVE = 10;
    public int NUMBERSOFTRETASTOFAIL = 3;
    public bool isTretaHappening = true;
    public GameObject room;
    public UiController uiController;
    public ComputerController roomController;
    public StartMenuController menuController;
    public GameObject computer;
    public GameObject coffee;
    public GameObject toilet;
    public GameObject plant;
    public GameObject dog;
    public GameObject victoryAnim;
    public GameObject loseAnim;
    public UnityEvent onEnabled;
    public UnityEvent onWin;
    public UnityEvent onLose;

    private int counterToStartTreta = 5;
    private int currentTreta = 1;
    private int numberOfTretasFailed = 0;
    private Coroutine coroutineTreta;
    private int coffeeActivityCount = 0;
    private int plantsActivityCount = 0;
    private int toiletActivityCount = 0;
    private int dogActivityCount = 0;

    void Start()
    {
        Cursor.visible = true;

        EnableObjectColliders(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuController.isMenuOpen)
            {
                menuController.Close();
            } else
            {
                menuController.Open("RESTART");
            }
        }
    }

    private void EnableObjectColliders(bool isEnable)
    {
        coffee.GetComponent<BoxCollider2D>().enabled = isEnable;
        toilet.GetComponent<BoxCollider2D>().enabled = isEnable;
        plant.GetComponent<BoxCollider2D>().enabled = isEnable;
        dog.GetComponent<BoxCollider2D>().enabled = isEnable;

        if (isEnable)
        {
            onEnabled.Invoke();
        }
    }

    private void StartTreta()
    {
        if (!isTretaHappening)
        {
            currentTreta++;
            room.SetActive(true);
            roomController.SetTretaStart();
            coroutineTreta = StartCoroutine(StartCoroutineTretaCounter(counterToStartTreta));
            isTretaHappening = true;
        }
    }

    public void StartTretaInterval()
    {
        isTretaHappening = false;

        if (currentTreta == 1)
        {
            EnableObjectColliders(true);
        }

        if (currentTreta == NUMBEROFTRETASTOSOLVE)
        {
            EnableObjectColliders(false);
            room.SetActive(false);
            roomController.SetTretaStopped();
            StartCoroutine(StartEndScene(GetEndAnimation()));
            return;
        }

        if (numberOfTretasFailed == NUMBERSOFTRETASTOFAIL)
        {
            EnableObjectColliders(false);
            room.SetActive(false);
            roomController.SetTretaStopped();
            StartCoroutine(StartEndScene("BadEndAnimation"));
            return;
        }

        room.SetActive(false);
        roomController.SetTretaStopped();
        int interval = Random.Range(5, 21);
        StartCoroutine(StartCoroutineIntervalCounter(interval));
    }

    private IEnumerator StartEndScene(string endAnimation)
    {
        yield return new WaitForSeconds(2);

        PlayerPrefs.SetInt("score", uiController.currentScore);
        PlayerPrefs.SetInt("strikes", numberOfTretasFailed);
        PlayerPrefs.SetInt("dog_walks", dogActivityCount);
        PlayerPrefs.SetInt("coffees_taken", coffeeActivityCount);
        PlayerPrefs.SetInt("plants_watered", plantsActivityCount);
        PlayerPrefs.SetInt("shits_taken", toiletActivityCount);
        PlayerPrefs.SetString("end_animation", endAnimation);

        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }

    private string GetEndAnimation()
    {
        if (coffeeActivityCount == 0)
        {
            return "CoffeeEndAnimation";
        } 
        else if (plantsActivityCount == 0)
        {
            return "PlantsEndAnimation";
        } 
        else if (toiletActivityCount == 0)
        {
            return "ToiletEndAnimation";
        } 
        else if (dogActivityCount == 0)
        {
            return "DogEndAnimation";
        }

        return "HappyEndAnimation";
    }

    private IEnumerator StartCoroutineIntervalCounter(int interval)
    {
        int countdown = 0;
        while (countdown <= interval)
        {
            yield return new WaitForSeconds(1);
            countdown++;
        }
        StartTreta();
    }

    private IEnumerator StartCoroutineTretaCounter(int startIn)
    {
        int countdown = 0;
        while (countdown <= startIn)
        {
            yield return new WaitForSeconds(1);
            countdown++;
        }
        UpdateTretasFailed();
        StartTretaInterval();
        DevOpsLost();
    }

    public void UpdateTretasFailed()
    {
        numberOfTretasFailed++;
        uiController.UpdateStrikes(numberOfTretasFailed);
    }

    public void StopTretaCoroutine()
    {
        if (coroutineTreta != null)
        {
            StopCoroutine(coroutineTreta);
        }
    }

    public void UpdateActivityCount(ActivityType type)
    {
        if (type == ActivityType.COFFEE)
        {
            coffeeActivityCount++;
        } else if (type == ActivityType.PLANTS)
        {
            plantsActivityCount++;
        } else if (type == ActivityType.TOILET)
        {
            toiletActivityCount++;
        } else if (type == ActivityType.DOG)
        {
            dogActivityCount++;
        }
    }

    public void DevOpsWin()
    {
        victoryAnim.SetActive(true);
        onWin.Invoke();
    }

    public void DevOpsLost()
    {
        loseAnim.SetActive(true);
        onLose.Invoke();
    }
}
