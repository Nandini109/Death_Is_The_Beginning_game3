using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 D < 4 : jump
 D > 4 D < 10 : ice crystals
Every Attack -> Cooldown Move towards player
 */
public class IceEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool isCollingDown = false;
    [SerializeField] private float punchRange = 2f;
    [SerializeField] private float iceCrystalRange = 7f;
    [SerializeField] private float jumpRange = 12f;
    [SerializeField] private float CoolDownTime = 2f;
    [SerializeField] private float timeToLand = 1f;
    [SerializeField] private float health = 100f;
    [SerializeField] private float moveSpeed = 2f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking)
        {
            TrackPlayerAndAttack();
        }
    }

    private void TrackPlayerAndAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log(distanceToPlayer);

        

        if (distanceToPlayer >= 1f && distanceToPlayer <= 3f)
        {
            PunchAttack();
        }
        else if(distanceToPlayer >= 4f && distanceToPlayer <= 6f)
        {
            IceCrystalAttck();
        }
        else if(distanceToPlayer >= 8f && distanceToPlayer <= 11f)
        {
            JumpAttack();
        }
        else
        {
            
            if (!isCollingDown)
            {
                StartCoroutine(CoolDown());
            }
        }
    }

    private void PunchAttack()
    {
        if(!isCollingDown)
        {
            isAttacking = true;
            animator.SetTrigger("Punch");
            Debug.Log("Punch Coming in");

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > punchRange)
            {
                StartCoroutine(CoolDown());
            }
        }
    }

    private void JumpAttack()
    {
        if (!isCollingDown)
        {
            isAttacking = true;
            animator.SetTrigger("Jump");  
            
            StartCoroutine(JumpAttackHappen());
            Debug.Log("Jump Coming in");
        }
    }

    private IEnumerator JumpAttackHappen()
    {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = player.position;
        float timeElapsed = 0f;
        animator.SetTrigger("Fall");
        while (timeElapsed < timeToLand)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed / timeToLand);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;  
        animator.SetTrigger("Land");  
        StartCoroutine(CoolDown());
    }

    private void IceCrystalAttck()
    {
        if (!isCollingDown)
        {
            isAttacking = true;
            animator.SetTrigger("Attack2");
            StartCoroutine(CoolDown());
            Debug.Log("IceAttack Coming in");
        }
    }

    private IEnumerator CoolDown()
    {
        isCollingDown = true;
        yield return new WaitForSeconds(CoolDownTime);
        isCollingDown = false;
        isAttacking = false;
        animator.SetTrigger("CoolDown");
        Debug.Log("I am Cooling down");
    }

    private void MoveTowardsPlayer()
    {
        Debug.Log("Moving towards player");
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();


        if (playerController != null)
        {
            if (playerController.IsAttacking())
            {
                TakeDamage(2);
              
            }
            else
            {
                playerController.Death();
                Debug.Log("Player hit by ice enemy and dies!");
            }
        }
    }



}
