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

        [SerializeField] private bool canFollow;

        public bool CanFollow
        {
            get => canFollow;
            set => canFollow = value;
        }

        public override void Start()
        {
            base.Start();
            dataManager = FindObjectOfType<PokemonsDataManager>(); // Ask if good
           
            attachedPokemon = dataManager.GetPokemonWithName(attachedPokemon.Name);
            health.MaxHealth = attachedPokemon.Data.statbase.MaxHP;
            health.CurrentHealth = attachedPokemon.Data.statbase.HP;
            
            if (!AttachedPokemon.Data.isStarter)
            {
                StartCoroutine(Damage());   
            }
        }

        public override void Update()
        { 
            base.Update();
            
         //   Debug.Log("data " + attachedPokemon.Data);
            
           // attachedPokemon.Data.statbase.HP = health.CurrentHealth;

            if(canFollow && leader != null)
            {
                Vector2 direction = leader.Direction != Vector2.zero ? leader.Direction : Vector2.one;
                transform.position = (leader.transform.position - (Vector3)direction * 1.5f);
            }

        }

        private IEnumerator Damage()
        {
            yield return new WaitForSeconds(5f);
            health.Damage(1);


            if (health.IsDead)
            {
                attachedPokemon.Data.statbase.HP = 0;
                AttachedPokemon.Data.isDead = true;
                Destroy(gameObject);
                yield return null;
            }
            
            StartCoroutine(Damage());
        }

    }
}
