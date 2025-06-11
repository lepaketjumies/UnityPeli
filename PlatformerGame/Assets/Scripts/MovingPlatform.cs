using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool startMovingUp = true;

    private Vector3 startPosition;
    private bool movingUp;

    void Start()
    {
        startPosition = transform.position;
        movingUp = startMovingUp;
    }
    void Update()
    {
        MovePlatform();
    }
    void MovePlatform()
    {
        float direction = movingUp ? 1 : -1;
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime);

        if (movingUp && transform.position.y >= startPosition.y + moveDistance)
        {
            movingUp = false;
        }
        else if (!movingUp && transform.position.y <= startPosition.y - moveDistance)
        {
            movingUp = true;
        }
    }
}