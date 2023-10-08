using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ManagePokemonBoost : MonoBehaviour
    {

        [SerializeField] private PokemonsDataManager pokemonDatasManager;
        [SerializeField] private PokemonEventsRef eventRef;
        [SerializeField] private PlayerReference playerRef;

        private Player _player;

        private List<object> _pokemonBoosts = new List<object>();

        private void Start()
        {
            eventRef.Instance.OnSwitchPokemonTeam += OnSwitchPokemonTeam;
            _player = (Player)playerRef.Instance;
            OnSwitchPokemonTeam(_player.Followers);
        }

        private void OnDestroy()
        {
            eventRef.Instance.OnSwitchPokemonTeam -= OnSwitchPokemonTeam;
        }

        private void OnSwitchPokemonTeam(List<EntityLiving> newTeam)
        {
            if(_player != null)
            {

                foreach(object boost in _pokemonBoosts)
                {
                    _player.Speed.RemoveTransformator(boost);
                    newTeam.ForEach(entityPokemon => entityPokemon.Speed.RemoveTransformator(boost));
                    pokemonDatasManager.datasPokemon.ForEach(pokemonObject => pokemonObject.Rarity.RemoveTransformator(boost));
                }

                _pokemonBoosts.Clear();


                foreach (EntityLiving entity in newTeam)
                {
                    if (entity is PokemonEntity)
                    {
                        PokemonEntity pokemon = (PokemonEntity)entity;

                        foreach (string type in pokemon.AttachedPokemon.Data.type)
                        {
                            switch(type.ToLower())
                            {
                                case "flying":
                                    object speedBost = _player.Speed.AddTransformator(s => s + 2, 1);
                                    _pokemonBoosts.Add(speedBost);
                                    newTeam.ForEach(entityPokemon => _pokemonBoosts.Add(entityPokemon.Speed.AddTransformator(s => s + 2, 1)));
                                    break;

                                case "rock": // Add push force
                                    break;

                                    /*
                                     * 
                                     * regen
                                     * 
                                     */
                            }
                        }

                        switch(pokemon.AttachedPokemon.Data.id) 
                        {
                            case 52: // Miaouss
                                pokemonDatasManager.datasPokemon.ForEach(pokemonObject => _pokemonBoosts.Add(pokemonObject.Rarity.AddTransformator(r => r + 10, 1)));
                                break;
                        }
                    }
                }
            }
        }
    }
}
