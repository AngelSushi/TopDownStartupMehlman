using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PushableBloc : MonoBehaviour
    {

        private Rigidbody2D rb;

        [SerializeField] private float pushForce;

        private void Start() => rb = GetComponent<Rigidbody2D>();

        public void PushBlock(Vector3 direction)
        {
            rb.AddForce(direction * pushForce,ForceMode2D.Impulse);
        }
    }
}
