using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityFlip : MonoBehaviour
{
    private float normalGravity = 1f;
    private float flippedGravity = -1f;
    private Rigidbody2D rb;

    private bool isFlipped = false;
    private PlayerInputs _playerInputs;

    [SerializeField] private float flipJumpForce = 5f;
    
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        
        _playerInputs.Enable();
        _playerInputs.PlayerActions.FlipGravity.performed += OnFlipGravity;
    }

    private void OnDisable()
    {
        
        _playerInputs.Disable();
        _playerInputs.PlayerActions.FlipGravity.performed -= OnFlipGravity; 
    }
    private void OnFlipGravity(InputAction.CallbackContext context)
    {
        Debug.Log("I'm trying to flip the player");
        //isFlipped = !isFlipped;

        if (isFlipped == false)
        {
            Debug.Log("Player Flipped");
            rb.gravityScale = flippedGravity;
            transform.localScale = new Vector3(1, -1, 1);
            rb.AddForce(new Vector2(0, flipJumpForce), ForceMode2D.Impulse);
            isFlipped = true;
        }
        else
        {
            Debug.Log("Player is back to normal");
            rb.gravityScale = normalGravity;
            transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(0, -flipJumpForce), ForceMode2D.Impulse);
            isFlipped = false;
        }
    }

    public void OnSwordAttack()
    {

    }
}
