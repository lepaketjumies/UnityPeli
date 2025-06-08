using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour{
    public Transform player;
    public GameObject projectilePrefab;
    public float detectionRange  =10f;
    public float throwInterval = 2f;
    public float throwForce = 10f;
    public Vector2 throwArc = new Vector2(0.5f, 1f);
    private float lastThrowTime = 0f;
    private bool isFacingRight = true;

    void Update(){
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer <= detectionRange){
            FacePlayer();

            if(Time.time >= lastThrowTime + throwInterval){
                ThrowProjectile();
                lastThrowTime = Time.time;
            }
        }
    }
    private void FacePlayer(){
        if((player.position.x > transform.position.x && !isFacingRight) || (player.position.x < transform.position.x && isFacingRight)){
            Flip();
        }
    }
    private void Flip(){
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void ThrowProjectile(){
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector2 throwDirection = (player.position - transform.position).normalized;
        Vector2 arcThrow = new Vector2(throwDirection.x * throwArc.x, throwArc.y).normalized * throwForce;

        rb.linearVelocity = arcThrow;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            if (collision.transform.position.y > transform.position.y + 0.5f){
                Destroy(gameObject);
            }
            else{
                collision.GetComponent<PlayerHealth>().LoseLife();
            }
        }
        if (collision.CompareTag("Bullet")){
            Destroy(gameObject);
        }
    }
}