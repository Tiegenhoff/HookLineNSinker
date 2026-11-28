using UnityEngine;

[CreateAssetMenu(fileName = "FishName", menuName = "Encyclopedia/Fish Info")]
public class FishInfo : ScriptableObject
{
    public string fishName;
    public Sprite fishImage;
    public string weightText;
    [TextArea(3, 10)]
    public string description;
}