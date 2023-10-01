using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private PoolInstance _sender;

    public PoolInstance Sender
    {
        set => _sender = value;
    }

    private Rigidbody2D _rb;

    private Vector2 _direction;

    public Vector2 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private CircleCollider2D _collider2D;

    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,Vector2.SignedAngle(Vector3.right,_direction));
  
        _rb.velocity = _direction * speed;
    
        if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), _collider2D.bounds))
        {
            _sender.Pool.Release(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                _sender.Pool.Release(transform.gameObject);
                break;
            
            case "Ice":
                Destroy(collision.gameObject);
                _sender.Pool.Release(transform.gameObject);
                break;
            
            case "Torch":
                OnOff onOff = collision.gameObject.GetComponent<OnOff>();
                collision.gameObject.GetComponent<SpriteRenderer>().sprite = onOff.OnSprite;
                onOff.Actualize(((Player)_sender.PlayerRef.Instance).CurrentRoom,collision.gameObject.transform.localPosition,true);
                break;
        }
    }
    
}
