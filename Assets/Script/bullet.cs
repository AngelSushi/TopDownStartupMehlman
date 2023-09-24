using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float _speed = 100f;

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
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _rb.velocity = _direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.name == "Wall") {
            Debug.Log("hit wall");
            _sender.Pool.Release(transform.gameObject);
        }
    }
    
}
