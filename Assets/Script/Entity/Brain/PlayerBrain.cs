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

    [SerializeField, BoxGroup("Dependencies")] private PlayerReference playerRef;
    
    [SerializeField] private EntityLiving leader;

    [SerializeField, BoxGroup("Input")] private InputActionProperty moveFirst;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveSecond;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveThird;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveFourth;
    
    [SerializeField,BoxGroup("Input")] private InputActionProperty attack;

    [SerializeField, BoxGroup("Input")] private InputActionProperty capture;
    [SerializeField, BoxGroup("Input")] private InputActionProperty push;

    [SerializeField, BoxGroup("Input")] private InputActionProperty menu;

    [SerializeField, BoxGroup("Input")] private InputActionProperty move;

    private EntityLiving _currentController;

    private void Start()
    { 
        moveFirst.action.started += SwitchFirst;
        moveSecond.action.started += SwitchSecond;
        moveThird.action.started += SwitchThird;
        moveFourth.action.started += SwitchFourth;
        attack.action.started += OnAttack;
        capture.action.started += OnCapture;
        push.action.started += OnPush;
        push.action.performed += OnPushPerf;
        menu.action.started += OnOpenInventory;
        
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;
    }

   

    private void OnDestroy()
    {
        moveFirst.action.started -= SwitchFirst;
        moveSecond.action.started -= SwitchSecond;
        moveThird.action.started -= SwitchThird;
        moveFourth.action.started -= SwitchFourth;
        attack.action.started -= OnAttack;
        capture.action.started -= OnCapture;
        push.action.started -= OnPush;
        push.action.performed -= OnPushPerf;
        menu.action.started -= OnOpenInventory;
        
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;
    }
    
    private void OnOpenInventory(InputAction.CallbackContext obj)
    {
        ((Player)playerRef.Instance).ManageInventory();
    }
    

    private void OnCapture(InputAction.CallbackContext e)
    {
        if (((Player)playerRef.Instance).IsInInventory)
        {
            return;
        }
        
        ((Player)playerRef.Instance).CapturePokemon();
    }

    private void OnAttack(InputAction.CallbackContext e)
    {
        if (((Player)playerRef.Instance).IsInInventory)
        {
            return;
        }
        
      //  ((Player)playerRef.Instance).LaunchAttack(leader.HasLeader ? leader.FirstLeader : leader);
    }

    private void OnPush(InputAction.CallbackContext e)
    {

        if (((Player)playerRef.Instance).IsInInventory)
        {
            return;
        }
        
        Collider2D boxCollider = Physics2D.OverlapCircle(transform.position, 1,1 << 6);
        
        if (boxCollider != null && boxCollider.TryGetComponent(out PushableBloc pushableBloc))
        {
            pushableBloc.PushBlock(((Player)playerRef.Instance).Direction);
        }
    }

    private void OnPushPerf(InputAction.CallbackContext e)
    {
        Debug.Log("perff");
    }

    private void SwitchFirst(InputAction.CallbackContext e)
    {
        Debug.Log("switch first");
        SwitchLeader(0);
    } 
    
    private void SwitchSecond(InputAction.CallbackContext e) => SwitchLeader(1);
    private void SwitchThird(InputAction.CallbackContext e) => SwitchLeader(2);
    private void SwitchFourth(InputAction.CallbackContext e) => SwitchLeader(3);
    
    
    
    private void SwitchLeader(int newLeaderIndex)
    {
        if (((Player)playerRef.Instance).IsInInventory)
        {
            return;
        }
        
        
      /*  Debug.Log("switch");
        
        if (((Player)playerRef.Instance).IsInInventory)
        {
            return;
        }
        
        
        
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
              //  newLeader.Followers[i].Leader = newLeader.Followers[i - 1];   
            }
            
        }

        leader = newLeader;
     //   newLeader.Leader = null;
      //player.Followers = order;   
        
        FindObjectOfType<CameraBinder>().Cam.Follow = leader.transform; // A ENLEVER AIE AIE AIE 
        
        */
    }
    
    protected void OnMove(InputAction.CallbackContext e)
    {
        Player p = (Player)playerRef.Instance;

        if (p.IsInInventory)
        {
            return;
        }


        if (e.started)
        {
            _currentController.IsMoving = true;
        }

        if (e.canceled)
        {
            _currentController.IsMoving = false;
        }
        
        
        
       /* if (p.HasLeader)
        {
            if (e.started)
            {
                p.FirstLeader.IsMoving = true;
            }

            if (e.performed)
            {
                p.FirstLeader.Direction = e.ReadValue<Vector2>();
            }
            
            if (e.canceled)
            {
                p.FirstLeader.IsMoving = false;
                p.FirstLeader.StopMove();
            }
        }
        else
        {
            if (e.started)
            {
                p.IsMoving = true;
            }

            if (e.performed)
            {
                p.Direction = e.ReadValue<Vector2>();
            }
            
            if (e.canceled)
            {
                p.IsMoving = false;
                p.StopMove();
            }
        }
        */
    }
}
