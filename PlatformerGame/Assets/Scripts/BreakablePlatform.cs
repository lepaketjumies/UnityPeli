using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour{
    [SerializeField] private float delayBeforeBreak = 0.5f;
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private GameObject debrisParent;
    [SerializeField] private float respawnTime = 5f;

    private Vector3 originalPosition;
    private bool isBreaking = false;

    private Vector3[] debrisOriginalPositions;

    void Start(){
        originalPosition = transform.position;
        if (debrisParent != null){
            debrisOriginalPositions = new Vector3[debrisParent.transform.childCount];
            for (int i = 0; i < debrisParent.transform.childCount; i++){
                debrisOriginalPositions[i] = debrisParent.transform.GetChild(i).position;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") && !isBreaking){
            StartCoroutine(BreakPlatform());
        }
    }
    private IEnumerator BreakPlatform(){
        isBreaking = true;

        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration){
            Vector3 randomOffset = new Vector3(Random.Range(-shakeIntensity, shakeIntensity),
            Random.Range(-shakeIntensity,shakeIntensity), 0f);

            transform.position = originalPosition + randomOffset;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = originalPosition;
        yield return new WaitForSeconds(delayBeforeBreak);

        gameObject.SetActive(false);

        if(debrisParent != null){
            foreach(Transform debris in debrisParent.transform){
                Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
                if(rb != null){
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        PlatformerRespawner.Instance.RespawnPlatform(this, respawnTime);
    }
    public void ResetPlatform(){
        gameObject.SetActive(true);
        if (debrisParent != null){
            for (int i = 0; i < debrisParent.transform.childCount; i++){
                Transform debris = debrisParent.transform.GetChild(i);
                debris.position = debrisOriginalPositions[i];

                Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
                if (rb != null){
                    rb.bodyType = RigidbodyType2D.Static;
                    rb.linearVelocity = Vector2.zero;
                }
            }
            isBreaking = false;
        }
    }
}