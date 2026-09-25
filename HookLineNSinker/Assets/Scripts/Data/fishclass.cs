using UnityEngine;

public class fishclass : MonoBehaviour
{
    public string title;
    public float catchrate;
    public float weight;
    public int money;
    public  Rarity rarity;
   
    public fishclass(string title, float catchrate, float weight,int money)
    {
        this.title = title;
        this.catchrate = catchrate;
        this.weight = weight;
        this.money = money;
        Rarity ratity = new Rarity("Common", 1.0f);
        rarity = ratity.randomrarity();
    }

    public int getmoney()
    {
        float newmoney = (money * rarity.getmutiplyer());
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
