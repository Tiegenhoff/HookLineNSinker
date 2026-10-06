using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public partial class FishingMinigame : MonoBehaviour
{
    public int lineTension; // increases while not aligned with fish
    private int fishStamina; // duration of struggle (current fishStamina
    public bool inPlay;
    private int lives; // 3 lives, regen 1 on skill check rounds that you don't fail
    private fishclass cfishClass;

    //skill check
    public GameObject checkRot;
    public GameObject pointerRot;
    public Slider checkSize;
    /* For lining up the rotation check the rotation vs slider amt
     * say set it to .5 and check against rotation
     * Though should assumably be 180 and .25:90 meaning the rotation as to be within those past
     * So 36 degrees per .1 and 3.6 per .01 (duh its 1.0 and 360 degrees)
     */

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
        cfishClass = currentFish.GetComponent<fishclass>();
        int stamMax = (int)cfishClass.weight * 10;

        while (inPlay)
        {
            // start skill check (getting them in a row give a bonus to the amount it travels upwards?)


            // start struggle
            fishStamina = stamMax;
            yield return StartCoroutine(FishStrugle());





            yield return null;
        }

    }

    IEnumerator SkillRoutine()
    {
        while (true)
        {

        }
    }

    IEnumerator SkillCheck()
    {

    }

    IEnumerator FishStrugle()
    {
        bool struggle = true;

        // start coroutine for knob and line color change for red

        while (struggle)
        {


            yield return null;
        }

        // end coroutine for knob and line color chng

        /*
         * For angle change, check the value of the slider and it determines the fish angle change
         * likewise determine a float to angle conversion
         * 
         */
    }



}
