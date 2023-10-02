using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    public class PokemonEntity : EntityLiving
    { // Class Pokemon


        [SerializeField, BoxGroup("Dependencies")] private PokemonsDataManager dataManager;
        
        [SerializeField, Required("nop")] private Health health;
        
        [SerializeField, Expandable] private PokemonObject attachedPokemon;
        public PokemonObject AttachedPokemon
        {
            get => attachedPokemon;
            set => attachedPokemon = value;
        }


        private void Awake()
        {
        }

        public override void Start()
        {
            base.Start();
                        
            attachedPokemon = dataManager.GetPokemonWithName(attachedPokemon.Name);
            //attachedPokemon = attachedPokemon.GetClone();
            //Debug.Log("starter " + attachedPokemon.IsStarter);
            //attachedPokemon.Data.statbase.MaxHP = attachedPokemon.Data.statbase.HP;
            
            health.MaxHealth = attachedPokemon.Data.statbase.HP;
            health.CurrentHealth = health.MaxHealth;
            
            if (!AttachedPokemon.IsStarter)
            {
                StartCoroutine(Damage());   
            }
        }

        private void Update() => attachedPokemon.Data.statbase.HP = health.CurrentHealth;

        private IEnumerator Damage()
        {
            yield return new WaitForSeconds(5f);
            health.Damage(50);


            if (health.IsDead)
            {
                Debug.Log("kill");
                AttachedPokemon.IsDead = true;
                Destroy(gameObject);
                yield return null;
            }
            
            StartCoroutine(Damage());
        }

    }
}
