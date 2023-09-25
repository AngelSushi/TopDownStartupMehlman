using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class RoomGenerator : MonoBehaviour
    {
        [SerializeField, BoxGroup("Dependencies")] private RoomManager roomManager;

        [SerializeField] private int maxGenerateRoom;
        [SerializeField] private int squareSpawnSize;

        // Aucun retour en arrière possible ==> rmmove de la porte quand on sort

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GenerateRoom();
            }
        }

        private void GenerateRoom()
        {
            int roomToGenerate = maxGenerateRoom - roomManager.GeneratedRooms.Count;

            for (int i = 0; i < roomToGenerate; i++)
            {
                int randomRoom = Random.Range(0, roomManager.RoomsData.Count);
                RoomData randomRoomData = roomManager.RoomsData[randomRoom];
                
                Room room = new Room(randomRoomData.roomParent,randomRoomData.patternRef,randomRoomData.patternGroundColor,randomRoomData.patternOnOffColor,randomRoomData.patternPokemonColor,
                    randomRoomData.patternVoidColor,randomRoomData.startCoords,randomRoomData.canHaveMecanism);
                
                room.GenerateRoom();

                int randomX = Random.Range(-squareSpawnSize, squareSpawnSize);
                int randomY = Random.Range(-squareSpawnSize, squareSpawnSize);

                while (Physics2D.OverlapCircleAll(new Vector2(randomX, randomY), 25, 1 << 3).Length > 0)
                {
                    randomX = Random.Range(-squareSpawnSize, squareSpawnSize);
                    randomY = Random.Range(-squareSpawnSize, squareSpawnSize);
                }
                
                
                GameObject roomInstance = Instantiate(room.RoomParent, roomManager.transform);
                roomInstance.transform.position = new Vector3(randomX, randomY, roomInstance.transform.position.z);
                roomInstance.SetActive(true);

                room.RoomGO = roomInstance;
                
                roomManager.InstantiateRoomPokemon(room);
                
                roomManager.GeneratedRooms.Add(room);

            }
        }
        
        
        
    }
}
