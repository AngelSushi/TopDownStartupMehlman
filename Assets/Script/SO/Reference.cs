using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISet<T>
{
    void Set(T value);
}

public class Reference<T> : ScriptableObject, ISet<T>
{
    T _instance;

    public T Instance { get => _instance;}

    public event Action<T, T> OnValueChanged;

    void ISet<T>.Set(T value)
    {
        var old = _instance;
        _instance = value;
        OnValueChanged?.Invoke(_instance, old);
    }

}
