using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : Entity
    {

        public BulletPool pool;

        public Entity leader;
        
        
        void Start()
        {
            StartCoroutine(SpawnBullet());

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
    }
}
