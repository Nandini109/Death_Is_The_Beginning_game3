using System;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private float _groundCheckDistance = 0.5f;
        [SerializeField] private float _groundCheckDelay = .01f;
        [SerializeField] private Vector3 _colliderOffset;
        [SerializeField] private LayerMask _groundLayer;
        public event Action<bool> GroundChanged;
        private bool _isGrounded = false;
        private float _timeSinceDelay;

        private PlayerController _playerController;
        public bool IsGrounded
        {
            get
            {
                return _isGrounded;
            }
            private set
            {
                if (value != _isGrounded)
                {
                    _isGrounded = value;
                    GroundChanged?.Invoke(_isGrounded);
                }
            }
        }

        public void DelayGrounding()
        {
            _timeSinceDelay = Time.time;
            IsGrounded = false;
        }
        void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        void Update()
        {
            if (Time.time - _timeSinceDelay < _groundCheckDelay)
                return;

            Vector2 groundCheckDirection = _playerController.Data.IsFlipped ? Vector2.up : Vector2.down;
            //IsGrounded = Physics2D.Raycast(transform.position + _colliderOffset, Vector2.down, _groundCheckDistance, _groundLayer) || Physics2D.Raycast(transform.position - _colliderOffset, Vector2.down, _groundCheckDistance, _groundLayer);
            IsGrounded = Physics2D.Raycast(transform.position + _colliderOffset, groundCheckDirection, _groundCheckDistance, _groundLayer) ||
                         Physics2D.Raycast(transform.position - _colliderOffset, groundCheckDirection, _groundCheckDistance, _groundLayer);
        }
    }
}