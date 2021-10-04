using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerController : MonoBehaviour
{
    public GameController gameController;
    public TretaOverlayController tretaController;
    public GameObject room;
    public Sprite computerSprite;
    public Sprite computerSpriteError;

    private int[] tretaDifficulty = { 20, 30, 45, 60 };
    private int[] tretaTimeToSolve = { 4, 6, 8, 10 };

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && gameController.isTretaHappening)
        {
            gameController.StopTretaCoroutine();
            room.SetActive(false);
            int treta = Random.Range(0, 4);
            tretaController.Show(tretaDifficulty[treta], tretaTimeToSolve[treta]);
        }
    }

    public void SetTretaStart()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = computerSpriteError;
    }

    public void SetTretaStopped()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = computerSprite;
    }
}
