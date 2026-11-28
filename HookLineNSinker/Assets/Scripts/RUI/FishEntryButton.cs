using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FishEntryButton : MonoBehaviour
{
    private TMP_Text fishNameText;
    private TMP_Text pageNumberText;
    private FishEncyclopediaController controller;
    private int fishIndex;
    private void Awake()
    {
        fishNameText = transform.Find("FishName").GetComponent<TMP_Text>();
        pageNumberText =transform.Find("Pagenumber").GetComponent<TMP_Text>();
    }

    public void Setup(
        FishInfo fish,
        int index,
        FishEncyclopediaController encyclopediaController)
    {
        fishNameText.text = fish.fishName;
        pageNumberText.text = (index + 1).ToString();
        fishIndex = index;
        controller = encyclopediaController;
        GetComponent<Button>().onClick.AddListener(OpenFish);
    }

    private void OpenFish()
    {
        controller.OpenFish(fishIndex);
    }
}