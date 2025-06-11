using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    [SerializeField] private Transform spikes;
    [SerializeField] private float upPosition = 1f;
    [SerializeField] private float downPosition = 0f;
    [SerializeField] private float moveSpeed = 3f;

    private Coroutine moveCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start moving spikes up smoothly
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveSpikes(upPosition));

            // Call LoseLife on the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.LoseLife();
            }
        }
    }

    private System.Collections.IEnumerator MoveSpikes(float targetY)
    {
        while (Mathf.Abs(spikes.position.y - targetY) > 0.01f)
        {
            float newY = Mathf.MoveTowards(spikes.position.y, targetY, moveSpeed * Time.deltaTime);
            spikes.position = new Vector3(spikes.position.x, newY, spikes.position.z);
            yield return null;
        }
        spikes.position = new Vector3(spikes.position.x, targetY, spikes.position.z);
    }
}