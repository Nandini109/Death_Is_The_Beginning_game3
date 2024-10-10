using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningWater : MonoBehaviour
{
    public LayerMask playerLayer;
    [SerializeField] private float drownTime = 3f;
    [SerializeField] private float waterLevel = 0f;
    [SerializeField] private float speedInWater = 0.5f;
    private Coroutine drowningCoroutine;
    private float originalSpeed;
    private IEnumerator CheckWaterLevel(Collider2D player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        originalSpeed = playerController.Data.MovementSpeed;
        playerController.Data.MovementSpeed *= speedInWater;
        while (true)
        {

            bool isFullyInsideWater = player.bounds.min.y < waterLevel && player.bounds.max.y < waterLevel;

            if (isFullyInsideWater) // is the player is inside water
            {
                yield return new WaitForSeconds(drownTime);

                if (player.bounds.min.y < waterLevel && player.bounds.min.y < waterLevel)
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
            //Change the speed back to original
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Data.MovementSpeed = originalSpeed;
            Debug.Log(originalSpeed);
        }
    }
}
