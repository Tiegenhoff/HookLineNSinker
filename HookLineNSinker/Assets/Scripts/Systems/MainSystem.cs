using HookLineNSinker;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// Allows this script to access Unity's UI Slider component
using UnityEngine.UI;

public class MainSystem : MonoBehaviour
{
    // ex
    [SerializeField] private CastingFunction castingFunction;
    [SerializeField] private FishingMinigame fishingMinigame;
    [SerializeField] private int depth = -670;
    private Coroutine playLoop;

    public bool pause; // Will disable the ability to activate the fish mingame

    public FishingRod currentFishingRod;
    public Lure currentLure;
    public List<Bait> currentBait;

    //private HookLineNSinker.CastingFunction

    /* Lets split up the depths into parts (each will be at the tier's end point) (390 intervals)
     * Top -280
     * 1: -670
     * 2: -1060
     * 3: -1450
     * 4: -1840
     * 5: -2230
     * 6: -2620
     * 7: -3010
     * 8: -3400(End)
     * Remember some tiers can have repeat fish 
     */


    // call from the play button at the start
    public void BeginPlay()
    {
        if (playLoop != null)
            return;
        // call a load script function
        //blank

        // Begin loop
        playLoop = StartCoroutine(PlayLoop());

    }

    private IEnumerator PlayLoop()
    {
        while (true)
        {
            Debug.Log($"Starting Casting");
            castingFunction.Cast(depth);

            while (!fishingMinigame.inPlay)
            {
                yield return null;
            }

            Debug.Log($"End Casting");
            castingFunction.StopCasting();

            while (fishingMinigame.inPlay)
            {
                yield return null;
            }

            yield return null;
        }
    }

    // Updates the fishing depth using the selected depth tier
    public void SetFishingDepth(HookLineNSinker.FishingDepth selectedTier)
    {
        // Converts the selected tier into its actual casting depth
        depth = HookLineNSinker.FishingDepthUtility.GetDepthValue(selectedTier);

        // Prints the selected depth so it can be checked in the Console
        Debug.Log("Fishing depth changed to: " + depth);
    }

    // Returns the currently selected fishing depth
    public int GetFishingDepth()
    {
        // Returns the depth currently stored by MainSystem
        return depth;
    }

}

// Note to self: Don't forget to fix the handle rotation when slidiing
// Also fix the Skill Check position
// Also add a click to start the fishing process
