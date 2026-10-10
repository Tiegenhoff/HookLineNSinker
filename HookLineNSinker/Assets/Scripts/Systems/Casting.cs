// Allows this script to use IEnumerator
using System.Collections;

// Allows this script to use List<T>, which is used to store fish
using System.Collections.Generic;
using UnityEngine;

// Keeps all of the casting-related code inside the project's namespace
namespace HookLineNSinker
{
    // Stores all fixed values used by the casting system
    public static class CastingConstants
    {
        // Stores the shortest possible amount of time that the casting function can wait
        public const float MIN_WAIT_SECONDS = 15f;

        // Stores the longest ppossible amount of time that the casting function can wait
        public const float MAX_WAIT_SECONDS = 30f;

        // Stores the smallest possible value that the random catch roll can produce
        public const float MIN_ROLL_VALUE = 0f;

        // Stores the largest possible value that the random catch roll can produce
        public const float MAX_ROLL_VALUE = 100f;

        // Stores the smallest valid catch rate that a fish can have
        public const float MIN_CATCH_RATE = 0f;
    }

    // Makes this class visible in the Unity Inspector while still allowing it to be serialized
    [System.Serializable]

    // Stores one fish and the enum name used to identify that fish
    public class FishEntry
    {
        // Stores the enum that identifies which fish this entry represents
        [SerializeField] private Fish fishType;

        // Stores a reference to the actual fishclass component
        [SerializeField] private Fishclass fish;

        // Allows other parts of the casting script to read the fish enum
        public Fish FishType => fishType;

        // Allows other parts of the casting script to acces the fishclass script
        public Fishclass Fish => fish;
    }

    // Makes this class visible in the Unity Inspector while still allowing it to be serialized
    [System.Serializable]

    // Stores all of the fish that can be caught at one particular depth
    public class DepthFishList
    {
        // Stores the depth that these fish belong to
        [SerializeField] private int depth;

        // Stores every fish that can be caught at this depth
        [SerializeField] private List<FishEntry> fish = new List<FishEntry>();

        // Allows the casting script to read the depth without directly changing it
        public int Depth => depth;

        // Allows the casting script to access the fish assigned to this depth
        public List<FishEntry> Fish => fish;
    }

    // Creates the main MonoBehaviour that will control the casting process
    public class CastingFunction : MonoBehaviour
    {
        // Stores all of the depth groups and the fish available at each depth
        [SerializeField] private List<DepthFishList> fishByDepth = new List<DepthFishList>();

        // Stores the currently running coroutine so that we can stop it when necessary
        private Coroutine castingCoroutine;

        // Stores a reference to the separate catch function script
        [SerializeField] private CatchFunction catchFunction;

        // Starts the casting process using the depth provided by the caller
        public void Cast(int depth)
        {
            // Checks whether another casting coroutine is already running
            if (castingCoroutine != null)
            {
                // Stopes the currently running casting coroutine so that two casting processes do not run at the same time
                StopCoroutine(castingCoroutine);

                // Clears the reference to the old cororutine because it is no longer running
                castingCoroutine = null;
            }

            // Checks whether a catch function has been assigned in the Unity inspector
            if (catchFunction == null)
            {
                // Prints a warning so we know the catch function reference is missing
                Debug.LogWarning("CastingFunction does not have a CatchFunction assigned.");

                // Stops the casting process because there is nowhere to send the selected fish
                return;
            }

            // Starts the casting coroutine and gives it the depth that the player is fishing at
            castingCoroutine = StartCoroutine(CastingRoutine(depth));
        }

        // Handles the repeated waiting, rolling, fish selection, and catching process
        private IEnumerator CastingRoutine(int depth)
        {
            // Creates an infintie loop so the fishing process continues until it is stopped
            while (true)
            {
                // Creates a random waiting time between the minimum and maximum wait times
                float waitTime = Random.Range(CastingConstants.MIN_WAIT_SECONDS, CastingConstants.MAX_WAIT_SECONDS);

                // Waits for the randomly selected amount of time before attempting to catch a fish
                yield return new WaitForSeconds(waitTime);

                // Generates a random floating-point number between the minimum and maximum roll values
                float roll = Random.Range(CastingConstants.MIN_ROLL_VALUE, CastingConstants.MAX_ROLL_VALUE);

                // Finds the best fish that the current roll is able to catch at the requested depth
                Fishclass selectedFish = SelectFish(depth, roll);

                // Checks whether a valid fish was found
                if (selectedFish != null)
                {
                    // Sends the depth and selected fish to the Catch function
                    catchFunction.Catch(depth, selectedFish);
                }
            }
        }

