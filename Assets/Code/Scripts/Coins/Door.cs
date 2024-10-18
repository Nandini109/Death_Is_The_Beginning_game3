using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int coinsRequired = 10;
    [SerializeField] private GameObject uiPanel;  
    private bool isPlayerNearby = false;
    private CoinCount coinCount;
    private void Start()
    {
        coinCount = GetComponent<CoinCount>();
        uiPanel.SetActive(false); 
    }

    private void Update()
    {
        if (isPlayerNearby)  
        {
            if (coinCount.NumberOfCoins >= coinsRequired)
            {
                uiPanel.SetActive(true);  
            }
        }
    }

    public void OpenDoor()
    {
        if (coinCount.NumberOfCoins >= coinsRequired)
        {
            coinCount.DeductCoins(coinsRequired);  
                                                   
            Destroy(gameObject);  
            uiPanel.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Found");
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            uiPanel.SetActive(false);  
        }
    }
}
