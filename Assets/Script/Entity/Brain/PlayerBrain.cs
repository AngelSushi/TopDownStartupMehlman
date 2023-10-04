using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

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
    private Player _player;
    
    [SerializeField] private EntityLiving leader;

    [SerializeField, BoxGroup("Input")] private InputActionProperty moveFirst;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveSecond;
    [SerializeField, BoxGroup("Input")] private InputActionProperty moveThird;
    
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
        attack.action.started += OnAttack;
        capture.action.started += OnCapture;
        push.action.started += OnPush;
        push.action.performed += OnPushPerf;
        menu.action.started += OnOpenInventory;
        
        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        _currentController = (EntityLiving)playerRef.Instance;
        _player = (Player)playerRef.Instance;
    }

   

    private void OnDestroy()
    {
        moveFirst.action.started -= SwitchFirst;
        moveSecond.action.started -= SwitchSecond;
        moveThird.action.started -= SwitchThird;
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
        _player.ManageInventory();
    }
    

    private void OnCapture(InputAction.CallbackContext e)
    {
        if (_player.IsInInventory)
        {
            return;
        }
        
        ((Player)playerRef.Instance).CapturePokemon();
    }

    private void OnAttack(InputAction.CallbackContext e)
    {
        if (_player.IsInInventory)
        {
            return;
        }
        
        _player.LaunchAttack(_currentController);
    }

    private void OnPush(InputAction.CallbackContext e)
    {
        if (_player.IsInInventory)
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

    private void SwitchFirst(InputAction.CallbackContext e) => SwitchLeader(0);
    private void SwitchSecond(InputAction.CallbackContext e) => SwitchLeader(1);
    private void SwitchThird(InputAction.CallbackContext e) => SwitchLeader(2);
    
    
    
    private void SwitchLeader(int newLeaderIndex)
    {
        if (_player.IsInInventory)
        {
            return;
        }
        
        if(_currentController is Player)
        {
            Player playerController = (Player)_currentController;

            if(newLeaderIndex < playerController.Followers.Count)
            {
                _currentController = playerController.Followers[newLeaderIndex];

                foreach(EntityLiving controller in playerController.Followers)
                {
                    if(controller is PokemonEntity)
                    {
                        PokemonEntity pokemonController = (PokemonEntity)controller;
                        pokemonController.CanFollow = false;                 
                    }
                }
            }
        }
        else
        {
            _currentController.Leader = null;            
                        
            foreach(EntityLiving entityLiving in _player.Followers)
            {
                if(entityLiving is PokemonEntity)
                {
                    PokemonEntity pokemonEntity = (PokemonEntity)entityLiving;

                    if(pokemonEntity != _currentController && pokemonEntity.Leader != null)
                    {
                        pokemonEntity.CanFollow = true;
                    }
                }
            }
            EntityLiving entity = _player.Followers.FirstOrDefault(entityLiving => entityLiving.Leader == _currentController);
            int entityIndex = _player.Followers.IndexOf(_currentController);         

            if(entity != null)
            {
                if (entityIndex == 0)
                {
                    entity.Leader = _player;
                }
                else
                {
                    if (entityIndex < _player.Followers.Count - 1)
                    {
                        EntityLiving targetEntity = (EntityLiving)_player.Followers[entityIndex - 1];

                        if (((PokemonEntity)targetEntity).CanFollow)
                        {
                            entity.Leader = _player.Followers[entityIndex - 1];
                        }
                        else
                        {
                            targetEntity = _player;
                            for(int i = entityIndex;i > 0; i--)
                            {
                                if(((PokemonEntity)_player.Followers[i]).CanFollow)
                                {
                                    targetEntity = _player.Followers[i];
                                    break;
                                }
                            }

                            entity.Leader = targetEntity;
                        }
                    }
                    else
                    {
                        entity.Leader = null;
                    }
                }
            }

            
            _currentController = (EntityLiving)playerRef.Instance;
            
        }

        _player.SwitchController(_currentController);
    }
    
    protected void OnMove(InputAction.CallbackContext e)
    {     
        if (_player.IsInInventory)
        {
            return;
        }

        if (e.started)
        {
            _currentController.IsMoving = true;
            _currentController.Direction = e.ReadValue<Vector2>();
        }

        if(e.performed)
        {
            _currentController.Direction = e.ReadValue<Vector2>();
        }
        

        if(_currentController is Player)
        {
            Player playerController = (Player)_currentController;

            foreach(EntityLiving follower in playerController.Followers)
            {
                follower.Direction = _currentController.Direction;
            }
        }

        if (e.canceled)
        {
            _currentController.IsMoving = false;
        }
        
    }
}
