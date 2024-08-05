using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ObjectTimer : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(StartTimer(timer));
        }
    }

    IEnumerator StartTimer(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
}