        // Finds which fish should be caught based on the requested depth and random roll
        private Fishclass SelectFish(int depth, float roll)
        {
            // Finds the group of fish that belongs to the requested depth
            DepthFishList depthFish = GetFishForDepth(depth);

            // Checks whether no fish group was found for the requested depth
            if (depthFish == null)
            {
                // Prints a warning to the Unity Console so we know the requested depth has no fish configured
                Debug.LogWarning("No fish have been configured for depth " + depth + ".");

                // Returns null because there is no fish available to select
                return null;
            }

            // Creates a list that will contain all fish tied for the lowest qualifying catch rate
            List<Fishclass> lowestCatchRateFish = new List<Fishclass>();

            // Stores the lowest catch rate found so far
            float lowestCatchRate = float.MaxValue;

            // Loops through every fish assigned to the requested depth
            foreach (FishEntry fishEntry in depthFish.Fish)
            {
                // Checks whether the current Inspector entry is missing
                if (fishEntry == null)
                {
                    // Skips this entry because there is no fish information to use
                    continue;
                }

                // Gets the actual fishclass component from the current fish entry
                Fishclass currentFish = fishEntry.Fish;

                // Checks whether the actual fishclass reference is missing
                if (currentFish == null)
                {
                    // Skips this entry because there is no actual fish to check
                    continue;
                }

                // Gets the catch rate from the current fish
                float currentCatchRate = currentFish.getcatchrate();

                // Checks whether the fish has an invalid catch rate below the allowed minimum
                if (currentCatchRate < CastingConstants.MIN_CATCH_RATE)
                {
                    // Skips this fish becuase it's catch rate is invalid
                    continue;
                }

                // Checks whether the random roll is lower than the fish's catch rate
                if (roll < currentCatchRate)
                {
                    // Skips this fish because the random roll did not pass it's catch rate
                    continue;
                }

                // Checks whether this fish has a lower catch rate than every qualifying fish found so far
                if (currentCatchRate < lowestCatchRate)
                {
                    // Updates the lowest catch rate to this fish's catch rate
                    lowestCatchRate = currentCatchRate;

                    // Removes any previously stored fish because this fish has a better qualifying catch rate
                    lowestCatchRateFish.Clear();

                    // Adds this fish as the new best qualifying fish
                    lowestCatchRateFish.Add(currentFish);

                    // Moves to the next fish in the depth list.
                    continue;
                }

                // Checks whether this fish ahs exactly the same catch rate as the current lowest catch rate
                if (currentCatchRate == lowestCatchRate)
                {
                    // Adds this fish to the tie list so that a random fish can be selected later
                    lowestCatchRateFish.Add(currentFish);
                }
            }

            // Checks whether no fish passed the random roll
            if (lowestCatchRateFish.Count == 0)
            {
                // Returns null because there was no fidh that the player managed to catch
                return null;
            }

            // Generates a random whole-number index from the list of tied lowest-catch-rate fish
            int selectedIndex = Random.Range(0, lowestCatchRateFish.Count);

            // Gets the fish stored at the randomly selected index
            Fishclass selectedFish = lowestCatchRateFish[selectedIndex];

            // Returns the slected fish to the casting coroutine
            return selectedFish;
        }

        // Finds the fish list associated with the depth passed into the casting function
        private DepthFishList GetFishForDepth(int depth)
        {
            // Loops through every configured depth group
            foreach (DepthFishList depthFish in fishByDepth)
            {
                // Checks whether the current depth group is missing
                if (depthFish == null)
                {
                    // Skips the missing depth group and continues searching
                    continue;
                }

                // Checks whether this depth group matches the requested depth
                if (depthFish.Depth == depth)
                {
                    // Returns the matching depth group
                    return depthFish;
                }
            }

            // Returns null because no matching depth group was found
            return null;
        }

        // Stops the current casting process when another script tell this object to stop casting
        public void StopCasting()
        {
            // Checks whether a casting coroutine is currently running.
            if (castingCoroutine != null)
            {
                // Stops the currently running casting coroutine
                StopCoroutine(castingCoroutine);

                // Clears the coroutine reference because the coroutine is no longer running
                castingCoroutine = null;
            }
        }

        // Runs automatically when this GameObject is destroyed
        private void OnDestroy()
        {
            // Checks whether a casting coroutine is currently running
            if (castingCoroutine != null)
            {
                // Stops the casting coroutine so it does not continue running after this object is destroyed
                StopCoroutine(castingCoroutine);

                // Clears the coroutine reference because the object is being destoryed
                castingCoroutine = null;
            }
        }
    }
}