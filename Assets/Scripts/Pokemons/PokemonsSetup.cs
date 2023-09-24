using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PokemonsSetup : MonoBehaviour
    {

        [SerializeField] private PokemonObject[] pokemonObjects;
        [SerializeField] private DataReader dataReader;
        
        void Awake()
        {
            foreach (PokemonObject pokemonObject in pokemonObjects)
            {
                pokemonObject.data = dataReader.GetPokemonByName(pokemonObject.name);
                
                Debug.Log("data " + pokemonObject.data.name.english);
                
            }



            if (dataReader.GetPokemonByName("LOL") == null)
            {
                Debug.Log("each");
            }
        }

    }
}
