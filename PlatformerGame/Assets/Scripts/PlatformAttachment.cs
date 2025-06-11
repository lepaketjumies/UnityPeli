using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttachment : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 playerWorldPos = collision.transform.position;
            collision.transform.SetParent(null);
            collision.transform.position = playerWorldPos;
        }
    }
}