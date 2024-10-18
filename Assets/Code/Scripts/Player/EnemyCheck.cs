using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    public PlayerController controller;
    public GameObject CoinPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller != null)
        {
            Debug.LogError(collision.gameObject);
            if (controller.IsAttacking() && collision.gameObject.CompareTag("Enemy"))
            {
                float coinSpacing = 0.8f; 
                for (int i = 0; i < 3; i++)
                {
                    Vector2 coinPosition = (Vector2)collision.gameObject.transform.position + new Vector2(i * coinSpacing, 0);
                    Instantiate(CoinPrefab, coinPosition, CoinPrefab.transform.rotation);
                }
                Destroy(collision.gameObject);

            }
        }
    }
}
