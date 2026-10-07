using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public partial class FishingMinigame : MonoBehaviour
{
    private int lineTension; // increases while not aligned with fish
    private int fishStamina; // duration of struggle (current fishStamina
    public bool inPlay;
    private int lives; // 3 lives, regen 1 on skill check rounds that you don't fail
    private fishclass cfishClass;

    // skill check
    public GameObject checkObj;
    public GameObject checkRot;
    public GameObject pointerRot;
    public Slider checkSize;
    private int totalChecks;
    private bool noFailure = true;
    /* For lining up the rotation check the rotation vs slider amt
     * say set it to .5 and check against rotation
     * Though should assumably be 180 and .25:90 meaning the rotation as to be within those past
     * So 36 degrees per .1 and 3.6 per .01 (duh its 1.0 and 360 degrees)
     */

    // Rod alignment Fish Struggle
    public Slider RodAlignment;
    public GameObject RodHandle;

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
        bool win = false;

        inPlay = true;
        cfishClass = currentFish.GetComponent<fishclass>();
        int stamMax = (int)(cfishClass.weight * 3);

        totalChecks = 0; // as it goes up checks get harder

        while (inPlay)
        {
            // start skill check (getting them in a row give a bonus to the amount it travels upwards?)
            checkObj.SetActive(true);
            yield return StartCoroutine(SkillRoutine());
            checkObj.SetActive(false);

            // check for win
            if (camera.transform.position.y > -280f)
            {
                inPlay = false;
                win = true;
                break;
            }

            // start struggle
            fishStamina = stamMax;
            yield return StartCoroutine(FishStrugle());

            yield return null;
        }

        // this will be implemented later once more ui has been developed
        if (win)
        {

        }
        else
        {

        }
    }

    IEnumerator SkillRoutine()
    {
        int checkCount = Random.Range(3, 7);
        noFailure = true;

        while (checkCount > 0)
        {
            // if win condition has already been met
            if (camera.transform.position.y > -280f)
            {
                checkCount = 0;
                break;
            }

            // skill check
            yield return StartCoroutine(SkillCheck());

            checkCount--;
            yield return null;
        }

        if (noFailure && lives < 3)
            lives++;
    }

    IEnumerator SkillCheck()
    {

        // set random check size
        float checkFloat = Random.Range(.15f, .25f);
        float decreasePercentage = Random.Range(0, totalChecks + 1);
        checkFloat *= 1f - (.1f * decreasePercentage);
        
        checkSize.value = checkFloat;
        Debug.Log($"Check Value = {checkFloat}");

        // set random rotation
        float randRotation = Random.Range(0, 360);
        checkRot.transform.rotation = Quaternion.Euler(0, 0, randRotation);

        // consider setting a random speed later
        float pointerSpeed = 100;

        // giving the player a second to breathe
        yield return new WaitForSeconds(1);

        bool spinMeBaby = true;

        while (spinMeBaby)
        {
            pointerRot.transform.Rotate(0, 0, pointerSpeed * Time.deltaTime);
            if (Mouse.current.leftButton.wasPressedThisFrame)
                spinMeBaby = false;

            yield return null;
        }

        // after check and effects
        float pRotNum = pointerRot.transform.eulerAngles.z;
        float cRotNum = checkRot.transform.eulerAngles.z;

        Debug.Log($"Pointer = {pRotNum} \nCheck = {cRotNum}");

        if (pRotNum !< cRotNum && (pRotNum - cRotNum) <= (cRotNum + (checkFloat * 360)))
        {
            Debug.Log("Win");
            float height = 10; // this can be changed or edited down the line
            camera.transform.position += new Vector3(0, height, 0);
        }
        else
        {
            Debug.Log("Lose");
            noFailure = false;
            lives--;
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator FishStrugle()
    {
        // start coroutine for knob and line color change for red

        while (fishStamina > 0)
        {
            // change position / rotation
            float randChange = Random.Range(0, 100);
            if (randChange < 35)
            {

            }

            yield return new WaitForSeconds(1);
            fishStamina--;
        }

        // end coroutine for knob and line color chng

        /*
         * For angle change, check the value of the slider and it determines the fish angle change
         * likewise determine a float to angle conversion
         * 
         */
    }

}
