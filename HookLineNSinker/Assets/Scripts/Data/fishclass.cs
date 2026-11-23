using HookLineNSinker;
using UnityEngine;

public class Fishclass : MonoBehaviour
{
    public string title;
    public float catchrate;
    public float weight;
    public int money;
    public Rarity type;

    // added this (use an Enum for the different types but generally it'll be something like 1 = catfish)
    // anyways the important part is the number anyways the enum just makes it easier on us (don't worry about it too much)
    // Identifies the species of this fish using your Fish enum
    public Fish fishType;
    // Identifies the depth tier where this fish belongs
    public FishingDepth fishingDepth;
    
    public Fishclass(string title, float catchrate, float weight,int money)
    {
        this.title = title;
        this.catchrate = catchrate;
        this.weight = weight;
        this.money = money;
        type = new Rarity("Common", 1.0f);
        type = type.randomrarity();
    }

    public int getmoney()
    {
        float newmoney = (money * type.getmutiplyer());
        money=(int)newmoney;
        return money;
    }
    public string gettitle()
    {
        return title;
    }
    public float getcatchrate()
    {
        return catchrate;
    }
    public float getweight()
    {
        return weight;
    }
}
