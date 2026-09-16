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
       Rarity[,] rarities = new Rarity[4, 4] { 
        //the rarity that can be getten
        { new Rarity("Common", 1.0f),new Rarity("Common", 1.0f),new Rarity("Common", 1.0f),new Rarity("Common", 1.0f)}, 
        {new Rarity("Common", 1.0f),new Rarity("Rare", 1.3f),new Rarity("Rare", 1.3f),new Rarity("Rare", 1.3f)},
       {new Rarity("Common", 1.0f) ,new Rarity("Rare", 1.3f),new Rarity("Epic", 1.5f), new Rarity("Epic", 1.5f) }, 
       {new Rarity("Common", 1.0f) ,new Rarity("Rare", 1.3f),new Rarity("Epic", 1.5f) ,new Rarity("Legendary", 2.0f) } 
       };
        //randomly select a rarity from the array
        System.Random random = new System.Random();
        int row = random.Next(0, rarities.GetLength(0));
        int col = random.Next(0, rarities.GetLength(1));

        return rarities[row, col];
    }

   
}