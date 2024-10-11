using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform enemyObject;
    [SerializeField] private Transform player;

    int EnemyDirection = 1;

    [SerializeField] private float enemySightRange = 5f;
    [SerializeField] private float attackSpeed = 2f;
    
    private bool isPlayerInRange = false;
    private Vector2 initialPosition;
    private bool isPatrolling = false;

    private void Start()
    {
        initialPosition = enemyObject.position;
    }
    private void Update()
    {
        if (IsPlayerInRange())
        {
            
            FollowPlayer(); // Follow the player if in range
            isPatrolling = false; // Ensure patrol is not active
        }
        else
        {
            if (!isPatrolling)
            {
                ReturnToStart(); // Return to starting position if not in range
            }
        }
    }

    private bool IsPlayerInRange()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(enemyObject.position, player.position);
            return distanceToPlayer <= enemySightRange;
        }
        return false;
    }

    private void Patrol()
    {
        //Debug.Log("I'm on Patrol!!");
        Vector2 target = CurrentMovementTarget();

        enemyObject.position = Vector2.Lerp(enemyObject.position, target, moveSpeed * Time.deltaTime);

        float distance = (target - (Vector2)enemyObject.position).magnitude;

        if (distance <= 0.1f)
        {
            EnemyDirection *= -1;
        }
    }
    Vector2 CurrentMovementTarget()
    {
        if (EnemyDirection == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    private void FollowPlayer()
    {
        
        enemyObject.position = Vector2.MoveTowards(enemyObject.position, player.position, attackSpeed * Time.deltaTime);
    }

    private void ReturnToStart()
    {
        
        enemyObject.position = Vector2.MoveTowards(enemyObject.position, initialPosition, attackSpeed * Time.deltaTime);

        float distanceToStart = Vector2.Distance(enemyObject.position, initialPosition);
        if (distanceToStart <= 0.1f) 
        {
            //Debug.Log("Imma go back on duty");
            isPatrolling = true;
            Patrol(); 
        }
    }

  

    private void OnDrawGizmos()
    {
        if (enemyObject != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(enemyObject.transform.position, startPoint.position);
            Gizmos.DrawLine(enemyObject.transform.position, endPoint.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.IsAttacking())
            {
                Debug.Log("Player Killed Flying Enemy");
                Destroy(gameObject);  
            }
            else
            {
                playerController.Death();
                Debug.Log("Player hit by flying enemy and dies!");
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPatrolling)
        {
            Patrol(); 
        }
    }
}
