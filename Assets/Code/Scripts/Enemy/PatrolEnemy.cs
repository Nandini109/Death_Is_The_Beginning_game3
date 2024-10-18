using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform enemyObject;
    [SerializeField] private GameObject CoinPrefab;

    int EnemyDirection = 1;

    private void Update()
    {
        Vector2 target = CurrentMovementTarget();

        enemyObject.position = Vector2.Lerp(enemyObject.position, target, moveSpeed * Time.deltaTime);

        float distance = (target - (Vector2)enemyObject.position).magnitude;

        if( distance <= 0.1f)
        {
            EnemyDirection *= -1;
        }
    }
    Vector2 CurrentMovementTarget()
    {
        if(EnemyDirection == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }
    private void OnDrawGizmos()
    {
        if(enemyObject != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(enemyObject.transform.position, startPoint.position);   
            Gizmos.DrawLine(enemyObject.transform.position, endPoint.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.Death();
            Debug.Log("Player hit by patrol enemy and dies!");
            
        }
    }
}
