using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkyRotate : MonoBehaviour
{
    public float spinSpeed = 10f;

    [Header("Background UI")]
    public GameObject lad;
    public GameObject dock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpinBaby());
    }

    private IEnumerator SpinBaby()
    {
        while (true)
        {
            transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
            yield return null;
        }
    }

}
