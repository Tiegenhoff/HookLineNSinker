using UnityEngine;


public class lure : MonoBehaviour
{
    public strinf title;
    public int cost;
    public foalt increasecatchrate;
    
    public Fishclass fish;

    public lure(string title, int cost, float increasecatchrate, Fishclass fish)
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


    public float checkFish(Fishclass newfish)
    {
        if (fish == newfish)
        {
           return lure.applylure();
          
        }
        else
        {
            return fish.catchrate;
        }
    }
}