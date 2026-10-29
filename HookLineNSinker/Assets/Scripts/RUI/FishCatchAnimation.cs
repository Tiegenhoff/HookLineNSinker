using UnityEngine;

public class FishCatchAnimation : MonoBehaviour
{
    public GameObject fishDetailsPopup;
    public GameObject sideUI;

    private void OnEnable()
    {
        sideUI.SetActive(false); // Deactivate the side UI while running the animation
    }

    public void ShowFishPopup() 
    {
        fishDetailsPopup.SetActive(true); // Show the popup once the animation is done
        gameObject.SetActive(false); // hide the fish shadow sprite once the animation is done
    }
}