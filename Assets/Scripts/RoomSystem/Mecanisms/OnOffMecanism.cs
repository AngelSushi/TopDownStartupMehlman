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

            return _blocs.All(bloc => bloc.IsOn);
        }
    }
}
