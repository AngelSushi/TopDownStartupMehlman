using System;
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
    [SerializeField] private bool isStarter;
    [SerializeField] private bool isDead;

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

    public bool IsStarter
    {
        get => isStarter;
        set => isStarter = value;
    }

    public bool IsDead
    {
        get => isDead; 
        set => isDead = value;
    }

    public PokemonObject GetClone()
    {
        var t = ScriptableObject.CreateInstance<PokemonObject>();
        t.name = name;
        t.Data = data;
        t.Sprite = sprite;
        t.Rarity = rarity;
        t.IsStarter = isStarter;
        t.IsDead = isDead;
        return t;
    }

}