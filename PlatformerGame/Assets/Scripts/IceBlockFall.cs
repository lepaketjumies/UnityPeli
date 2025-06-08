using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockFall : MonoBehaviour{
    public float shakeDuration = 1f;
    public float shakeMagnitude = 0.1f;
    public float fallDelay = 0.5f;

    private bool isShaking = false;
    private Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void StartShaking(){
        if (!isShaking){
            isShaking = true;
            StartCoroutine(ShakeAndFall());
        }
    }
    private IEnumerator ShakeAndFall(){
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration){
            float xoffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yoffset = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = originalPosition + new Vector3(xoffset, yoffset, 0);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
    }
}