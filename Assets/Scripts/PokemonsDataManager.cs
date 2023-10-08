using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class PokemonsDataManager : MonoBehaviour
    {
        [SerializeField] private List<PokemonObject> allPokemons;

        [SerializeField, Expandable] public List<PokemonObject> datasPokemon;
        [SerializeField] private DataReader reader;
        private void Awake()
        {
            foreach (PokemonObject pokemon in allPokemons)
            {
                PokemonObject pokemonInstance = pokemon.GetClone();
                pokemonInstance.Data = reader.GetPokemonByName(pokemon.Name);
                pokemonInstance.Rarity = new Alterable<int>(pokemonInstance.Data.rarity);

                datasPokemon.Add(pokemonInstance);
            }
            
            Debug.Log("finish generate clone");
        }

        public PokemonObject GetPokemonWithName(string pokemonName) => datasPokemon.FirstOrDefault(pokemon => pokemon.Name.ToLower().Equals(pokemonName.ToLower()));
    }
}
