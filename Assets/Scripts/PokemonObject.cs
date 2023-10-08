using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game
{
    [CreateAssetMenu(menuName = "PokemonData/Create Pokemon", fileName = "Create Pokemon")]
    public class PokemonObject : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private Pokemon data;
        [SerializeField] private Sprite sprite;

        public Alterable<int> Rarity;
        public RuntimeAnimatorController animator;
       
        public string Name
        {
            get => name;
            set => name = value;
        }

        public Pokemon Data
        {
            get => data;
            set => data = value;
        }

        public Sprite Sprite
        {
            get => sprite;
            set => sprite = value;
        }



        public PokemonObject GetClone()
        {
            var t = ScriptableObject.CreateInstance<PokemonObject>();
            t.name = name;
            t.Data = data;
            t.Sprite = sprite;
            t.animator = animator;

            return t;
        }

    }
}