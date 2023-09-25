using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PokemonData/Create Pokemon", fileName = "Create Pokemon")]
public class PokemonObject : ScriptableObject
{

    [SerializeField] private string name;
    [SerializeField] private Pokemon data;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int rarity;

    public int Rarity
    {
        get => rarity;
        set => rarity = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Pokemon Data
    {
        get => data;
        set => data = value;
    }

    public Sprite Sprite
    {
        get => sprite;
        set => sprite = value;
    }
}
