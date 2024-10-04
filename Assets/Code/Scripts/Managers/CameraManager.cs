using Cinemachine;
using Code.Scripts.Player;
using UnityEngine;
using static Cinemachine.CinemachineCore;

namespace Code.Scripts.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private PlayerEventData playerEventData;
        protected override void Initialize()
        {
            
            playerEventData.PlayerSpawns.AddListener((player) =>
            {
                CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera.Follow = player.transform;
            });
        }
    }
}