using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class PokemonsSetup : MonoBehaviour
    {

        [SerializeField] private DataReader dataReader;

        private string[] names =
        {
            "Pikachu","Miaouss","Psykokwak","Machoc","Osselait","Taupiqueur","Racaillou","Magneti","Hypnomade","Krabby",
            "Excelangue","Smogo","Voltorbe","Ectoplasma","Onix","Kicklee","Tygnon","Leveinard","Lippoutou","Scarabrute",
            "Tauros","Magicarpe","Leviator","Lokhlass","Evoli","Aquali","Voltali","Pyroli","Porygon","Amonita","Kabuto",
            "Ptera","Ronflex","Artikodin","Electhor","Sulfura","Dracolosse","Draco","Mewtwo","Mew"
        };
        
        void Awake()
        {

           foreach (Pokemon pokemon in dataReader.GetPokemons())
           {
               string path = "Assets/Scripts/Pokemons/" + pokemon.name.french + ".asset";

               if (AssetDatabase.LoadAssetAtPath<PokemonObject>(path) == null)
               {
                   PokemonObject pokemonObject = ScriptableObject.CreateInstance<PokemonObject>();

                   pokemonObject.Name = pokemon.name.french;
                   pokemonObject.Data = pokemon;
                   
                   AssetDatabase.CreateAsset(pokemonObject,path);
                   AssetDatabase.Refresh();
               }
               
           }

        }

        private void CheckIfRegister()
        {
            foreach (string name in names)
            {
                if (dataReader.GetPokemonByName(name) == null)
                {
                    Debug.Log("pokemon is not register " + name);
                }
            }
        }

        private void RemovedUnExistPokemon()
        {
            List<Pokemon> pokemons = dataReader.GetPokemons().ToList();
            List<Pokemon> removedPokemon = new List<Pokemon>();
          
            foreach (Pokemon pokemon in pokemons)
            {
                if (!names.Contains(pokemon.name.french))
                {
                    removedPokemon.Add(pokemon);
                }
            }
          
            removedPokemon.ForEach(pokemon => pokemons.Remove(pokemon));

            PokemonData pData = new PokemonData();
            pData.pokemons = pokemons.ToArray();

            string json = JsonUtility.ToJson(pData);
            System.IO.File.WriteAllText(AssetDatabase.GetAssetPath(dataReader._pokemonFile), json);
            AssetDatabase.Refresh();
        }

    }
    
}
