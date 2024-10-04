using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Player;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputs _playerInputs;
    private PlayerController _playerController;
    private void OnEnable()
    {
        if (_playerController == null)
            _playerController = GetComponent<PlayerController>();
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.PlayerActions.Movement.performed +=
                (val) => _playerController.HandleMovement(val.ReadValue<Vector2>());
            _playerInputs.PlayerActions.Jump.performed += (val) => _playerController.HandleJump();
            _playerInputs.PlayerActions.Jump.canceled += (val) => _playerController.CancelJump();

        }
        _playerInputs.Enable();
    }
}
