using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class PokemonCapture : MonoBehaviour
    {

        [SerializeField,BoxGroup("Dependencies")] private Player player;

        private void Start() => player.OnCapturePokemon += OnCapturePokemon;
        private void OnDestroy() => player.OnCapturePokemon -= OnCapturePokemon;
        
        
        /*
         *
         * Si mes followers < 3 => ajouter a followers
         * 
         */
        

        private void OnCapturePokemon()
        {
            foreach (Collider2D pokemonCollider in Physics2D.OverlapCircleAll(transform.position, 1, 1 << 8))
            {
                Debug.Log("collide with pokemon");

                if (pokemonCollider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    if (!player.OwnedPokemons.Contains(enemy.AttachedPokemon))
                    {
                        player.OwnedPokemons.Add(enemy.AttachedPokemon);
                    }
                    
                    Destroy(pokemonCollider.gameObject);   
                }
            }
        }
    }
}
