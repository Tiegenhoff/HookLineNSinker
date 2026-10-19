// Allows this script to use Unity's GameObject class and Monobehaviour class
using UnityEngine;

// Keeps the catch function inside the game's namespace
namespace HookLineNSinker
{
    // Creates the MonoBehaviour that handles what happens after the casting function selects a fish
    public class CatchFunction : MonoBehaviour
    {
        // Stores a reference to the FishingMinigame script that will handle the catching minigame
        [SerializeField] private FishingMinigame fishingMinigame;
        
        // Receives the depth and already-selected fish from the casting function
        public void Catch(int depth, Fishclass selectedFish)
        {
            // Checks whether the casting function failed to provide a fish
            if (selectedFish == null)
            {
                // Prints a warning to the Unity console so we know that no fish was provided
                Debug.LogWarning("Catch function was called without a selected fish.");

                // Stops this function because there is no fish to instantiate
                return;
            }

            // Checks whether a FishingMinigame has been assigned in the Unity inspector
            if (fishingMinigame == null)
            {
                // Prints a warning so we know the FishingMinigame reference is missing
                Debug.LogWarning("CatchFunction does not have a FishingMinigame assigned.");

                // Stops the function because there is no minigame to start
                return;
            }

            // Gets the GameObject that contains the selected fishclass component
            GameObject fishPrefab = selectedFish.gameObject;

            // Creates a new instance of the already-selected fish prefab
            GameObject fish = Instantiate(fishPrefab);

            // Gets the fishclass component from the newly instantiated fish GameObject
            Fishclass fishData = fish.GetComponent<Fishclass>();

            // Checks whether the instantiated fish is missing its fishclass component
            if (fishData == null)
            {
                // Prints a warning to the Unity console so the missing fihsclass component can be found
                Debug.LogWarning("The instantiated fish does not contain a fishclass component.");

                // Destroys the incorrectly configured fish instance
                Destroy(fish);

                // Stops this function because the fish data is required by the minigame
                return;
            }

            // Sends the instantiated fish, its fish data, and the fishing depth to the minigame
            CatchMinigameFunction(fish, fishData, depth);
        }

        // Receives all of the information that the catching minigame needs to begin
        private void CatchMinigameFunction(GameObject fish, Fishclass fishData, int depth)
        {
            // Prints information to the Unity console so we can verify that the correct fish reached the minigame
            Debug.Log("Starting catch minigame for " + fishData.gettitle() + " at depth " + depth + ".");

            // Sends the fishing depth and instantiated fish GameObject to the FishingMinigame script
            fishingMinigame.StartTheCatch(depth, fish);
        }
    }
}
