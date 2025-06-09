using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //public int lives = 3;
    public Transform initialRespawnPoint;
    public Transform currentRespawnPoint;
    private Rigidbody2D rb;

    public int maxHealth = 5;
    private int currentHealth;

    public Image healthIcon;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI feedbackText;
    public bool isRespawningFromCheckpoint = false;

    public GameObject collectableItem;
    public Transform dropPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentRespawnPoint = initialRespawnPoint;

        currentHealth = 3;

        UpdateHealthUI();

        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }
    private void DropCollectable(Vector3 dropPosition)
    {
        if (collectableItem != null)
        {
            Vector3 offset = new Vector3(5f, 0f, 0f);

            Vector3 spawnPosition = dropPosition + offset;

            Debug.Log("Pudotettu esine sijaintiin: " + spawnPosition);

            Instantiate(collectableItem, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Kerättävää ei ole asetettu PlayerHealth skritpiin.");
        }
    }
    public void HandleEnemyDeath()
    {
        DropCollectable(transform.position);
    }
    public void LoseLife()
    {
        currentHealth--;
        if (currentHealth > 0)
        {
            UpdateHealthUI();
            Respawn();
        }
        if (currentHealth == 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
    public void AddHealth(int healthAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + healthAmount, maxHealth);

            UpdateHealthUI();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthUI();

        if (currentHealth == 0)
        {
            LoseLife();
        }
    }
    private void UpdateHealthUI()
    {
        healthText.text = currentHealth.ToString();
    }
    void Respawn()
    {
        transform.position = currentRespawnPoint.position;
        rb.linearVelocity = Vector2.zero;
        UpdateHealthUI();
        if (!isRespawningFromCheckpoint)
        {
            ShowFeedbackText("Respawn");
        }
        else
        {
            isRespawningFromCheckpoint = false;
        }
    }
    private void ShowFeedbackText(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);

            Invoke("HideFeedbackText", 2f);
        }
    }
    private void HideFeedbackText()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }
    private void ShowCheckpointFeedback()
    {
        ShowFeedbackText("Checkpoint");
        Invoke("HideFeedbackText", 2f);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void DeathZone()
    {
        LoseLife();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Checkpoint":
                currentRespawnPoint = collision.transform;
                StartCoroutine(ShowCheckpointFeedbackDelayed());
                break;

            case "Enemy":
                float yOffset = 0.2f;
                if (transform.position.y <= collision.transform.position.y + yOffset)
                {
                    LoseLife();
                }
                break;

            case "EnemyBullet":
                LoseLife();
                break;

            case "Health":
                if (currentHealth < maxHealth)
                {
                    AddHealth(1);
                    Destroy(collision.gameObject);
                }
                break;

            case "Deathzone":
                DeathZone();
                break;
        }
    }
    private IEnumerator ShowCheckpointFeedbackDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        ShowFeedbackText("Checkpoint");
    }
}