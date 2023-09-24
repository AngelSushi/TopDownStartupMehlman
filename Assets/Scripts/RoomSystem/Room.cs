using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{

    [Serializable]
    public struct RoomData
    {
        public Texture2D patternRef;
        public Color patternGroundColor;
        public Color patternOnOffColor;
        public Vector2 startCoords;
    }
    
    [System.Serializable]
    public class Room
    {
        [SerializeField] private Texture2D patternRef;
        [SerializeField] private Color patternGroundColor;
        [SerializeField] private Color patternOnOffColor;
        [SerializeField] private Vector2 startCoords;

        [SerializeField] private List<Bloc> blocs = new List<Bloc>();

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
            get => mecanisms.Count > 0;
        }
        
        [SerializeField] private bool hasUnlockMecanism;

        public bool HasUnlockMecanism
        {
            get => hasUnlockMecanism;
            set => hasUnlockMecanism = value;
        }

        public Room(Texture2D patternRef, Color patternGroundColor,Color patternOnOffColor, Vector2 startCoords)
        {
            this.patternRef = patternRef;
            this.patternGroundColor = patternGroundColor;
            this.patternOnOffColor = patternOnOffColor;
            this.startCoords = startCoords;
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
                    }
                }
            }
            
        }

    }
}
