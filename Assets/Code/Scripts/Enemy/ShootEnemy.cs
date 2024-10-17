using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;     
    [SerializeField] private GameObject bulletPrefab;  
    [SerializeField] private Transform firePoint;   
    [SerializeField] private float fireRate = 2f;   
    [SerializeField] private GameObject CoinPrefab;
    private void Start()
    {
        
        StartCoroutine(ShootAtPlayer());
    }

    private void Update()
    {
       
        
    }

    

    private IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * 10f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {                     
        playerController.Death();
        Debug.Log("Player hit by patrol enemy and dies!");
            
        }
    }
}
