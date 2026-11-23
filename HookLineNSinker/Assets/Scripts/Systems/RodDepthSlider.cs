using UnityEngine;
using UnityEngine.UI;
using HookLineNSinker;

// Controls the UI slider used to select the fishing depth
public class RodDepthSlider : MonoBehaviour
{
    // Stores the UI Slider used to select the depth tier
    [SerializeField] private Slider depthSlider;

    // Stores the MainSystem that receives the selected depth
    [SerializeField] private MainSystem mainSystem;

    // Stores the currently selected depth tier
    private FishingDepth currentDepthTier;

    // Runs when the script becomes enabled
    private void OnEnable()
    {
        // Checks whether the slider has been assigned
        if (depthSlider == null)
        {
            // Prints a warning if the slider reference is missing
            Debug.LogWarning("RodDepthSlider is missing its Slider reference.");

            // Stops initialization because the slider cannot be used
            return;
        }

        // Checks whether MainSystem has been assigned
        if (mainSystem == null)
        {
            // Prints a warning if the MainSystem reference is missing
            Debug.LogWarning("RodDepthSlider is missing its MainSystem reference.");

            // Stops initialization because there is nowhere to send the depth
            return;
        }

        // Sets the smallest selectable tier
        depthSlider.minValue = (int)FishingDepth.Tier1;

        // Sets the largest selectable tier
        depthSlider.maxValue = (int)FishingDepth.Tier8;

        // Makes the slider select whole-number depth tiers
        depthSlider.wholeNumbers = true;

        // Adds the depth-changing function to the slider's value change event
        depthSlider.onValueChanged.AddListener(ChangeFishingDepth);

        // Initializes the slider using the depth currently stored in MainSystem
        SetSliderToCurrentDepth();
    }

    // Converts the current MainSystem depth into the corresponding slider value
    private void SetSliderToCurrentDepth()
    {
        // Gets the currently selected depth from MainSystem
        int currentDepth = mainSystem.GetFishingDepth();

        // Converts the actual depth into a fishing tier
        currentDepthTier = FishingDepthUtility.GetTierFromDepth(currentDepth);

        // Updates the slider without triggering its value changed event
        depthSlider.SetValueWithoutNotify((int)currentDepthTier);
    }

    // Runs whenever the player changes the slider
    public void ChangeFishingDepth(float sliderValue)
    {
        // Converts the slider value into the FishingDepth enum
        currentDepthTier = (FishingDepth)Mathf.RoundToInt(sliderValue);

        // Sends the selected tier to MainSystem
        mainSystem.SetFishingDepth(currentDepthTier);
    }

    // Runs when the script is disabled or destroyed
    private void OnDisable()
    {
        // Checks whether a slider has been assigned
        if (depthSlider != null)
        {
            // Removes the listener to prevent duplicate event subscriptions
            depthSlider.onValueChanged.RemoveListener(ChangeFishingDepth);
        }
    }
}
