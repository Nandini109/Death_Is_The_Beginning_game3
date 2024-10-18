using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCount : MonoBehaviour
{
    public int NumberOfCoins { get; private set; } 

    public UnityEvent<CoinCount> OnCoinsCollected;
    public void CoinsCollected()
    {
        
        NumberOfCoins++;
        OnCoinsCollected?.Invoke(this);
    }

    public void DeductCoins(int amount)
    {
        if (NumberOfCoins >= amount)
        {
            NumberOfCoins -= amount;  
        }
    }
}
