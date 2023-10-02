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

        [SerializeField] private List<PokemonObject> ownedPokemons = new List<PokemonObject>();
        [SerializeField] private List<PokemonObject> seenPokemons = new List<PokemonObject>();
        [SerializeField] private bool isInInventory;
        [SerializeField] private ManageFollowers manageFollowers;
        public bool IsInInventory
        {
            get => isInInventory;
            set => isInInventory = value;
        }
        
        public List<PokemonObject> OwnedPokemons
        {
            get => ownedPokemons;
        }

        public List<PokemonObject> SeenPokemons
        {
            get => seenPokemons;
        }
        
        

        public event Action<List<PokemonObject>, List<PokemonObject>,Player> OnOpenInventory;
        public event Func<Player,List<PokemonObject>> OnCloseInventory;
        public event Action<List<PokemonObject>> OnUpdateFollowers;
        public event Action OnCapturePokemon;
        public event Action<EntityLiving> OnLaunchAttack;

        public override void Start()
        {
            base.Start();
            roomManager.Instance.OnFinishGenerateRoom += OnFinishGenerateRoom;

            int index = 0;
            foreach (EntityLiving pokemonEntity in Followers)
            {
                if (pokemonEntity is PokemonEntity)
                {
                    PokemonEntity enemy = (PokemonEntity)pokemonEntity;
                    OwnedPokemons.Insert(index,enemy.AttachedPokemon);
                    index++;
                    enemy.gameObject.SetActive(true);
                }
            }

            
        }

        private void OnDestroy()
        {
            roomManager.Instance.OnFinishGenerateRoom -= OnFinishGenerateRoom;
        }

        public override void Update()
        {
            base.Update();

            if (CurrentRoom != null)
            {
                if (CurrentBloc is BlockTp)
                {
                    Room lastRoom = roomManager.Instance.GeneratedRooms[0];
                    Destroy(lastRoom.RoomGO);
                    roomManager.Instance.GeneratedRooms.Remove(lastRoom);
                    roomManager.Instance.GenerateRoom();
                    Room newRoom = roomManager.Instance.GeneratedRooms[0];
                    transform.position = newRoom.RoomGO.transform.TransformPoint(newRoom.Blocs.FirstOrDefault(bloc => bloc is BlockSpawn).LocalPosition);
                }
                
                if (CurrentRoom != null && CurrentRoom.HasMecanism && CurrentRoom.Mecanisms.All(mecanism => mecanism.Solve()) && !CurrentRoom.HasUnlockMecanism)
                {
                    CurrentRoom.HasUnlockMecanism = true;
                    CurrentRoom.RoomPokemon.SetActive(true);
                    Debug.Log("solve mechanic can go to other room");// Drop Key Anim   
                }
            }
        }

        public void LaunchAttack(EntityLiving attacker) => OnLaunchAttack?.Invoke(attacker);
        public void CapturePokemon() => OnCapturePokemon?.Invoke();
        
        private void OnFinishGenerateRoom(bool isFirstGeneration) {
            if (isFirstGeneration)
            {
                Room targetRoom = roomManager.Instance.GeneratedRooms[0];
                transform.position = targetRoom.RoomGO.transform.TransformPoint(targetRoom.Blocs.FirstOrDefault(bloc => bloc is BlockSpawn).LocalPosition);
            }
        }

        public void ManageInventory()
        {
            if (!isInInventory)
            {
                OnOpenInventory?.Invoke(OwnedPokemons,SeenPokemons,this);
                manageFollowers.DestroyFollowers();
            }
            else
            {
                List<PokemonObject> newFollowers = OnCloseInventory?.Invoke(this);
                manageFollowers.UpdateFollowersObject(newFollowers);
                OnUpdateFollowers?.Invoke(newFollowers);
            }
        }
    }
}
