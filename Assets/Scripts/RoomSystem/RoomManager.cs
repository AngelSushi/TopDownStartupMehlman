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
        [SerializeField] private List<Room> rooms;


        public Room _currentRoom;

        public static RoomManager instance;
        
        /*
           *
           * Ajouter les différents mécanismes
           * Génération du pokémon ( avec rareté )
           * Systeme procédurale de génération de salles ( optionnel) 
           * 
           */
        
        
        
        private void Start()
        {
            instance = this;

            foreach (RoomData data in roomsData)
            {
                Room room = new Room(data.patternRef,data.patternGroundColor,data.patternOnOffColor,data.startCoords);
                rooms.Add(room);
                
            }
            
            rooms[0].GenerateRoom();
            _currentRoom = rooms[0];
            
            rooms[0].Mecanisms.Add(new OnOffMecanism(_currentRoom.Blocs.OfType<BlocOnOff>().ToList()));
        }

        private void Update()
        {
            if (_currentRoom.HasMecanism && _currentRoom.Mecanisms.All(mecanism =>mecanism.Solve()) && !_currentRoom.HasUnlockMecanism)
            {
                _currentRoom.HasUnlockMecanism = true;
                // Drop Key Anim    
                Debug.Log("solve mechanic can go to other room");
            }
            
        }
    }
}
