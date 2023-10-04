using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PokemonEventsInjector : MonoBehaviour
    {
        [SerializeField] PokemonEvents _e;
        [SerializeField] PokemonEventsRef _ref;

        ISet<PokemonEvents> RealRef => _ref;


        void Awake()
        {
            //_ref.Set(_e);
            RealRef.Set(_e);
        }
    }
}
