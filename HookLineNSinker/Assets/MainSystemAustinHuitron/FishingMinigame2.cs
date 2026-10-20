using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public partial class FishingMinigame : MonoBehaviour
{
    private int lineTension; // increases while not aligned with fish
    private int fishStamina; // duration of struggle (current fishStamina
    public bool inPlay;
    private int lives; // 3 lives, regen 1 on skill check rounds that you don't fail
    private Fishclass cfishClass;

    // skill check
    public GameObject checkObj;
    public GameObject checkRot;
    public GameObject pointerRot;
    public Slider checkSize;
    private int totalChecks;
    private bool noFailure = true;
    public float pointerSpeed = 200;
    /* For lining up the rotation check the rotation vs slider amt
     * say set it to .5 and check against rotation
     * Though should assumably be 180 and .25:90 meaning the rotation as to be within those past
     * So 36 degrees per .1 and 3.6 per .01 (duh its 1.0 and 360 degrees)
     */

    // Rod alignment Fish Struggle
    public Slider rodAlignment;
    public GameObject rodHandle;

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

        //inPlay = true; ex
        cfishClass = currentFish.GetComponent<Fishclass>();
        int stamMax = (int)(cfishClass.weight * 3);
        lineTension = 0; // breaks if it hits 10
        rodHandle.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        lives = 3;

        totalChecks = 0; // as it goes up checks get harder

        while (inPlay)
        {
            // start skill check (getting them in a row give a bonus to the amount it travels upwards?)
            checkObj.SetActive(true);
            yield return StartCoroutine(SkillRoutine());
            checkObj.SetActive(false);

            // check for failure
            if (lives == 0)
            {
                inPlay = false;
                win = false;
                break;
            }
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
            
            // check for failure
            if (lineTension >= 10)
            {
                inPlay = false;
                win = false;
                break;
            }

            yield return null;
        }

        // this will be implemented later once more ui has been developed
        if (win)
        {

        }
        else
        {

        }
        // return to the surface
        StartCoroutine(ReturnToSurface(.15f));
    }

    IEnumerator SkillRoutine()
    {
        int checkCount = Random.Range(3, 7);
        noFailure = true;

        while (checkCount > 0)
        {
            if (lives == 0)
            {
                checkCount = 0;
                break;
            }

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
        checkFloat = Mathf.Clamp(checkFloat, 0.05f, 0.25f);

        checkSize.value = checkFloat;
        Debug.Log($"Check Value = {checkFloat}");

        // set random rotation
        float randRotation = Random.Range(0, 360);
        checkRot.transform.rotation = Quaternion.Euler(0, 0, randRotation);
        
        randRotation = Random.Range(0, 360);
        pointerRot.transform.rotation = Quaternion.Euler(0, 0, randRotation);

        // set random position

        // consider setting a random speed later
        //float pointerSpeed = 100;

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

        // is the pointer within the check zone?
        float zoneWidth = checkFloat * 360f;
        //float angleFromStart = (pRotNum - cRotNum + 360f) % 360f;
        float angleFromStart = (cRotNum - pRotNum + 360f) % 360f;

        if (angleFromStart <= zoneWidth)
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

        totalChecks++;
        yield return new WaitForSeconds(1);
    }

    IEnumerator FishStrugle()
    {
        // start coroutine for knob and line color change for red
        
        int fS = fishStamina * 3; // perhaps make the multiplier a modifier later on?

        // decrease lineTension
        lineTension = (lineTension > 5) ? Random.Range(0, 4) : 0;

        while (fS > 0)
        {
            /*
             * tension line increases by 1 when the knob rotation is over like 7 or 8
             * likewise decrease by 1 when the knob rotation is under.
             * if tension gets to 10 the line breaks and the fish escapes
             */


            /*if (rodHandle.transform.localRotation.z > 7 || rodHandle.transform.localRotation.z < -7)
                lineTension++;*/
            float handleAngle = Mathf.DeltaAngle( 0f, rodHandle.transform.localEulerAngles.z);

            if (Mathf.Abs(handleAngle) > 7f)
                lineTension++;

            if (lineTension >= 10)
                yield break;

            // higher the tension closer to red it is.
            rodHandle.GetComponent<Image>().color = new Color(1f, (1f - ((float)lineTension * .1f)), (1f - ((float)lineTension * .1f)), 1f);

            // change position / rotation
            float randChange = Random.Range(0, 100);
            if (randChange < 35)
            {
                StartCoroutine(FishRotate());
            }

            yield return new WaitForSeconds(.5f);
            fS--;
        }

        // end coroutine for knob and line color chng

        /*
         * For angle change, check the value of the slider and it determines the fish angle change
         * likewise determine a float to angle conversion
         * 
         */
    }

    IEnumerator FishRotate()
    {

        // rotate the knob and therefore the fish on the line
        float dur = .15f;
        float timer = 0;

        Quaternion start = rodHandle.transform.localRotation;

        // pick the rotation based on the slider value
        Quaternion end = getRotationDirection();
        if (end == Quaternion.Euler(0, 0, 0))
            yield break;

        while (timer < dur)
        {
            timer += Time.deltaTime;

            rodHandle.transform.localRotation = Quaternion.Lerp(start, end, timer / dur);

            yield return null;
        }

        rodHandle.transform.localRotation = end;
    }

    private Quaternion getRotationDirection() // can end up changing this later for more dramatic distant rotations.
    {
        Quaternion rot = Quaternion.Euler(0, 0, 0);

        int direction = 0;

        if (rodAlignment.value <= 0.25f)
            direction = 1; // left
        else if (rodAlignment.value >= 0.75f)
            direction = -1; // right
        else
            direction = Random.Range(0, 2) == 0 ? -1 : 1; // random

        return rot = Quaternion.Euler(0, 0, 15f * direction);
    }

    // return camera to defaut position
    private IEnumerator ReturnToSurface(float dur)
    {
        float timer = 0;

        Vector3 start = camera.transform.position;
        Vector3 end = new Vector3(0, 0, -695);
        
        while (timer < dur)
        {
            timer += Time.deltaTime;

            camera.transform.position = Vector3.Lerp(start, end, timer / dur);

            yield return null;
        }

        camera.transform.position = end;

        // turn off fishing ui
        minigameUI.SetActive(false);
    }
}
