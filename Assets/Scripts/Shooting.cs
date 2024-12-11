using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawner;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] AudioSource audioSource; 
    [SerializeField] PlayerController playerController;
    private Vector3 facingDirection = Vector3.down; 

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerController != null && playerController.isGameOver) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            facingDirection = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            facingDirection = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            facingDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            facingDirection = Vector3.right;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            UpdateBulletSpawnerPosition();
            FireSpreadBullets();

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void UpdateBulletSpawnerPosition()
    {
        if (facingDirection == Vector3.up)
        {
            bulletSpawner.localPosition = new Vector3(0, 0.3f, 0);
        }
        else if (facingDirection == Vector3.down)
        {
            bulletSpawner.localPosition = new Vector3(0, -0.3f, 0);
        }
        else if (facingDirection == Vector3.left)
        {
            bulletSpawner.localPosition = new Vector3(-0.3f, 0, 0);
        }
        else if (facingDirection == Vector3.right)
        {
            bulletSpawner.localPosition = new Vector3(0.3f, 0, 0);
        }
    }

    private void FireSpreadBullets()
    {
        float spreadAngle = 15f; 
        FireBulletAtAngle(0); 
        FireBulletAtAngle(spreadAngle); 
        FireBulletAtAngle(-spreadAngle); 
    }

    private void FireBulletAtAngle(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
        Vector3 direction = Quaternion.Euler(0, 0, angle) * facingDirection;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
}
