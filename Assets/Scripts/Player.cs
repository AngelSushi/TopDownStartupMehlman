using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Player : EntityLiving
    {

        [SerializeField] private InputActionReference action;
        [SerializeField] private float speed; 
        private Rigidbody2D _rb;

        [SerializeField] private DataReader data;
        
        public event Action<Pokemon> OnCapturePokemon;

        private void Start()
        {
            action.action.started += OnMove;
            action.action.performed += OnMove;
            action.action.canceled += OnMove;

            _rb = GetComponent<Rigidbody2D>();

            Debug.Log("data " + data);
            Debug.Log("pok " + data.GetPokemonById(100));
            
            OnCapturePokemon?.Invoke(data.GetPokemonById(100));
        }

        public override void Update()
        {
            base.Update();
            
        }

        public override void Move() => _rb.velocity = direction * speed;
        public override void StopMove() =>_rb.velocity = Vector2.zero;

        private void OnMove(InputAction.CallbackContext e)
        {
            if (e.started)
            {
                isMoving = true;
            }

            if (e.performed)
            {
                direction = e.ReadValue<Vector2>();
            }
            
            if (e.canceled)
            {
                isMoving = false;
                StopMove();
            }
        }
        
    }
}
