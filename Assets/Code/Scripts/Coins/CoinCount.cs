using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCount : MonoBehaviour
{
    public int NumberOfCoins { get; private set; } //only this script can set the value

    public UnityEvent<CoinCount> OnCoinsCollected;
    public void CoinsCollected()
    {
        //Incrementing the number of rings collected
        NumberOfCoins++;
        OnCoinsCollected?.Invoke(this);
    }

    public void DeductCoins(int amount)
    {
        if (NumberOfCoins >= amount)
        {
            NumberOfCoins -= amount;  // Deduct the required amount of coins
        }
    }
}
