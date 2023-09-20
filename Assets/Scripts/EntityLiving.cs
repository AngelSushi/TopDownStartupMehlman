using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        public EntityLiving leader;


        private EntityLiving FirstLeader
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

        private bool HasLeader
        {
            get => FirstLeader != null;
        }
        
        
        protected Vector2 direction;
        
        

        public virtual void Update()
        {
            if (isMoving || (HasLeader && FirstLeader.isMoving))
            {
                Move();
            }
        }

        public virtual void Move()
        {
            if (HasLeader)
            {
                transform.position = (leader.transform.position - (Vector3)FirstLeader.direction * 1.5f);
            }
            
        }

        public virtual void StopMove() { }
    }
}
