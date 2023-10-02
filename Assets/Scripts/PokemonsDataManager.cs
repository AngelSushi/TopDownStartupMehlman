using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class PokemonsDataManager : MonoBehaviour
    {
        [SerializeField] private List<PokemonObject> allPokemons;

        [SerializeField, Expandable] public List<PokemonObject> datasPokemon;
        private void Awake()
        {
            foreach (PokemonObject pokemon in allPokemons)
            {
                PokemonObject pokemonInstance = pokemon.GetClone();
                pokemonInstance.Data.statbase.MaxHP = pokemonInstance.Data.statbase.HP;
                datasPokemon.Add(pokemonInstance);
            }
        }

        public PokemonObject GetPokemonWithName(string pokemonName) => datasPokemon.FirstOrDefault(pokemon => pokemon.Name.Equals(pokemonName));
    }
}
