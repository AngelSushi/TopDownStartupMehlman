using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PokemonEvents : MonoBehaviour
    {

        public event Action<List<EntityLiving>> OnSwitchPokemonTeam;


        public void SwitchPokemonTeam(List<EntityLiving> newTeam) => OnSwitchPokemonTeam?.Invoke(newTeam);


    }
}
