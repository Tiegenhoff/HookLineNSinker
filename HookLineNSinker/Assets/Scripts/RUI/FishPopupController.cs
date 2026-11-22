using UnityEngine;

public class FishPopupController : MonoBehaviour
{
    public GameObject sideUI;

    // Reactivate the Side UI once the fish popup
    // is closed.
    public void ClosePopup()
    {
        sideUI.SetActive(true);
        gameObject.SetActive(false);
    }
}