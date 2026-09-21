using System.Collections;
using UnityEngine;

public partial class FishingMinigame : MonoBehaviour
{
    public int lineTension;
    public int fishStamina;
    public bool inPlay;

    /*
     * Start with skill check
     * then have the fish struggle
     * repeat.
     * 
     * failure occurs when you fail 3 checks, you regain a life when you complete a skill check section without failure.
     * 
     * SkillCheck 5x - > Struggling (keep following the fish with the slider) 
     */
    IEnumerator FishingGo()
    {
        inPlay = true;

        while (inPlay)
        {









            yield return null;
        }




    }




}
