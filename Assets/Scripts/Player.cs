using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Player : Entity,IMovable
    {

        [SerializeField] private InputActionReference action;
        [SerializeField] private float speed;
        
        private Rigidbody2D _rb;
        private Vector2 _direction;

        private bool _isMooving;
        
        private void Start()
        {
            action.action.started += OnMove;
            action.action.performed += OnMove;
            action.action.canceled += OnMove;

            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_isMooving)
            {
                Move();
            }
            
            Debug.DrawLine(transform.position,transform.position + transform.forward * 15,Color.yellow);
        }

        public void Move() => _rb.velocity = _direction * speed;
        public void StopMove() =>_rb.velocity = Vector2.zero;

        private void OnMove(InputAction.CallbackContext e)
        {
            Debug.Log("read value");

            if (e.started)
            {
                _isMooving = true;
            }

            if (e.performed)
            {
                _direction = e.ReadValue<Vector2>();
            }
            
            if (e.canceled)
            {
                _isMooving = false;
                Debug.Log("stop it");
                StopMove();
            }
        }
        
    }
}
