using UnityEngine;

public class Fishclass : MonoBehaviour
{
    public string title;
    public float catchrate;
    public float weight;
    public int money;
    public Rarity type;
   
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
