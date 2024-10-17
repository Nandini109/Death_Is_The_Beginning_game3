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
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private GameObject CoinPrefab;
    private void Start()
    {
        
        StartCoroutine(ShootAtPlayer());
    }

    private void Update()
    {
       
        RotateAroundPivot();
    }

    private void RotateAroundPivot()
    {

        Vector2 direction = player.position - pivotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        firePoint.rotation = Quaternion.Lerp(firePoint.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
            if (playerController.IsAttacking())
            {

                Debug.Log("Patrol enemy defeated by player!");
                Destroy(gameObject);
                float coinSpacing = 0.8f;
                for (int i = 0; i < 6; i++)
                {
                    Vector2 coinPosition = (Vector2)gameObject.transform.position + new Vector2(i * coinSpacing, 0);
                    Instantiate(CoinPrefab, coinPosition, CoinPrefab.transform.rotation);
                }
            }
            else
            {
                playerController.Death();
                Debug.Log("Player hit by patrol enemy and dies!");
            }
        }
    }
}
