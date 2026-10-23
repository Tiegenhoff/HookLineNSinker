// Provides Unity functionality, including MonoBehaviour and Color
using UnityEngine;

// Provides the UI Image component functionality for the fish sprite
using UnityEngine.UI;

// Defines a script that changes the color of a UI Image
public class FishSpriteColorSwap : MonoBehaviour
{
    // Stores a reference to the UI Image attached to the GameObject
    private Image fishImage;

    // Runs when this GameObject is initialized
    private void Awake()
    {
        // Get the Image component attached to this GameObject
        fishImage = GetComponent<Image>();
    }

    // Creates a public function that other scripts can call to change the color of the fish sprite
    public void ChangeFishColor(Color newColor)
    {
        // Sets the color of the fish sprite to the new color passed in as a parameter
        fishImage.color = newColor;
    }
}
