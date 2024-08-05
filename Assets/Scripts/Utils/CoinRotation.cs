using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float RotationSpeed;

    private void Start() {
        RotationSpeed = Random.Range(2f, 4f);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.isPaused == false)
        {
            transform.Rotate(Vector3.forward * RotationSpeed);
        }
        else{
            Time.timeScale = 0f;
        }


    }
}
