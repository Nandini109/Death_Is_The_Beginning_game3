using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    public PlayerController controller;
    public GameObject CoinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller != null)
        {
            Debug.LogError("MAAAAAAAAAA");
            Debug.LogError(collision.gameObject);
            if (controller.IsAttacking() && collision.gameObject.CompareTag("Enemy"))
            {
                float coinSpacing = 0.8f;
                Debug.LogError("MA CHUDAAAAAA");
                for (int i = 0; i < 3; i++)
                {
                    Vector2 coinPosition = (Vector2)collision.gameObject.transform.position + new Vector2(i * coinSpacing, 0);
                    Instantiate(CoinPrefab, coinPosition, CoinPrefab.transform.rotation);
                }

                Debug.LogError("MA CHUDA");
                Destroy(collision.gameObject);

            }
        }
    }
}
