using UnityEngine;

public class FishPopupController : MonoBehaviour
{
    public GameObject sideUI;
    public GameObject parentObj;
    public GameObject shadowObj;

    // Reactivate the Side UI once the fish popup
    // is closed.
    public void ClosePopup()
    {
        sideUI.SetActive(true);

        // turn off the full seq
        parentObj.SetActive(false);

        gameObject.SetActive(false);

        // prep next
        shadowObj.SetActive(true);
        gameObject.SetActive(false);
    }

}