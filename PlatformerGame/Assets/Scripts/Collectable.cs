using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour{
    public float rotationSpeed = 100f;

    private void Update(){
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            ScoringSystem.theScore += 10;
            Destroy(gameObject);
        }
    }
}