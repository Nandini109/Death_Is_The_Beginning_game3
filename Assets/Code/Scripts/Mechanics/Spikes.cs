using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Spikesss");
            playerController.Death();
        }
    }
}
