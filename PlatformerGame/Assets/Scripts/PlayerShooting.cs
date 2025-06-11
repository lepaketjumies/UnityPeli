using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float arcHeight = 0.1f;

    private int ammoCount = 0;
    public TextMeshProUGUI ammoText;
    public int maxAmmo = 7;

    void Start(){
        ammoText.text = "Ammo: " + ammoCount;
    }

    void Update(){
        if (Input.GetButtonDown("Fire1") && CursorChange.cursorChanged && ammoCount > 0){
            Shoot();
        }
    }
    public void AddAmmo(){
        ammoCount = maxAmmo;
        UpdateAmmoText();
    }
    public void UpdateAmmoText(){
        ammoText.text = "Ammo: " + ammoCount;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Ammo")){
            AddAmmo();
            Destroy(other.gameObject);
        }
    }
    void Shoot(){
        ammoCount--;
        UpdateAmmoText();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 direction = (mousePosition - firePoint.position).normalized;

        StartCoroutine(MoveBullet(bullet, direction));
    }

    private IEnumerator MoveBullet(GameObject bullet, Vector3 direction)
    {
        Vector3 startPosition = bullet.transform.position;
        float distance = Vector3.Distance(startPosition, startPosition + direction * 10);
        float time = 0;

        while (time < 1)
        {
            if (bullet == null) yield break;
            time += Time.deltaTime * bulletSpeed / distance;
            float height = Mathf.Sin(Mathf.PI * time) * arcHeight;
            bullet.transform.position = Vector3.Lerp(startPosition, startPosition + direction * 10, time) + Vector3.up * height;

            yield return null;
        }
        if (bullet != null)
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
    }
}