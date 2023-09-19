using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {

        public BulletPool pool;
        
        
        void Start()
        {
            StartCoroutine(SpawnBullet());
        }

        private IEnumerator SpawnBullet()
        {
            
            pool.Pool.Dispose();
            yield return new WaitForSeconds(5f);
            
            pool.Pool.Get();

            StartCoroutine(SpawnBullet());
        }
    }
}
