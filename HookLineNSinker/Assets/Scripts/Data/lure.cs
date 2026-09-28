using UnityEngine;


public class lure : MonoBehaviour
{
    public string title;
    public int cost;
    public float increasecatchrate;
    
    public fishclass fish;

    public lure(string title, int cost, float increasecatchrate, fishclass fish)
    {
        this.title = title;
        this.cost = cost;
        this.increasecatchrate = increasecatchrate;
        this.fish = fish;
    }

    public float applylure()
    {
        fish.catchrate *= increasecatchrate;
        return fish.catchrate;
    }


    public float checkFish(fishclass newfish)
    {
        if (fish == newfish)
        {
            return applylure();
          
        }
        else
        {
            return fish.catchrate;
        }
    }
}