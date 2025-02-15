using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{

    [Serializable]
    public struct RoomData
    {
        public GameObject roomParent;
        public Texture2D patternRef;
        public Color patternGroundColor;
        public Color patternOnOffColor;
        public Color patternPokemonColor;
        public Color patternVoidColor;
        public Color patternSpawnColor;
        public Color patternTpColor;
        public Vector2 startCoords;
        public List<MecanismType> allMecanisms;
    }
    
    [System.Serializable]
    public class Room
    {
        [SerializeField] private GameObject roomParent;
        [SerializeField] private Texture2D patternRef;
        [SerializeField] private Color patternGroundColor;
        [SerializeField] private Color patternOnOffColor;
        [SerializeField] private  Color patternPokemonColor;
        [SerializeField] private  Color patternVoidColor;
        [SerializeField] private Color patternSpawnColor;
        [SerializeField] private Color patternTpColor;
        [SerializeField] private Vector2 startCoords;
        [SerializeField] private GameObject room;
        [SerializeField] private List<Bloc> blocs = new List<Bloc>();
        [SerializeField] private List<MecanismType> allMecanisms;

        public Texture2D PatternRef
        {
            get => patternRef;
        }
        
        public bool CanHaveMecanism
        {
            get => allMecanisms.Count > 0;
        }

        public List<MecanismType> AllMecanisms
        {
            get => allMecanisms;
        }

        public GameObject RoomGO
        {
            get => room;
            set => room = value;
        }
        public List<Bloc> Blocs
        {
            get => blocs;
        }

        [SerializeField] private List<Mecanism> mecanisms = new List<Mecanism>();

        public List<Mecanism> Mecanisms
        {
            get => mecanisms;
        }

        public bool HasMecanism
        {
            get => Mecanisms.Count > 0;
        }
        
        [SerializeField] private bool hasUnlockMecanism;

        public bool HasUnlockMecanism
        {
            get => hasUnlockMecanism;
            set => hasUnlockMecanism = value;
        }

        public GameObject RoomParent
        {
            get => roomParent;
        }

        private GameObject roomPokemon;

        public GameObject RoomPokemon
        {
            get => roomPokemon;
            set => roomPokemon = value;
        }

        public Room(GameObject roomParent, Texture2D patternRef, Color patternGroundColor, Color patternOnOffColor, Color patternPokemonColor, Color patternVoidColor, Color patternSpawnColor, Color patternTpColor, Vector2 startCoords,List<MecanismType> allMecanisms)
        {
            this.roomParent = roomParent;
            this.patternRef = patternRef;
            this.patternGroundColor = patternGroundColor;
            this.patternOnOffColor = patternOnOffColor;
            this.patternPokemonColor = patternPokemonColor;
            this.patternVoidColor = patternVoidColor;
            this.patternSpawnColor = patternSpawnColor;
            this.patternTpColor = patternTpColor;
            this.startCoords = startCoords;
            this.allMecanisms = allMecanisms;
        }


        // Ajouter a la main les rooms avec une struct RoomData pr les trucs de base
        
        public void GenerateRoom()
        {
            for (int i = 0; i < patternRef.height; i++)
            {
                for (int j = 0; j < patternRef.width; j++)
                {
                    Color pixelColor = patternRef.GetPixel(j,i);

                    if (pixelColor.a == 1)
                    {
                        if (pixelColor == patternGroundColor)
                        {
                            blocs.Add(new Bloc(new Vector2(startCoords.x +  j,startCoords.y +  i),new Vector2Int(i,j))); // with a cell size of (1,1)
                        }

                        if (pixelColor == patternOnOffColor)
                        {
                            blocs.Add(new BlocOnOff(new Vector2(startCoords.x +  j,startCoords.y +  i),new Vector2Int(i,j))); // with a cell size of (1,1)
                        }

                        if (pixelColor == patternPokemonColor)
                        {
                            blocs.Add(new BlocPokemon(new Vector2(startCoords.x + j,startCoords.y + i),new Vector2Int(i,j)));
                        }

                        if (pixelColor == patternVoidColor)
                        {
                            blocs.Add(new BlocVoid(new Vector2(startCoords.x + j,startCoords.y + i),new Vector2Int(i,j)));
                        }

                        if (pixelColor == patternSpawnColor)
                        {
                            blocs.Add(new BlockSpawn(new Vector2(startCoords.x +  j,startCoords.y +  i),new Vector2Int(i,j)));
                        }

                        if (pixelColor == patternTpColor)
                        {
                            blocs.Add(new BlockTp(new Vector2(startCoords.x +  j,startCoords.y +  i),new Vector2Int(i,j)));
                        }

                    }
                }
            }
            
        }

        public void AddMecanism(Mecanism mecanism) => Mecanisms.Add(mecanism);

        public bool Exists(Vector2Int roomCoords) => blocs.Where(bloc => bloc.RoomPosition == roomCoords).ToList().Count > 0;
        
        
        public Bloc GetBlocAt(Vector2Int roomCoords)
        {
            return blocs.FirstOrDefault(bloc => bloc.RoomPosition == roomCoords);
        }
    }
}
