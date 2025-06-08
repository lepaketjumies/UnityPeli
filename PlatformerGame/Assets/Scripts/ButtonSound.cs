using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource audioSource;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }
    void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}