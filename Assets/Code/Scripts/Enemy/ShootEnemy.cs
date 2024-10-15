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
        Debug.Log(angle);

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
}
