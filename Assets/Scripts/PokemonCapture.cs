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
                if (pokemonCollider.transform.parent.gameObject.TryGetComponent(out PokemonEntity pokemonEntity))
                {
                    if (!player.OwnedPokemons.Contains(pokemonEntity.AttachedPokemon))
                    {
                        player.OwnedPokemons.Add(pokemonEntity.AttachedPokemon);
                        Destroy(pokemonCollider.transform.parent.gameObject);
                    }
                    else
                    {
                        if(player.Followers.Contains(pokemonEntity))
                        {
                            // Récupérer le dernier pokémon qui a un leader
                            PokemonEntity lastFollower = (PokemonEntity)player.Followers.FindLast(pokemon => pokemon.Leader != null);
                            pokemonEntity.Leader = lastFollower;
                            pokemonEntity.CanFollow = true;
                        }
                        else
                        {
                            Destroy(pokemonCollider.gameObject);
                        }
                    }
                    
                  
                }
            }
        }
    }
}
