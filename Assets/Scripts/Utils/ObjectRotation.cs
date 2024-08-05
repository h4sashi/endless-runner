using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private float RotationSpeed;

    public float moveDistance = 5.0f;
    public float moveSpeed = 2.0f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingUp = true;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.up * moveDistance;

    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.isPaused == false)
        {
            // transform.Rotate(Vector3.right * RotationSpeed);
        }
        else
        {
            Time.timeScale = 0f;
        }

        float step = moveSpeed * Time.deltaTime;

        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            if (transform.position == endPosition)
                movingUp = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
            if (transform.position == startPosition)
                movingUp = true;
        }

    }
}
