using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private AudioClip jumpSound;
    
    private AudioSource audioSource;

    void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

            if(playerRB != null){
                playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpForce);

                audioSource.clip = jumpSound;
                audioSource.Play();
            }
        }
    }
}