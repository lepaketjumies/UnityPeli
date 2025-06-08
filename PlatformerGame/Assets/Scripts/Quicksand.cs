using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour{
    [SerializeField] private float sinkingSpeed = 0.5f;
    [SerializeField] private float moveSpeedMultiplier = 0.5f;
    [SerializeField] private float jumpForceMultiplier = 0.7f;
    private float originalGravityScale;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

            if (playerRB != null)
            {
                originalGravityScale = playerRB.gravityScale;
                playerRB.gravityScale *= sinkingSpeed;
            }
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.moveSpeed *= moveSpeedMultiplier;
                playerMovement.jumpForce *= jumpForceMultiplier;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

            if(playerRB != null && playerRB.linearVelocity.y <= 0){
                playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, Mathf.Max(playerRB.linearVelocity.y, - sinkingSpeed));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

            if (playerRB != null)
            {
                playerRB.gravityScale = originalGravityScale;
            }

            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ResetMovement();
            }
        }
    }
}