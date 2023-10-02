using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Codice.Client.Common.FsNodeReaders;
using UnityEngine;

namespace Game
{
    public class TeamUI : MonoBehaviour
    {

        [SerializeField] private GameObject pokemonUIPrefab;
        [SerializeField] private PlayerReference player;

        private List<GameObject> _childs = new List<GameObject>();
        private List<PokemonObject> _pokemons = new List<PokemonObject>();
        private void Awake()
        {
            if (player.Instance != null)
            {
                SpawnPlayer(player.Instance, player.Instance);
            }
            else
            {
                player.OnValueChanged += SpawnPlayer;
                
            }


            foreach (EntityLiving entityLiving in ((Player)player.Instance).Followers)
            {
                if (entityLiving is PokemonEntity)
                {
                    PokemonEntity pokemonEntity = (PokemonEntity) entityLiving;
                    _pokemons.Add(pokemonEntity.AttachedPokemon);
                }
            }

            OnUpdateFollowers(_pokemons);
        }

        private void SpawnPlayer(Entity sEntity,Entity oEntity)
        {
            if (sEntity != null && sEntity is Player)
            {
                Player p = (Player)sEntity;
                p.OnOpenInventory += OnOpenInventory;
                p.OnUpdateFollowers += OnUpdateFollowers;
            }
            else
            {
                if (oEntity != null && oEntity is Player)
                {
                    Player p = (Player)oEntity;
                    p.OnOpenInventory -= OnOpenInventory;
                    p.OnUpdateFollowers -= OnUpdateFollowers;
                }
            }
        }

        private void OnUpdateFollowers(List<PokemonObject> followers)
        {
            Player p = (Player)player.Instance;
            _childs.Clear();

            foreach (PokemonObject pokemonObject in followers)
            {
                GameObject initPrefab = Instantiate(pokemonUIPrefab, transform);
                PokemonUI pokemonUI = initPrefab.GetComponent<PokemonUI>();

                pokemonUI.PokemonSprite.sprite = pokemonObject.Sprite;
                pokemonUI.PokemonName.text = pokemonObject.Data.name.french;

                PokemonEntity pokemonEntity = (PokemonEntity)p.Followers.FirstOrDefault(pokemonEntity =>
                {
                    if (pokemonEntity is PokemonEntity)
                    {
                        if (((PokemonEntity)pokemonEntity).AttachedPokemon == pokemonObject)
                        {
                            return true;
                        }
                    }

                    return false;
                });

                pokemonUI.HealthBar.Health = pokemonEntity.GetComponent<Health>();
                pokemonUI.HealthBar.HealthText = pokemonUI.HealthText;
                _childs.Add(initPrefab);
            }
        }

        private void OnOpenInventory(List<PokemonObject> arg1, List<PokemonObject> arg2, Player arg3)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    
    
}
