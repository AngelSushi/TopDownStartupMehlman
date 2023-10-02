using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TypeInjector : MonoBehaviour
    {
        [SerializeField] TypeSpriteConverter _e;
        [SerializeField] TypeSpriteConverterRef _ref;

        ISet<TypeSpriteConverter> RealRef => _ref;


        void Awake()
        {
            //_ref.Set(_e);
            RealRef.Set(_e);
        }
    }
}
