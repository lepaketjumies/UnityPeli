using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Transform spikes;
    [SerializeField] private float upPosition = -3.7f;
    [SerializeField] private float downPosition = -5f;
    [SerializeField] private float moveSpeed = 0.05f;

    private bool isPlayerInTrigger = false;
    private bool isMovingUp = true;
    private Coroutine spikeCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (spikeCoroutine == null)
            {
                spikeCoroutine = StartCoroutine(MoveSpikes());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (spikeCoroutine != null)
            {
                StopCoroutine(spikeCoroutine);
                spikeCoroutine = null;
            }
            spikes.position = new Vector3(spikes.position.x, downPosition, spikes.position.z);
            isMovingUp = true;
        }
    }

    private IEnumerator MoveSpikes()
    {
        while (isPlayerInTrigger)
        {
            float targetPositionY = isMovingUp ? upPosition : downPosition;

            while (Mathf.Abs(spikes.position.y - targetPositionY) > 0.01f)
            {
                float newY = Mathf.MoveTowards(spikes.position.y, targetPositionY, moveSpeed * Time.deltaTime);
                spikes.position = new Vector3(spikes.position.x, newY, spikes.position.z);
                yield return null;
            }
            isMovingUp = !isMovingUp;
            yield return new WaitForSeconds(5);
        }
        spikeCoroutine = null;
    }
}