using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RoomInjector : MonoBehaviour
    {
        
        [SerializeField] RoomManager _e;
        [SerializeField] RoomManagerRef _ref;

        ISet<RoomManager> Ref => _ref;
        
        private void Awake() => Ref.Set(_e);
        
    }
}
