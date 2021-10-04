using UnityEngine;
using UnityEngine.EventSystems;

public class ActivityController : MonoBehaviour
{
    public GameController gameController;
    public ActivityOverlayController activityOverlay;
    public UiController uiController;
    public int activityTime;
    public string activityText;
    public int activityScore;
    public string activityAnim;
    public ActivityType activityType;
    public Sprite[] sprites;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            gameController.UpdateActivityCount(activityType);
            activityOverlay.Show(activityText, activityTime, activityAnim);
            uiController.UpdateScore(activityScore);
        }
    }

    public enum ActivityType
    {
        COFFEE,
        PLANTS,
        TOILET,
        DOG
    }
}
