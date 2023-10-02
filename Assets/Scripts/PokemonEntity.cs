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

        public PokemonsDataManager DataManager
        {
            get => dataManager;
            set => dataManager = value;
        }

        [SerializeField, Required("nop")] private Health health;
        
        [SerializeField, Expandable] private PokemonObject attachedPokemon;
        public PokemonObject AttachedPokemon
        {
            get => attachedPokemon;
            set => attachedPokemon = value;
        }

        public override void Start()
        {
            base.Start();
            attachedPokemon = dataManager.GetPokemonWithName(attachedPokemon.Name);
            
            
            health.MaxHealth = attachedPokemon.Data.statbase.MaxHP;
            health.CurrentHealth = attachedPokemon.Data.statbase.HP;
            
            if (!AttachedPokemon.IsStarter)
            {
                StartCoroutine(Damage());   
            }
        }

        public override void Update()
        { 
            base.Update();
            attachedPokemon.Data.statbase.HP = health.CurrentHealth;
        }

        private IEnumerator Damage()
        {
            yield return new WaitForSeconds(5f);
            health.Damage(5);


            if (health.IsDead)
            {
                Debug.Log("kill");
                attachedPokemon.Data.statbase.HP = 0;
                AttachedPokemon.IsDead = true;
                Destroy(gameObject);
                yield return null;
            }
            
            StartCoroutine(Damage());
        }

    }
}
