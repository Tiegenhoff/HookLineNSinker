using HookLineNSinker;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        // call a load script function
        //blank

        // Begin loop
        StartCoroutine(PlayLoop());

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
    
}

// Note to self: Don't forget to fix the handle rotation when slidiing