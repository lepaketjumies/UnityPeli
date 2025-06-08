using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxJumps = 2;

    private float normalMoveSpeed;
    private float defaultJumpForce;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private int jumpCount = 0;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        normalMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
    }

    public void ResetMovement()
    {
        moveSpeed = normalMoveSpeed;
        jumpForce = defaultJumpForce;
    }
    void Update(){
        MovePlayer();
        Jump();
        FlipSprite();
    }
    void MovePlayer(){
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.linearVelocity.y);
    }
    void Jump(){
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            isGrounded = false;
        }
    }
    void FlipSprite(){
        float moveX = Input.GetAxis("Horizontal");
        if (moveX > 0 && !facingRight){
            Flip();
        }
        else if (moveX < 0 && facingRight){
            Flip();
        }
    }
    void Flip(){
        facingRight = !facingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}