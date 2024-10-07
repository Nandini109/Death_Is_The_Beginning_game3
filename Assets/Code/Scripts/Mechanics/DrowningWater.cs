using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningWater : MonoBehaviour
{
    public LayerMask playerLayer;
    [SerializeField] float DrownTime = 3f;
    [SerializeField] float WaterLevel = 0f;
    private Coroutine drowningCoroutine;
    private IEnumerator CheckWaterLevel(Collider2D player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        while (true)
        {
            bool isFullyInsideWater = player.bounds.min.y < WaterLevel && player.bounds.max.y < WaterLevel;

            if (isFullyInsideWater) // is the player is inside water
            {
                yield return new WaitForSeconds(DrownTime);

                if (player.bounds.min.y < WaterLevel && player.bounds.min.y < WaterLevel)
                {
                    Debug.Log("Player Drownedddd!!!");
                    playerController.Death(); //if the player is inisde water for more than 3f they die
                }
            }
            else
            {
                break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is in the water");
            drowningCoroutine = StartCoroutine(CheckWaterLevel(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (drowningCoroutine != null)
            {
                Debug.Log("Player is out od water");
                StopCoroutine(drowningCoroutine);
                drowningCoroutine = null;

            }
        }
    }
}
