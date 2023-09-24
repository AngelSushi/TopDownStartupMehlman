using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PokemonCapture : MonoBehaviour
    {

        [SerializeField] private Player player;

        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log("enter");
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("enter02");
        }
    }
}
