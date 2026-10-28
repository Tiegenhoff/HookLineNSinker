using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public partial class FishingMinigame : MonoBehaviour
{
    /*public int depth;
    public GameObject fishObj;
    //public FishClass fish; //This needs relies on the fish class existing.
    */

    public GameObject camera;
    public GameObject minigameUI; // the ui for the minigame such as the slider, skill check etc.

    public GameObject rodSlider; // the slider for the rod direction (this doesn't actually use the slider ui element)
    public GameObject rodKnob; // the knob of the rodSlider

    public GameObject skillCheckG; // the dbd skill check gameobject
    public Slider skillCheckSlider; // the slider for the skill check to change how big the check range is.

    //public GameObject fishingLine;

    public GameObject currentFish; // the current fish gobject
    public int currentDepth; // when the current depth would reach 0 you win. MAX 3400 rn

    [Header("AnimationControl")]
    public Animator ladAnimatior;
    private bool startAction = false;

    /* Setup the fishing minigame
     *  First Setup the UI using worldspace as it moves.
     *  Second, make sure the fish is in place and make it connect to the line
     *  Third ensure the knob scrollbar are in the correct 50% position
     *  Move the camera to the correct depth
     *  Start IEnumerator
    */
    public void StartTheCatch(int depth, GameObject fishObj)
    {
        Debug.Log($"Waiting To Start The Catch!!!");
        startAction = false;
        StartCoroutine(WaitForButtonPress(depth, fishObj));
    }

    // get the start press
    public void InteractWithRod()
    {
        startAction = true;
    }

    // waiting for the press
    private IEnumerator WaitForButtonPress(int depth, GameObject fishObj)
    {
        // change the animation
        ladAnimatior.Play("ladBiteraFish");

        while (!startAction)
        {
            yield return null;
        }

        ActuallyStartTheCatch(depth, fishObj);
        startAction = false;
        yield return null;
    }

    // continue to start
    private void ActuallyStartTheCatch(int depth, GameObject fishObj)
    {
        Debug.Log($"StartingCatch!!!");
        inPlay = true; // ex
        if (depth > 0)
            depth *= -1;

        // -280 is the win condition range.
        depth += (depth > -280) ? -280 : 0;

        // setup fish
        FishSetup(depth, fishObj);

        if (fishObj == null)
        {
            Debug.Log("[!!!] fishObj is null.");
            return;
        }

        // move the camera to starting depth
        StartCoroutine(MoveCameraDown(depth));

        // turn on the ui (still have to turn on specifc parts though
        minigameUI.SetActive(true);
    }



    private IEnumerator MoveCameraDown(int depth)
    {
        Vector3 start = camera.transform.position;
        Vector3 end = new Vector3(start.x, depth, start.z);

        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            
            camera.transform.position = Vector3.Lerp(start, end, t);

            yield return null;
        }

        camera.transform.position = end;
        // turn on ui elements
        rodSlider.SetActive(true);

        // turn on fishObject
        currentFish.transform.SetParent(rodKnob.transform.GetChild(0).GetChild(0).GetChild(0));
        // make transform up to the bait
        StartCoroutine(FishRise());

        // movefishing rod up along with fishGameobject
        //fishingLine.transform.localPosition += new Vector3(0, 35, 0);

        // start
        currentDepth = depth;
        StartCoroutine(FishingGo());
    }


    public void FishSetup(int depth, GameObject fishObj)
    {
        currentFish = fishObj;
        currentFish.transform.position += new Vector3(0, depth, 0); // the z is likely to change
        currentFish.SetActive(true);

    }

    IEnumerator FishRise()
    {
        float dur = 1;
        float timer = 0;

        Vector3 start = currentFish.transform.position;
        Vector3 end = Vector3.zero;

        while (timer < dur)
        {
            timer += Time.deltaTime;

            currentFish.transform.localPosition = Vector3.Lerp(start, end, timer / dur);

            yield return null;
        }

        currentFish.transform.localPosition = end;
    }
}
