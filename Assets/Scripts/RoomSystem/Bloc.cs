using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Bloc
    {
        [SerializeField] private Vector2 localPosition;

        public Vector2 LocalPosition
        {
            get => localPosition;
            set => localPosition = value;
        }

        [SerializeField] private Vector2Int roomPosition;

        public Vector2Int RoomPosition
        {
            get => roomPosition;
            set => roomPosition = value;
        }

        public Bloc(Vector2 localPosition, Vector2Int roomPosition)
        {
            this.localPosition = localPosition;
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

        public BlocOnOff(Vector2 localPosition, Vector2Int roomPosition) : base(localPosition, roomPosition) { }
    }

    [Serializable]
    public class BlocPokemon : Bloc
    {
        public BlocPokemon(Vector2 localPosition, Vector2Int roomPosition) : base(localPosition, roomPosition) {}
    }

    [Serializable]
    public class BlocVoid : Bloc
    {
        public BlocVoid(Vector2 localPosition, Vector2Int roomPosition) : base(localPosition, roomPosition) { }
    }

    [Serializable]
    public class BlockSpawn : Bloc
    {
        public BlockSpawn(Vector2 localPosition, Vector2Int roomPosition) : base(localPosition, roomPosition) { }
    }

    [Serializable]
    public class BlockTp : Bloc
    {
        public BlockTp(Vector2 localPosition, Vector2Int roomPosition) : base(localPosition, roomPosition) { }
    }
}
