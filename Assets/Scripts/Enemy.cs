using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : EntityLiving
    {

        public PoolInstance pool;
        


        public override void Start()
        {
            base.Start();
          //  StartCoroutine(SpawnBullet());

            if (leader != null)
            {

                if (transform.parent != leader.transform)
                {
                    transform.parent = leader.transform;
                    transform.position = leader.transform.position - leader.transform.up * -1.5f;
                }
            }
        }


        private IEnumerator SpawnBullet()
        {
            yield return new WaitForSeconds(0.5f);
            
            pool?.Pool.Get();

            StartCoroutine(SpawnBullet());
        }

        public override void Move()
        {
            base.Move();
        }

        public override void StopMove()
        {
            base.StopMove();
        }
    }
}
