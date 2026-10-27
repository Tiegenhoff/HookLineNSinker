using UnityEngine;

using UnityEngine;

public class FishCatchAnimation : MonoBehaviour
{
    public GameObject fishDetailsPopup;

    public void ShowFishPopup()
    {
        fishDetailsPopup.SetActive(true);
        gameObject.SetActive(false);
    }
}