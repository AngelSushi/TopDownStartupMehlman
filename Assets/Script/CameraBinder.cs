using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game;
using UnityEngine;

public class CameraBinder : MonoBehaviour
{

    [SerializeField] Cinemachine.CinemachineVirtualCamera _cam;

    public CinemachineVirtualCamera Cam
    {
        get => _cam;
    }
    
    [SerializeField] PlayerReference _playerRef;

    private void Awake()
    {
        if (_playerRef.Instance != null)
        {
            SpawnPlayer(_playerRef.Instance, _playerRef.Instance);
        }
        else
        {
            _playerRef.OnValueChanged += SpawnPlayer;

        }
    }

    private void SpawnPlayer(Entity sEntity, Entity oEntity)
    {
        if (sEntity != null && sEntity is Player)
        {
            Player p = (Player)sEntity;
            p.OnControllerSwitch += UpdateCameraFollow;
            
        }
        else
        {
            if (oEntity != null && oEntity is Player)
            {
                Player p = (Player)oEntity;
                p.OnControllerSwitch -= UpdateCameraFollow;
            }
        }
    }


    private void UpdateCameraFollow(Entity obj)
    {
        _cam.Follow = obj.transform;
    }

}
