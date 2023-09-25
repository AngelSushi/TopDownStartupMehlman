
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class GeneratePokemonRoom : MonoBehaviour
    {

        [SerializeField] private RoomManager roomManager;

        [SerializeField] private List<PokemonObject> pokemons;


        private void Start()
        {
            roomManager.OnGeneratePokemon += OnGeneratePokemon;
        }

        private void OnDestroy()
        {
            roomManager.OnGeneratePokemon -= OnGeneratePokemon;
        }


        private PokemonObject OnGeneratePokemon()
        {
            List<PokemonObject> possiblePokemons = new List<PokemonObject>();
            int random = Random.Range(1, 101);

            foreach (PokemonObject pokemon in pokemons)
            {
                if (random <= pokemon.Rarity)
                {
                    for (int i = 0; i < pokemon.Rarity; i++)
                    {
                        possiblePokemons.Add(pokemon);
                    }
                }
            }

            int randomPokIndex = Random.Range(0, possiblePokemons.Count - 1);

            return possiblePokemons[randomPokIndex];
        }
    }
}
