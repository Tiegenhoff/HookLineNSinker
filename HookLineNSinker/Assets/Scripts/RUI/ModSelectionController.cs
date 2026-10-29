using UnityEngine;
using UnityEngine.UI;

public class ModSelectionController : MonoBehaviour
{
    public GameObject modSelectionPanel;

    public Image baitSlotIcon;
    public Image hookSlotIcon;

    private Image activeSlotIcon;

    public void OpenBaitSelection()
    {
        activeSlotIcon = baitSlotIcon;
        modSelectionPanel.SetActive(true);
    }
    public void OpenHookSelection()
    {
        activeSlotIcon = hookSlotIcon;
        modSelectionPanel.SetActive(true);
    }
    public void EquipMod(Sprite newIcon)
    {
        if (activeSlotIcon == null)
            return;

        activeSlotIcon.sprite = newIcon;
        activeSlotIcon.enabled = true;
        activeSlotIcon.preserveAspect = true;

        modSelectionPanel.SetActive(false);
    }
}