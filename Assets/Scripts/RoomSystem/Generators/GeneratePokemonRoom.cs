
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
            
            Debug.Log("pokemon " + manager.GetPokemonWithName("Pikachu") + " rarity " + manager.GetPokemonWithName("Pikachu").Data.rarity);
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
                
                Debug.Log("rarity " + pokemon.Data.rarity);
                
                if (random <= pokemon.Data.rarity)
                {
                    for (int i = 0; i < pokemon.Data.rarity; i++)
                    {
                        possiblePokemons.Add(pokemon);
                    }
                }
            }

            int randomPokIndex = Random.Range(0, possiblePokemons.Count - 1);
            
            Debug.Log("randomPok " + randomPokIndex  + " count " + possiblePokemons.Count);

            return randomPokIndex < possiblePokemons.Count ? possiblePokemons[randomPokIndex] : null;
        }
    }
}
