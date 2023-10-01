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
            Vector3 direction = ((Player)playerRef.Instance).FirstLeader.Direction;
            
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.parent = transform;
            bulletInstance.transform.position = transform.position;
            bulletInstance.transform.Rotate(0,0,Vector3.Angle(bulletInstance.transform.up,direction) + 90);
            
            
            return bulletInstance;
        }

        private void OnTakeBullet(GameObject bullet)
        {
            Vector3 direction = ((Player)playerRef.Instance).FirstLeader.Direction;
            bullet.transform.position = transform.position + direction * 0.5f;
            
            bullet.GetComponent<bullet>().Sender = this;
            bullet.GetComponent<bullet>().Direction = direction;
            bullet.SetActive(true);
        }

        private void OnReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
        }

        private void OnBulletDestroy(GameObject bullet)
        {
            Destroy(bullet);
        }

    }
}
