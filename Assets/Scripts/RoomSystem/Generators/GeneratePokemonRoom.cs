
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


        [SerializeField] private PokemonsDataManager manager;
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
                PokemonObject newRef = manager.GetPokemonWithName(pokemon.Name);
                
                if (random <= newRef.Rarity.CalculateValue())
                {
                    for (int i = 0; i < newRef.Rarity.CalculateValue(); i++)
                    {
                        possiblePokemons.Add(newRef);
                    }
                }
            }

            int randomPokIndex = Random.Range(0, possiblePokemons.Count - 1);

            return randomPokIndex < possiblePokemons.Count ? possiblePokemons[randomPokIndex] : null;
        }
    }
}
