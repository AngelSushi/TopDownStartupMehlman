using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class PoolInstance : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private PlayerReference playerRef;

        public PlayerReference PlayerRef
        {
            get => playerRef;
            set => playerRef = value;
        }

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
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.parent = transform;
            bulletInstance.transform.position = transform.position;    
            return bulletInstance;
        }

        private void OnTakeBullet(GameObject bullet)
        {
            bullet.GetComponent<bullet>().Sender = this;
            bullet.SetActive(true);
        }

        private void OnReturnBullet(GameObject bullet) => bullet.SetActive(false);    
        private void OnBulletDestroy(GameObject bullet) => Destroy(bullet);     
    }
}
