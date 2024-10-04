using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerEventData", menuName = "SOs/PlayerEventData", order = 0)]
    public class PlayerEventData : ScriptableObject
    {
        public UnityEvent<PlayerController> PlayerSpawns, PlayerIdles, PlayerRuns, PlayerJumps, PlayerInAir;
        public UnityEvent<PlayerController> PlayerDeath, PlayerUpdate;
        public void HandlePlayerSpawn(PlayerController player) => PlayerSpawns?.Invoke(player);
        public void HandlePlayerIdles(PlayerController player) => PlayerIdles?.Invoke(player);
        public void HandlePlayerRuns(PlayerController player) => PlayerJumps?.Invoke(player);
        public void HandlePlayerInAir(PlayerController player) => PlayerInAir?.Invoke(player);
        public void HandlePlayerJumps(PlayerController player) => PlayerJumps?.Invoke(player);
        public void HandlePlayerDeath(PlayerController player) => PlayerDeath?.Invoke(player);
        public void HandlePlayerUpdate(PlayerController player)=> PlayerUpdate?.Invoke(player);
    }
}