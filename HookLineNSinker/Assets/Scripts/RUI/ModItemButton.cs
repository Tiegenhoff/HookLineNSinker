using UnityEngine;

public class ModItemButton : MonoBehaviour
{
    public ModSelectionController selectionController;
    public Sprite itemIcon;

    public void SelectItem()
    {
        selectionController.EquipMod(itemIcon);
    }
}