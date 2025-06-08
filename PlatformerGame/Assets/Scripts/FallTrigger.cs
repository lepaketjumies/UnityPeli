using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class FallTrigger : MonoBehaviour{
    public List<IceBlockFall> blocks;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !isTriggered){
            isTriggered = true;
            StartCoroutine(TriggerFall());
        }
    }
    private IEnumerator TriggerFall(){
        foreach (IceBlockFall block in blocks){
            block.StartShaking();
            yield return new WaitForSeconds(0.5f);
        }
    }
}