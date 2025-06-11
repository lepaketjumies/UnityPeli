using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Spikes")){
            Destroy(collision.gameObject);
        }
    }
}