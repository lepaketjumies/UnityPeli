using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatformer : MonoBehaviour{
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start(){
        startPosition = transform.position;
    }
    void Update(){
        MovePlatform();
    }
    void MovePlatform(){
        if(movingRight){
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if(transform.position.x >= startPosition.x + moveDistance){
                movingRight = false;
            }
        } else {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            if(transform.position.x <= startPosition.x - moveDistance){
                movingRight = true;
            }
        }
    }
}