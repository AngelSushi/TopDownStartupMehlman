using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class BulletPool : MonoBehaviour
    {
        public GameObject bulletPrefab;

        private ObjectPool<GameObject> _pool;

        public ObjectPool<GameObject> Pool
        {
            get
            {
                _pool ??= new ObjectPool<GameObject>(CreateBullet,OnTakeBullet,OnReturnBullet,OnBulletDestroy,true,10,20);
                return _pool;
            }
        }

        private GameObject CreateBullet()
        {
            GameObject bulletInstance = Instantiate(bulletPrefab,transform);
            bulletInstance.transform.position = transform.position;
            return bulletInstance;
        }

        private void OnTakeBullet(GameObject bullet)
        {
            bullet.SetActive(true);
        }

        private void OnReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
        }

        private void OnBulletDestroy(GameObject bullet)
        {
            Debug.Log("destroy object");
            Destroy(bullet);
        }

    }
}
