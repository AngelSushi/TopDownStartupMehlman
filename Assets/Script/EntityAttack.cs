using System.Collections;
using System.Collections.Generic;
using Game;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class EntityAttack : MonoBehaviour
{

    
    
    [SerializeField, BoxGroup("Dependencies")] private PlayerReference playerRef;
    [SerializeField, BoxGroup("Dependencies")] private EntityLiving entityRef;


    public event UnityAction OnAttack;


    
    
    
    /*public void LaunchAttack()
    {
        OnAttack?.Invoke();
        foreach (var el in _attackZone.InZone)
        {
            el.Damage(10);
        }
    }
    */

        

}
