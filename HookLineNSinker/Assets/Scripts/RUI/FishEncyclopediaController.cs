using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishEncyclopediaController : MonoBehaviour
{
    public FishInfo[] fishes;

    [Header("Contents")]
    public FishEntryButton fishEntryButtonPrefab;
    public Transform fishButtonsContainer;
    public Button previousContentsPageButton;
    public Button nextContentsPageButton;
    public TMP_Text contentsPageNumberText;
    public int fishButtonsPerPage = 6;

    [Header("Screens")]
    public GameObject fishListScreen;
    public GameObject fishDetailsScreen;

    [Header("Fish Entry")]
    public Image fishImage;
    public TMP_Text fishNameText;
    public TMP_Text weightText;
    public TMP_Text descriptionText;

    [Header("Fish Page Navigation")]
    public Button previousPageButton;
    public Button nextPageButton;
    private int currentFishIndex;
    private int currentContentsPage;

    // This function controlls the number of Fishes allowed on the
    // table of contents list page
    private int FishPerContentsPage
    {get { return Mathf.Max(1, fishButtonsPerPage); }}

    // This calculates how many pages ares required for the table of contents
    private int TotalContentsPages
    {
        get
        {
            return Mathf.Max(
                1,
                Mathf.CeilToInt(
                    (float)fishes.Length / FishPerContentsPage
                )
            );
        }
    }
    // Calls start() the first time EncyclopediaPanel becomes active
    // and selects the first fish as the initial fish and makes sure the encyclopedia
    // begins on the table of contents instead of a fish data entry.
    private void Start()
    {
        currentFishIndex = 0;
        ShowContents();
    }
    // This function is responsible for displaying the list of fishes (FishButtons) on
    // the table of contents
    private void DisplayContentsPage()
    {
        // remove the buttons from the previously displayed page
        foreach (Transform child in fishButtonsContainer)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }
        int firstFish = currentContentsPage * FishPerContentsPage;
        int lastFish =Mathf.Min( firstFish + FishPerContentsPage,fishes.Length);
        for (int i = firstFish; i < lastFish; i++)
        {
            if (fishes[i] == null)
                continue;
            FishEntryButton newButton = Instantiate(
                fishEntryButtonPrefab,
                fishButtonsContainer
            );
            newButton.Setup(fishes[i], i, this);
        }
        contentsPageNumberText.text = "Page " + (currentContentsPage + 1) + " of " + TotalContentsPages;
        previousContentsPageButton.interactable = currentContentsPage > 0;
        nextContentsPageButton.interactable =currentContentsPage < TotalContentsPages - 1;
    }
    // this first checks that another contents page exists and adds one to the page number
    // i.e it turns to the next page
    public void NextContentsPage()
    {
        if (currentContentsPage <TotalContentsPages - 1)
        {
            currentContentsPage++;
            DisplayContentsPage();
        }
    }
    // checks that we are not on the first table of contents page
    // then subtracts one and rebuilds the contents list
    public void PreviousContentsPage()
    {
        if (currentContentsPage > 0)
        {
            currentContentsPage--;
            DisplayContentsPage();
        }
    }

    public void OpenFish(int fishIndex)
    {
        if (fishIndex < 0 || fishIndex >= fishes.Length)
            return;
        currentFishIndex = fishIndex;
        fishListScreen.SetActive(false);
        fishDetailsScreen.SetActive(true);
        DisplayCurrentFish();
    }
    // this is the "Next" button on the fish data entry screens
    public void NextFish()
    {
        if (currentFishIndex < fishes.Length - 1)
        {
            currentFishIndex++;
            DisplayCurrentFish();
        }
    }
    // this is the previous button on the fish data entry screens
    public void PreviousFish()
    {
        if (currentFishIndex > 0)
        {
            currentFishIndex--;
            DisplayCurrentFish();
        }
    }

    public void ShowContents()
    {
        currentContentsPage = currentFishIndex / FishPerContentsPage;
        fishDetailsScreen.SetActive(false);
        fishListScreen.SetActive(true);
        DisplayContentsPage();
    }

    private void DisplayCurrentFish()
    {
        FishInfo fish = fishes[currentFishIndex];
        fishNameText.text = fish.fishName;
        fishImage.sprite = fish.fishImage;
        fishImage.preserveAspect = true;
        weightText.text = fish.weightText;
        descriptionText.text = fish.description;
        previousPageButton.interactable = currentFishIndex > 0;
        nextPageButton.interactable = currentFishIndex < fishes.Length - 1;
    }
}