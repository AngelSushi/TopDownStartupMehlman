using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokemonData/Create Pokemon", fileName = "Create Pokemon")]
public class PokemonObject : ScriptableObject
{

    public string name;
    public Pokemon data;
    public Sprite sprite;
}
