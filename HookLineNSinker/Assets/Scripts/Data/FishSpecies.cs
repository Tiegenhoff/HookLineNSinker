using UnityEngine;

[CreateAssetMenu(fileName = "FishSpecies", menuName = "Fishing/Fish Species")]
public class FishSpecies : ScriptableObject
{
    public string speciesName;  // The name of the fish species
    public float weight;  // The weight of the fish species
    public Rarity rarity;  // The rarity of the fish species
    public Sprite icon; // The icon representing the fish species
}
