using UnityEngine;


public class Lure : MonoBehaviour
{
    public string title;
    public int cost;
    public float increasecatchrate;
    
    public List<Fishclass> fish;

    public Lure(string title, int cost, float increasecatchrate, List<Fishclass> fish)
    {
        this.title = title;
        this.cost = cost;
        this.increasecatchrate = increasecatchrate;
        this.fish = fish;
    }

    
}