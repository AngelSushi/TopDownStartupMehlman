using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class EntityLiving : Entity,IMovable
    {

        [SerializeField, BoxGroup("Dependencies")] protected RoomManagerRef roomManager;
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

        private Room _currentRoom;
        
        public  Room CurrentRoom
        {
            get
            {
                GameObject collideRoom = null;
                
                foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(transform.position,8,1 << 3))
                {
                    collideRoom = collider2D.gameObject;
                }
                
                if (collideRoom != null)
                {
                    _currentRoom = roomManager.Instance.GetRoomByGameObject(collideRoom);
                    return _currentRoom;
                }
                
                return null;
                
                
            }
        }

        private Bloc _currentBloc;

        public Bloc CurrentBloc
        {
            get
            {
                _currentBloc = CurrentRoom.Blocs.FirstOrDefault(bloc =>
                {
                    Vector3 worldBlocPos = CurrentRoom.RoomGO.transform.TransformPoint(bloc.LocalPosition);
                    
                    if (transform.position.x >= worldBlocPos.x - 0.5f && transform.position.x <= worldBlocPos.x + 0.5f)
                    {
                        if (transform.position.y >= worldBlocPos.y - 0.5f && transform.position.y <= worldBlocPos.y + 0.5f)
                        {
                            return true;
                        }
                    }

                    return false;
                });

                return _currentBloc;
            }
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!HasLeader)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("PressObject"))
                {
                    if (col.gameObject.TryGetComponent(out OnOff onOff))
                    {
                        Debug.Log("on");
                        onOff.Actualize(CurrentRoom,col.gameObject.transform.localPosition,true);
                        onOff.TurnSprite();
                    }
                    
                    
                }
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!HasLeader)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("PressObject"))
                {
                    if (col.gameObject.TryGetComponent(out OnOff onOff))
                    {
                        Debug.Log("off");
                        onOff.Actualize(CurrentRoom,col.gameObject.transform.localPosition,false);
                        onOff.TurnSprite();
                    }
                    
                    
                }
            }
        }
    }
}
