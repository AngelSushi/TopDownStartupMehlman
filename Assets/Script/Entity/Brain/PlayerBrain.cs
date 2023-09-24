using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationWait : CustomYieldInstruction
{
    private Animation a;

    public AnimationWait(Animation a)
    {
        this.a = a;

        if (a.clip.isLooping) throw new System.Exception();

        a.Play();
    }

    public override bool keepWaiting => a.isPlaying;
}

public static class AnimationExtension
{
    public static AnimationWait PlayAndWaitCompletion(this Animation @this)
        => new AnimationWait(@this);
}



public class PlayerBrain : MonoBehaviour
{
    [SerializeField, BoxGroup("Dependencies")] private EntityMovement _movement;

    [SerializeField, BoxGroup("Dependencies")] private PlayerReference playerRef;
    
    [SerializeField] private EntityLiving leader;

    [SerializeField, BoxGroup("Input")] InputActionProperty _moveInput;

    [SerializeField, BoxGroup("Input")] private InputActionProperty moveFirst;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveSecond;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveThird;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveFourth;
    
    [SerializeField,BoxGroup("Input")] private InputActionProperty attack;

    [SerializeField, BoxGroup("Input")] private InputActionProperty capture;


    private void Start()
    {
        _moveInput.action.started += UpdateMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += StopMove;

      /*  moveFirst.action.started += SwitchFirst;
        moveSecond.action.started += SwitchSecond;
        moveThird.action.started += SwitchThird;
        moveFourth.action.started += SwitchFourth;
*/
      
        attack.action.started += OnAttack;

        capture.action.started += OnCapture;
    }
    
    private void OnDestroy()
    {
        _moveInput.action.started -= UpdateMove;
        _moveInput.action.performed -= UpdateMove;
        _moveInput.action.canceled -= StopMove;
        
      /*  moveFirst.action.started -= SwitchFirst;
        moveSecond.action.started -= SwitchSecond;
        moveThird.action.started -= SwitchThird;
        moveFourth.action.started -= SwitchFourth;
*/
      
        attack.action.started -= OnAttack;

        capture.action.started -= OnCapture;
    }

    private void OnCapture(InputAction.CallbackContext e)
    {
        
    }

    private void OnAttack(InputAction.CallbackContext e)
    {
        ((Player)playerRef.Instance).LaunchAttack(leader.HasLeader ? leader.FirstLeader : leader);
    }

    private void UpdateMove(InputAction.CallbackContext obj)
    {
        _movement.Move(obj.ReadValue<Vector2>().normalized);
    }
    private void StopMove(InputAction.CallbackContext obj)
    {
        _movement.Move(Vector2.zero);
    }

    private void SwitchFirst(InputAction.CallbackContext e) => SwitchLeader(0);
    private void SwitchSecond(InputAction.CallbackContext e) => SwitchLeader(1);
    private void SwitchThird(InputAction.CallbackContext e) => SwitchLeader(2);
    private void SwitchFourth(InputAction.CallbackContext e) => SwitchLeader(3);
    
    
    
    private void SwitchLeader(int newLeaderIndex)
    {
        
        EntityLiving newLeader = leader.Followers[newLeaderIndex];
        leader.IsMoving = false;
        leader.Leader = newLeader;
        
        
        newLeader.Followers = new List<EntityLiving>(leader.Followers);

        newLeader.Followers.RemoveAt(newLeaderIndex);
        leader.Followers.Clear();

        newLeader.Followers.Insert(newLeaderIndex,leader);
        
        for (int i = newLeader.Followers.Count - 1; i >= 0; i--)
        {
           
            if (i - 1 >= 0)
            {
                Debug.Log("entity " + newLeader.Followers[i].name +" with leader " + newLeader.Followers[i-1].name);
             //   newLeader.Followers[i].Leader = newLeader.Followers[i - 1];   
            }
            
        }

        leader = newLeader;
        newLeader.Leader = null;
      //player.Followers = order;   
        
        FindObjectOfType<CameraBinder>().Cam.Follow = leader.transform;
    }
}
