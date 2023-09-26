using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class RoomManager : MonoBehaviour
    {

        [SerializeField] private List<RoomData> roomsData;
        [SerializeField] private List<Room> generatedRooms = new List<Room>();
        [SerializeField] private GameObject pokemonPrefab;

        private Room _currentRoom;

        public List<RoomData> RoomsData
        {
            get => roomsData;
        }
        
        public List<Room> GeneratedRooms
        {
            get => generatedRooms;
        }

        
        /*
           *
           * Ajouter les différents mécanismes 
           * Génération du pokémon ( avec rareté ) - FAIT 
           * Systeme procédurale de génération de salles ( optionnel) 
           * 
           */

        [SerializeField] private int maxGenerateRoom;

        public event Func<Room> OnGenerateRoom;
        public event Func<PokemonObject> OnGeneratePokemon;
        public event Func<Room,Mecanism> OnGenerateMecanism;

        private void Start()
        {
            _currentRoom = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GenerateRoom();
            }
            
            if (_currentRoom != null && _currentRoom.HasMecanism && _currentRoom.Mecanisms.All(mecanism =>mecanism.Solve()) && !_currentRoom.HasUnlockMecanism)
            {
                _currentRoom.HasUnlockMecanism = true;
                Debug.Log("solve mechanic can go to other room");// Drop Key Anim   
            }
            
        }
        
        public void GenerateRoom()
        {
            int roomToGenerate = maxGenerateRoom - GeneratedRooms.Count;

            for (int i = 0; i < roomToGenerate; i++)
            {
                Room newRoom = OnGenerateRoom?.Invoke();

                if (newRoom != null)
                {
                    generatedRooms.Add(newRoom);
                    GenerateMecanism(newRoom);
                    InstantiateRoomPokemon(newRoom);
            
                    // a enlever la ligne du dessus
                    _currentRoom = newRoom;
                }
            }

            
            
        }
        
        public void InstantiateRoomPokemon(Room room)
        {
            PokemonObject roomPokemon = OnGeneratePokemon?.Invoke();
            
            GameObject roomPokemonInstance = Instantiate(pokemonPrefab, room.RoomGO.transform);
            roomPokemonInstance.transform.localPosition = room.Blocs.First(bloc => bloc is BlocPokemon).WorldPosition;
            roomPokemonInstance.GetComponentInChildren<SpriteRenderer>().sprite = roomPokemon.Sprite;
            roomPokemonInstance.SetActive(!room.HasMecanism);
        }

        public void GenerateMecanism(Room room)
        {
            Mecanism roomMecanism = OnGenerateMecanism?.Invoke(room);

            if (roomMecanism != null)
            {
                room.AddMecanism(roomMecanism);
            }
        }

        
    }
}
