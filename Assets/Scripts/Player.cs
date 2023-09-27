using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Player : EntityLiving
    {

        [SerializeField, BoxGroup("Dependencies")] private RoomManagerRef roomManager;
        
        
        [SerializeField] private InputActionReference action;

        [SerializeField] private DataReader data;
        
        public event Action<Pokemon> OnCapturePokemon;
        public event Action<EntityLiving> OnLaunchAttack;

        public override void Start()
        {
            base.Start();
            action.action.started += OnMove;
            action.action.performed += OnMove;
            action.action.canceled += OnMove;

            roomManager.Instance.OnFinishGenerateRoom += OnFinishGenerateRoom;
        }

        public void LaunchAttack(EntityLiving attacker) => OnLaunchAttack?.Invoke(attacker);
        public void CapturePokemon(Pokemon pokemon) => OnCapturePokemon?.Invoke(pokemon);

        private void OnFinishGenerateRoom(bool isFirstGeneration) {
            
            if (isFirstGeneration)
            {
                Room targetRoom = roomManager.Instance.GeneratedRooms[0];
                transform.position = targetRoom.RoomGO.transform.TransformPoint(targetRoom.Blocs.FirstOrDefault(bloc => bloc is BlockSpawn).LocalPosition);
            }
        }
        
        /*
             *
             *
             * IL me faudrait un script par attaque ( Fireball, Ice, Liane)
             * Un event est attaché a celui ci lorsuq'on attack depuis le playerbrain
             * 
         */
    }
}
