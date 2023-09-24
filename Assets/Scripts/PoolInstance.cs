using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class PoolInstance : MonoBehaviour
    {
        public GameObject bulletPrefab;

        private ObjectPool<GameObject> _pool;

        public ObjectPool<GameObject> Pool
        {
            get
            {
                _pool ??= new ObjectPool<GameObject>(CreateBullet,OnTakeBullet,OnReturnBullet,OnBulletDestroy,true,5,5);
                return _pool;
            }
        }

        private GameObject CreateBullet()
        {
            Debug.Log("on create");
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.parent = transform;
            bulletInstance.transform.position = transform.position;
            
            bulletInstance.GetComponent<bullet>().Sender = this;
            return bulletInstance;
        }

        private void OnTakeBullet(GameObject bullet)
        {
            Debug.Log("take bullet");
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }

        private void OnReturnBullet(GameObject bullet)
        {
            Debug.Log("return bullet");
            bullet.SetActive(false);
        }

        private void OnBulletDestroy(GameObject bullet)
        {
            Destroy(bullet);
        }

    }
}
