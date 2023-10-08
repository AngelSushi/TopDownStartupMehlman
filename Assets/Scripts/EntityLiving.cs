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
      
        protected Vector2 direction;

        public Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        [SerializeField] private bool canBeCaptured;

        protected Rigidbody2D rb;
        
        // 5 , 7 , 9 , 11
        [SerializeField] private float initialSpeed;
        private Alterable<float> _speed;

        public Alterable<float> Speed
        {
            get => _speed;
            set => _speed = value;
        }

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

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Speed = new Alterable<float>(initialSpeed);
        }

        public virtual void Update()
        {
            if(isMoving)
            {
                Move();
            }
        }

        public virtual void Move()
        {
            Debug.Log("speed " + _speed.CalculateValue());
            rb.velocity = direction * _speed.CalculateValue();          
        }

        public virtual void StopMove() {
        }

        private void OnTriggerEnter2D(Collider2D col)
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

        private void OnTriggerExit2D(Collider2D col)
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
