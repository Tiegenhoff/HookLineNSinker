using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenSaysMe : MonoBehaviour
{
    GameObject fish;
    Fishclass fishDetails;
    FishFlavor flavor;

    [Header("FishPopupUI")]
    public GameObject fishAnimPopup;
    public TextMeshProUGUI fishNameTEXT;
    public TextMeshProUGUI fishWeightTEXT;
    public TextMeshProUGUI fishFlavorTEXT;
    public Image fishImage;

    public void OpenMe(GameObject fishGet)
    {
        fish = fishGet;
        fishDetails = fish.GetComponent<Fishclass>();
        flavor = fish.GetComponent<FishFlavor>();

        fishImage.sprite = fish.GetComponent<Image>().sprite;
        
        fishNameTEXT.text = fishDetails.title;
        fishWeightTEXT.text = "Weight : " + fishDetails.weight.ToString() + " lb";

        fishFlavorTEXT.text = flavor.fishFlavor;
        
        fishAnimPopup.SetActive(true);
    }
}
