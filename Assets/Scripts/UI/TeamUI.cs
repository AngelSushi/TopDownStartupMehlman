using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common.FsNodeReaders;
using UnityEngine;

namespace Game
{
    public class TeamUI : MonoBehaviour
    {

        [SerializeField] private GameObject pokemonUIPrefab;
        [SerializeField] private PlayerReference player;

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
        }


        private void SpawnPlayer(Entity sEntity,Entity oEntity)
        {
            Debug.Log("spawnPlayer");
            
            if (sEntity != null && sEntity is Player)
            {
                Player p = (Player)sEntity;

                p.OnCapturePokemon += OnCapturePokemon;

            }
            else
            {
                if (oEntity != null && oEntity is Player)
                {
                    Player p = (Player)oEntity;
                    p.OnCapturePokemon -= OnCapturePokemon;
                }
            }
        }
        
        private void OnCapturePokemon(Pokemon capturePokemon)
        {
            Debug.Log("onCapturePokemon " + transform);
            Instantiate(pokemonUIPrefab,transform);
        }
    }
    
    
}
