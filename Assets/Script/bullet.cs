using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float _speed = 100f;

    private BulletPool _sender;

    public BulletPool Sender
    {
        set => _sender = value;
    }
    
    void Update()
    {
        transform.Translate(Vector3.up * - 1 *Time.deltaTime*_speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.name == "Wall") {
            Debug.Log("hit wall");
            _sender.Pool.Release(transform.gameObject);
        }
    }
    
}
