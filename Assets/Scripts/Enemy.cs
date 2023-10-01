using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : EntityLiving
    { // Class Pokemon


        [SerializeField] private PokemonObject attachedPokemon;

        public PokemonObject AttachedPokemon
        {
            get => attachedPokemon;
            set => attachedPokemon = value;
        }


    }
}
