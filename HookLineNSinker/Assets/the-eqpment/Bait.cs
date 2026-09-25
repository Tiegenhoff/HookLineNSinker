using UnityEngine;

public class Bait : MonoBehaviour
{
    public string baitName;
    public float attractiveness;
    public int cost;

    public Bait(string baitname, float attractiveness, int cost)
    {
        this.baitName = baitname;
        this.attractiveness = attractiveness;
        this.cost = cost;
    }
    // Add bait-related properties and methods here
}