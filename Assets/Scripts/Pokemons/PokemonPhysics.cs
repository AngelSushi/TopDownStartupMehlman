using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{

    [Serializable]
    public class PhysicsLayer
    {
        [SerializeField] private int layer1;
        [SerializeField] private int layer2;
        [SerializeField] private bool value;

        public int Layer1
        {
            get => layer1;
        }

        public int Layer2
        {
            get => layer2;
        }

        public bool Value
        {
            get => value;
        }
    }

    public class PokemonPhysics : MonoBehaviour
    {

        [SerializeField] private List<PhysicsLayer> physicLayers;

        [SerializeField] private PlayerReference _playerRef;

        private EntityLiving _entity;

        void Awake()
        {
            foreach(PhysicsLayer pLayer in physicLayers)
            {
                Physics2D.IgnoreLayerCollision(pLayer.Layer1,pLayer.Layer2,pLayer.Value);
            }

            if (_playerRef.Instance != null)
            {
                SpawnPlayer(_playerRef.Instance, _playerRef.Instance);
            }
            else
            {
                _playerRef.OnValueChanged += SpawnPlayer;
            }

            _entity = GetComponentInParent<EntityLiving>();
        }

        private void SpawnPlayer(Entity sEntity, Entity oEntity)
        {
            if (sEntity != null && sEntity is Player)
            {
                Player p = (Player)sEntity;
                p.OnControllerSwitch += OnControllerSwitch;
            }
            else
            {
                if (oEntity != null && oEntity is Player)
                {
                    Player p = (Player)oEntity;
                    p.OnControllerSwitch -= OnControllerSwitch;
                }
            }
        }

        private void OnControllerSwitch(EntityLiving newController)
        {
            if(newController is PokemonEntity)
            {
                Debug.Log("enter switch " + newController.name);
                foreach (PhysicsLayer pLayer in physicLayers)
                {
                    Debug.Log("set to " + (!pLayer.Value));
                    Physics2D.IgnoreLayerCollision(pLayer.Layer1, pLayer.Layer2, !pLayer.Value);
                }
            }
            else
            {
                foreach (PhysicsLayer pLayer in physicLayers)
                {
                    Debug.Log("set to true");
                    if(_entity is PokemonEntity)
                    {
                        PokemonEntity pokemonEntity = (PokemonEntity)_entity;
                        
                        if(pokemonEntity.CanFollow)
                        {

                        }

                    }
                    else
                    {
                      
                    }
                    
                }
            }
        }
    }
}
