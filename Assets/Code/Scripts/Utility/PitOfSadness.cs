using System;
using Code.Scripts.Managers;
using Code.Scripts.Player;
using UnityEngine;

namespace Code.Scripts.Utility
{
    public class PitOfSadness : MonoBehaviour
    {
        [SerializeField] private Transform _playerReturnLocation;
        [SerializeField] private float _timeToRespawn = 2f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                ((PlayerController)other.gameObject.GetComponent<PlayerController>()).Death();
                Invoke("Respawn", _timeToRespawn);
                
            }
        }

        private void Respawn()
        {
            PlayerSpawnManager.Instance.SpawnPlayer(_playerReturnLocation);
        }
    }
}