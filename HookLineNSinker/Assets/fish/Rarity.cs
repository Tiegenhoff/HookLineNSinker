using UnityEngine;


public class Rarity : MonoBehaviour
{
    public string title;
    public float mutiplyer;

    public Rarity(string title, float mutiplyer)
    {
        this.title = title;
        this.mutiplyer = mutiplyer;
    }

    public string gettitle()
    {
        return title;
    }
    public float getmutiplyer()
    {
        return mutiplyer;
    }

    public Rarity randomrarity()
    {
        Rarity common = new Rarity("Common", 1.0f);
        Rarity rare = new Rarity("Rare", 1.3f);
        Rarity epic = new Rarity("Epic", 1.5f);
        Rarity legendary = new Rarity("Legendary", 2.0f);
        
       Rarity[,] rarities = new Rarity[4, 4] { 
        //the rarity that can be getten
        { common,common,common,common}, 
        {common, rare, rare, rare},
       {common ,rare, epic, epic }, 
       {common ,rare, epic ,legendary } 
       };
        //randomly select a rarity from the array
        System.Random random = new System.Random();
        int row = random.Next(0, rarities.GetLength(0));
        int col = random.Next(0, rarities.GetLength(1));

        return rarities[row, col];
    }

   
}