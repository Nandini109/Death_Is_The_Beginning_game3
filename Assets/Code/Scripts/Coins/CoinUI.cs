using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinUI : MonoBehaviour
{
    private TextMeshProUGUI coinsCountText;

    void Start()
    {
        coinsCountText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinCount(CoinCount ringsCount)
    {
       
        coinsCountText.text = ringsCount.NumberOfCoins.ToString();
    }
}
