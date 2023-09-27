using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Game
{
    public class GenerateDoorRoom : MonoBehaviour
    {
        [SerializeField,BoxGroup("Dependencies")] private RoomManager roomManager;

        [SerializeField] private TileBase[] northDoor = new TileBase[6];
        [SerializeField] private TileBase[] eastDoor = new TileBase[6];
        [SerializeField] private TileBase[] southDoor = new TileBase[6];
        [SerializeField] private TileBase[] westDoor = new TileBase[6];


        private void OnGenerateDoor(Room room)
        {
            List<Bloc> sidedBlocks = room.Blocs
                .Where(bloc => bloc is not BlocOnOff && bloc is not BlocPokemon && bloc is not BlocVoid)
                .ToList()
                .Where(bloc => bloc.RoomPosition.x == 2 || bloc.RoomPosition.x == room.PatternRef.height - 3 || bloc.RoomPosition.y == 2 || bloc.RoomPosition.y == room.PatternRef.width - 3) // NE remove pas bien
                .ToList();

            // Gros soucis ca prend les blocs de void parfois 
            
            sidedBlocks.RemoveAll(bloc => bloc.RoomPosition == new Vector2Int(2, 2) || bloc.RoomPosition == new Vector2Int(room.PatternRef.height - 3, 2) || bloc.RoomPosition == new Vector2Int(2, room.PatternRef.width - 3) || bloc.RoomPosition == new Vector2Int(room.PatternRef.height - 3, room.PatternRef.width - 3));

            int randomBlocDoor = Random.Range(0, sidedBlocks.Count);
            Bloc blocDoor = sidedBlocks[randomBlocDoor];

            Tilemap groundMap = room.RoomGO.transform.GetChild(0).GetChild(0).GetComponent<Tilemap>();
            Tilemap wallsMap = room.RoomGO.transform.GetChild(0).GetChild(1).GetComponent<Tilemap>();
            
            Vector3 worldBlocPos = room.RoomGO.transform.TransformPoint(blocDoor.LocalPosition);
            
            /*
             * 1 : North
             * 2 : East
             * 3 :  South
             * 4 : West
             */
            
            (Vector3Int, int)[] sidesData =
            {
                (wallsMap.WorldToCell(new Vector3Int((int)worldBlocPos.x, (int)worldBlocPos.y + 1,(int) worldBlocPos.z)), 1),
                (wallsMap.WorldToCell(new Vector3Int((int)worldBlocPos.x + 1, (int)worldBlocPos.y,(int) worldBlocPos.z)),1),
                (wallsMap.WorldToCell(new Vector3Int((int)worldBlocPos.x, (int)worldBlocPos.y - 1,(int) worldBlocPos.z)),3),
                (wallsMap.WorldToCell(new Vector3Int((int)worldBlocPos.x - 1, (int)worldBlocPos.y,(int) worldBlocPos.z)),4)
            };

            Debug.Log("generate door");
            

            foreach ((Vector3Int, int) sideData in sidesData)
            {
                Vector3Int sideCell = sideData.Item1;
                int side = sideData.Item2;

                if (wallsMap.GetTile(sideCell) != null)
                {
                    groundMap.SetTile(sideCell, groundMap.GetTile(groundMap.WorldToCell(worldBlocPos)));

                    TileBase[] door = side == 1 ? northDoor
                        : side == 2 ? eastDoor
                        : side == 3 ? southDoor
                        : side == 4 ? westDoor
                        : northDoor;

                    int beginX = side == 1 || side == 4 ? -1 : 1;
                    int beginY = side == 3 || side == 4 ? -1 : 1;

                    int stepY = beginY < 0 ? 1 : -1;
                    int stepX = beginX < 0 ? 1 : -1;
                    int index = 0;
                    for (int i = beginY; i!= -beginY; i += stepY)
                    {
                        Debug.Log("y " + i);
                        for (int j = beginX; j != -beginX +stepX; j += stepX)
                        {
                            Debug.Log("x " + j);
                           // Debug.Log();
                            Vector3 cellWorld = wallsMap.CellToWorld(sideCell);
                            Vector3Int actualCellPos = new Vector3Int((int)cellWorld.x,(int)cellWorld.y,(int)cellWorld.z) + new Vector3Int(j,i,0);
                            actualCellPos = wallsMap.WorldToCell(actualCellPos);
                            
                            wallsMap.SetTile(actualCellPos,door[index]);
                            index++;

                        }
                    }

                    break;
                }
            }

            Debug.Log("worldPos " + worldBlocPos);
        }
    }
}
