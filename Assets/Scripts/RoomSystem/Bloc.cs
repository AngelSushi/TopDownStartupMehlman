using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Bloc
    {
        [SerializeField] private Vector2 worldPosition;

        public Vector2 WorldPosition
        {
            get => worldPosition;
            set => worldPosition = value;
        }

        [SerializeField] private Vector2Int roomPosition;

        public Vector2Int RoomPosition
        {
            get => roomPosition;
            set => roomPosition = value;
        }

        public Bloc(Vector2 worldPosition, Vector2Int roomPosition)
        {
            this.worldPosition = worldPosition;
            this.roomPosition = roomPosition;
        }
    }

    [Serializable]
    public class BlocOnOff : Bloc
    {
        [SerializeField] private bool isOn;

        public bool IsOn
        {
            get => isOn;
            set => isOn = value;
        }

        public BlocOnOff(Vector2 worldPosition, Vector2Int roomPosition) : base(worldPosition, roomPosition) { }
    }

    [Serializable]
    public class BlocPokemon : Bloc
    {
        public BlocPokemon(Vector2 worldPosition, Vector2Int roomPosition) : base(worldPosition, roomPosition) {}
    }

    [Serializable]
    public class BlocVoid : Bloc
    {
        public BlocVoid(Vector2 worldPosition, Vector2Int roomPosition) : base(worldPosition, roomPosition) { }
    }
}
