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

    void Start()
    {
        UpdateCameraFollow(_playerRef.Instance,_playerRef.Instance);

        _playerRef.OnValueChanged += UpdateCameraFollow;

        Player p = (Player)_playerRef.Instance;
        p.OnLeaderChanged += OnLeaderChanged;
    }

    private void OnDestroy()
    {
        _playerRef.OnValueChanged -= UpdateCameraFollow;
        
        Player p = (Player)_playerRef.Instance;
        p.OnLeaderChanged -= OnLeaderChanged;
        
        Debug.Log("leave");
    }

    private void UpdateCameraFollow(Entity obj,Entity last)
    {
        _cam.Follow = obj.transform;
    }

    private void OnLeaderChanged(EntityLiving newLeader)
    { 
        //Debug.Log("new focus on " + newLeader.name);
       // _cam.Follow = newLeader.transform;
    }
}
