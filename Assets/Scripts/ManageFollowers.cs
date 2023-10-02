using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class ManageFollowers : MonoBehaviour
    {
        [SerializeField,BoxGroup("Dependencies")] private PlayerReference playerRef;


        [SerializeField] private GameObject pokemonEmptyPrefab;
        [SerializeField] private GameObject pokemonFirePrefab;
        [SerializeField] private GameObject pokemonWaterPrefab;
        [SerializeField] private GameObject pokemonGrassPrefab;
        
        [SerializeField] private GameObject[] followersPokemons = new GameObject[3];


        public void UpdateFollowersObject(List<PokemonObject> followers)
        {
            Player p = (Player)playerRef.Instance;
            p.Followers.Clear();

            for (int i = 0; i < followers.Count; i++)
            {
                GameObject pokemon = pokemonEmptyPrefab;
                PokemonObject targetPokemon = followers[i];

                if (targetPokemon.Data.type.Contains("Grass"))
                {
                    pokemon = pokemonGrassPrefab;
                }

                if (targetPokemon.Data.type.Contains("Fire"))
                {
                    pokemon = pokemonFirePrefab;
                }

                if (targetPokemon.Data.type.Contains("Water"))
                {
                    pokemon = pokemonWaterPrefab;
                }

                GameObject pokemonInstance = Instantiate(pokemon);
                pokemonInstance.GetComponentInChildren<SpriteRenderer>().sprite = targetPokemon.Sprite;
                PokemonEntity enemy = pokemonInstance.GetComponent<PokemonEntity>();
                enemy.AttachedPokemon = followers[i];
                enemy.Leader = i == 0 ? p : followersPokemons[i - 1].GetComponent<EntityLiving>();
                pokemonInstance.name = targetPokemon.Name;
                enemy.GetComponent<Health>().CurrentHealth = targetPokemon.Data.statbase.HP;
                
                pokemonInstance.transform.position = (enemy.Leader.transform.position - (Vector3)enemy.FirstLeader.Direction * 1.5f);
                
                //
                
                p.Followers.Add(enemy);
                followersPokemons[i] = pokemonInstance;
            }
        }

        public void DestroyFollowers()
        {
            for (int i = 0; i < followersPokemons.Length; i++)
            {
                Destroy(followersPokemons[i]);
            }
        }
    }
}
