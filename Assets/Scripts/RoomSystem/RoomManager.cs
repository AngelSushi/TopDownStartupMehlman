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


        public event Func<PokemonObject> OnGeneratePokemon; 

        private void Start()
        {

            foreach (RoomData data in roomsData)
            {
               // Room room = new Room(data.roomParent,data.patternRef,data.patternGroundColor,data.patternOnOffColor,data.patternPokemonColor,data.patternVoidColor,data.startCoords);
               // rooms.Add(room);
            }   
            
          //  rooms[0].GenerateRoom();
          //  _currentRoom = rooms[0];
            
            //rooms[0].Mecanisms.Add(new OnOffMecanism(_currentRoom.Blocs.OfType<BlocOnOff>().ToList()));
            
            //InstantiateRoomPokemon(_currentRoom);
        }

        private void InstantiateRoomPokemon(Room room)
        {
            PokemonObject roomPokemon = OnGeneratePokemon?.Invoke();
            
            Debug.Log("generate pokemon is " + roomPokemon.Name);
            
            GameObject roomPokemonInstance = Instantiate(pokemonPrefab, room.RoomParent.transform);
            roomPokemonInstance.transform.position = room.Blocs.First(bloc => bloc is BlocPokemon).WorldPosition;
            roomPokemonInstance.GetComponentInChildren<SpriteRenderer>().sprite = roomPokemon.Sprite;
        }

        private void Update()
        {
          /*  if (_currentRoom.HasMecanism && _currentRoom.Mecanisms.All(mecanism =>mecanism.Solve()) && !_currentRoom.HasUnlockMecanism)
            {
                _currentRoom.HasUnlockMecanism = true;
                Debug.Log("solve mechanic can go to other room");// Drop Key Anim   
            }
            */
        }
    }
}
