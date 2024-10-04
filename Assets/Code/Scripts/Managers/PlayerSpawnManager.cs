using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Managers
{
    public class PlayerSpawnManager : Singleton<PlayerSpawnManager>
    {
        [SerializeField] private Transform _defaultSpawn;

        [SerializeField] private GameObject _playerPrefab;
        protected override void Initialize()
        {
            if(_playerPrefab!= null)
                Invoke("SpawnPlayer", 1f);
        }

        private void SpawnPlayer()
        {
            Instantiate(_playerPrefab, _defaultSpawn);
        }

        public void SpawnPlayer(Transform spawnTransform)
        {
            Instantiate(_playerPrefab, spawnTransform);
        }

    }
}