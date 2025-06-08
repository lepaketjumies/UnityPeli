using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerRespawner : MonoBehaviour{
    public static PlatformerRespawner Instance;

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void RespawnPlatform(BreakablePlatform platform, float respawnDelay){
        StartCoroutine(RespawnCoroutine(platform, respawnDelay));
    }
    private IEnumerator RespawnCoroutine(BreakablePlatform platform, float respawnDelay){
        yield return new WaitForSeconds(respawnDelay);
        platform.ResetPlatform();
    }
}