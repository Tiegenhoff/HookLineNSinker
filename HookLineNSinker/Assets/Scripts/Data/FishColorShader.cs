using UnityEngine;
using UnityEngine.UI;

// Stores the reusable colors used to represent fish rarities
public enum FishRarityColor
{
    Common,
    Rare,
    Epic,
    Legendary
}

// Controls the color of the UI Image attached to this GameObject
public class FishColorSwap : MonoBehaviour
{
    // Stores the Image component that will receive the color change
    [SerializeField] private Image targetImage;

    // Stores the color used for Common fish
    [SerializeField] private Color commonColor = Color.white;

    // Stores the color used for Rare fish
    [SerializeField] private Color rareColor = Color.blue;

    // Stores the color used for Epic fish
    [SerializeField] private Color epicColor = Color.magenta;

    // Stores the color used for Legendary fish
    [SerializeField] private Color legendaryColor = Color.yellow;

    // Changes the Image color using a specific Color value
    public void ChangeColor(Color newColor)
    {
        // Checks whether an Image has been assigned
        if (targetImage == null)
        {
            // Prints a warning if the Image reference is missing
            Debug.LogWarning("FishColorSwap is missing its target Image.");

            // Stops the function because there is no Image to modify
            return;
        }

        // Applies the requested color to the Image
        targetImage.color = newColor;
    }

    // Changes the Image color using the rarity color enum
    public void ChangeColor(FishRarityColor rarity)
    {
        // Checks which rarity color was requested
        switch (rarity)
        {
            // Handles Common fish
            case FishRarityColor.Common:

                // Applies the Common color
                ChangeColor(commonColor);

                // Stops checking the remaining cases
                break;

            // Handles Rare fish
            case FishRarityColor.Rare:

                // Applies the Rare color
                ChangeColor(rareColor);

                // Stops checking the remaining cases
                break;

            // Handles Epic fish.
            case FishRarityColor.Epic:

                // Applies the Epic color.
                ChangeColor(epicColor);

                // Stops checking the remaining cases
                break;

            // Handles Legendary fish
            case FishRarityColor.Legendary:

                // Applies the Legendary color
                ChangeColor(legendaryColor);

                // Stops checking the remaining cases
                break;

            // Handles an invalid or unrecognized rarity
            default:

                // Prints a warning so an invalid enum can be identified
                Debug.LogWarning("An invalid fish rarity color was provided.");

                // Stops checking the remaining cases
                break;
        }
    }

    // Allows another script to assign the Image at runtime if necessary
    public void SetTargetImage(Image image)
    {
        // Updates the Image reference
        targetImage = image;
    }
}

