using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowArea : MonoBehaviour{
    [SerializeField] private float slowTimeScale = 0.3f;
    [SerializeField] private float transitionDuration = 0.5f;
    private float originalTimeScale = 1f;

    private Coroutine slowCoroutine;

    void Start(){
        originalTimeScale = Time.timeScale;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            Debug.Log("Hidasta");
            if (slowCoroutine != null){
                StopCoroutine(slowCoroutine);
            }
            slowCoroutine = StartCoroutine(ChangeTimeScale(slowTimeScale));
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            if(slowCoroutine != null){
                StopCoroutine(slowCoroutine);
            }
            slowCoroutine = StartCoroutine(ChangeTimeScale(originalTimeScale));
        }
    }
    private IEnumerator ChangeTimeScale(float targetTimeScale){
        float startScale = Time.timeScale;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration){
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(startScale, targetTimeScale, elapsedTime / transitionDuration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        Time.timeScale = targetTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}