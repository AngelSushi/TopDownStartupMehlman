using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{

    public enum MecanismType
    {
        BUTTON,
        FIRE,
        BLOC,
        ICE
    }
    
    public class GenerateMecanismRoom : MonoBehaviour
    {
        
        
        

        public Mecanism OnGenerateMecanism(Room room)
        {
            MecanismType[] allTypes = (MecanismType[]) Enum.GetValues(typeof(MecanismType));
            MecanismType chooseTypeMecanism = (MecanismType) allTypes.GetValue(Random.Range(0, allTypes.Length));

            if (chooseTypeMecanism == MecanismType.ICE)
            {
                
            }
            else
            {
                
            }
            return null;
        }
    }
}
