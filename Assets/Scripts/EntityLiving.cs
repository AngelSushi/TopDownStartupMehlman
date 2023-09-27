using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class EntityLiving : Entity,IMovable
    {

        protected bool isMoving;

        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }
        
        [SerializeField] protected EntityLiving leader;

        public EntityLiving Leader
        {
            get => leader;
            set => leader = value;
        }

        public EntityLiving FirstLeader
        {
            get
            {
                EntityLiving newLeader = leader;

                while (newLeader != null && newLeader.leader != null)
                {
                    newLeader = newLeader.leader;
                }

                return newLeader;
            }
        }

        public bool HasLeader
        {
            get => FirstLeader != null;
        }
        
        protected Vector2 direction;

        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        [SerializeField] private List<EntityLiving> _followers = new List<EntityLiving>();

        public List<EntityLiving> Followers
        {
            get => _followers;
            set => _followers = value;
        }

        [SerializeField] private bool canBeCaptured;

        protected Rigidbody2D rb;
        [SerializeField] protected float speed; 
        
        public event Action<EntityLiving> OnLeaderChanged;

        private Room _currentRoom;
        
        public Room CurrentRoom
        {
            get
            {
                GameObject collideRoom = null;
                
                foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(transform.position,3,1 << 3))
                {
                    collideRoom = collider2D.gameObject;
                }

                if (collideRoom != null)
                {
                    _currentRoom = RoomManager.GetRoomByGameObject(collideRoom);
                    return _currentRoom;
                }
                else
                    return null;
                
                
            }
            protected set => _currentRoom =value;
        }


        public virtual void Start()
        { 
            AttachFollowers();
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Update()
        {
            if ((this is Player || canBeCaptured) && (isMoving || (HasLeader && FirstLeader.isMoving)))
            {
                Move();
            }
            
            
            Debug.Log("Room " + CurrentRoom?.RoomGO + " called from " + gameObject.name);
            
        }

        public virtual void Move()
        {
            if (HasLeader)
            {
                if (FirstLeader is Player)
                {
                    transform.position = (leader.transform.position - (Vector3)FirstLeader.direction * 1.5f);
                }
            }
            else
            {
                rb.velocity = direction * speed;
            }
            
        }

        public virtual void StopMove() { }
        
        

        private void AttachFollowers()
        {
            if (HasLeader && !FirstLeader.Followers.Contains(this))
            {
                FirstLeader.Followers.Add(this);
            }    
        }
        
        protected void OnMove(InputAction.CallbackContext e)
        {
            if (HasLeader)
            {
                if (e.started)
                {
                    FirstLeader.IsMoving = true;
                }

                if (e.performed)
                {
                    FirstLeader.Direction = e.ReadValue<Vector2>();
                }
            
                if (e.canceled)
                {
                    FirstLeader.IsMoving = false;
                    FirstLeader.StopMove();
                }
            }
            else
            {
                if (e.started)
                {
                    IsMoving = true;
                }

                if (e.performed)
                {
                    Direction = e.ReadValue<Vector2>();
                }
            
                if (e.canceled)
                {
                    IsMoving = false;
                    StopMove();
                }
            }
        }

        public void CallOnLeaderChanged(EntityLiving newLeader)
        {
            Debug.Log("call from " + gameObject.name);
            
            OnLeaderChanged?.Invoke(newLeader);
        }
    }
}
