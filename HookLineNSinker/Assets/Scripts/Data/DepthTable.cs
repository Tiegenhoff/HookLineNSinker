using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DepthTable", menuName = "Fishing/Depth Table")]
public class DepthTable : ScriptableObject
{
    public int minDepth;
    public int maxDepth;
    public List<FishSpecies> speciesList;
}
