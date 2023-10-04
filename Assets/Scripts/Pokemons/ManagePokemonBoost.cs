using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ManagePokemonBoost : MonoBehaviour
    {

        [SerializeField] private PokemonEventsRef eventRef;
        [SerializeField] private PlayerReference playerRef;

        private Player _player;

        private void Start()
        {
            eventRef.Instance.OnSwitchPokemonTeam += OnSwitchPokemonTeam;
            _player = (Player)playerRef.Instance;
        }

        private void OnDestroy()
        {
            eventRef.Instance.OnSwitchPokemonTeam -= OnSwitchPokemonTeam;
        }

        private void OnSwitchPokemonTeam(List<EntityLiving> newTeam)
        {
            if(_player != null)
            {
               // _player.Speed.RemoveTransformator();
            }
        }
    }
}
