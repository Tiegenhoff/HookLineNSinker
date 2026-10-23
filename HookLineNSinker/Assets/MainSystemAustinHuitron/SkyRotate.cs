using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotate : MonoBehaviour
{
    public float spinSpeed = 10f;
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
