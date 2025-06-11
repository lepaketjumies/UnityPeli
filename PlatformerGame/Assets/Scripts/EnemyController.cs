using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour{
    public float moveSpeed = 3f;
    public float patrolDistance = 20f;
    private bool movingRight = true;

    private float leftBoundary;
    private float rightBoundary;
    private Vector3 startPosition;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    void Start(){
        startPosition = transform.position;

        leftBoundary = startPosition.x - patrolDistance;
        rightBoundary = startPosition.x + patrolDistance;
    }
    void Update(){
        Patrol();
    }
    private void Patrol() {
        bool isGroundAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!isGroundAhead) {
            movingRight = !movingRight;
            Flip();
            return;
        }
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (movingRight && transform.position.x >= rightBoundary)
        {
            movingRight = false;
            Flip();
        } else if (!movingRight && transform.position.x <= leftBoundary)
        {
            movingRight = true;
            Flip();
        }
    }
    private void Flip(){
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Check if player is above enemy
                float yOffset = 0.2f;
                if (collision.transform.position.y > transform.position.y + yOffset)
                {
                    playerHealth.HandleEnemyDeath();
                    Destroy(gameObject);
                }
                else
                {
                    playerHealth.LoseLife();
                }
            }
        }
        else if (collision.collider.CompareTag("Bullet"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            playerHealth.HandleEnemyDeath();
        }
        else if (collision.collider.CompareTag("Obstacle"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }
}
