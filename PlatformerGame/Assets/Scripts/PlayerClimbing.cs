using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour{
    [SerializeField] private float climbSpeed = 1f;
    private bool isClimbing = false;
    private Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        if(isClimbing){
            float vertical = Input.GetAxis("Vertical");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Ladder")){
            isClimbing = true;
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Ladder")){
            isClimbing = false;
            rb.gravityScale = 1;
        }
    }
}