using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        CoinCount coinCount = other.GetComponent<CoinCount>();

        if (coinCount != null)
        {
            //collecting rings and making them disappear
            coinCount.CoinsCollected();
            gameObject.SetActive(false);


        }
    }
}
