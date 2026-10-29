using UnityEngine;
using UnityEngine.UI;

public class ShopItemPurchase : MonoBehaviour
{
    public GameObject modSelectionButton;
    public Button shopButton;
    public CanvasGroup shopItemCanvasGroup;

    private bool purchased = false;
    // When the corresponding item in the shop menu is bought (pressed)
    // it sets that same item in the mod selection menu to active.
    // Essentially, the mod items are hidden, and buying them in the shop
    // unhides them.
    // After 'buying' an item it becomes unpressable and dimmed
    public void BuyItem()
    {
        if (purchased)
            return;

        purchased = true;
        modSelectionButton.SetActive(true);
        shopButton.interactable = false;
        shopItemCanvasGroup.alpha = 0.4f;
    }
}