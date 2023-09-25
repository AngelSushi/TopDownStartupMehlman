using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    
    [Serializable]
    public class OnOffMecanism : Mecanism
    {

        private List<BlocOnOff> _blocs;
        
        public OnOffMecanism(List<BlocOnOff> blocs)
        {
            _blocs = blocs;
        }

        public override bool Solve()
        {
            
            
            foreach (BlocOnOff bloc in _blocs)
            {
//                Debug.Log("solve is " + bloc.RoomPosition + " value " + bloc.IsOn + " " + RoomManager.instance._currentRoom.Blocs.IndexOf(bloc));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _blocs.First(bloc => !bloc.IsOn).IsOn = true;
                Debug.Log("set true for bloc ");
            }
            
            return _blocs.All(bloc => bloc.IsOn);
        }
    }
}
