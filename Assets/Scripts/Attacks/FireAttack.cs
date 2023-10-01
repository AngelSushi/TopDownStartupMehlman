using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class FireAttack : MonoBehaviour
    {
        // Destruction lorsque collision ou sort du champ de la caméra
        
        [SerializeField] private PlayerReference playerRef;
        [SerializeField] private EntityLiving entityRef;

        [SerializeField] private PoolInstance pool;
        
        private bool _canLaunchAttack;
        
        [SerializeField] private float attackCooldown;
        
        private void Awake()
        {
            if (playerRef.Instance != null)
            {
                SpawnPlayerRef(playerRef.Instance, playerRef.Instance);
            }
            else
            {
                playerRef.OnValueChanged += SpawnPlayerRef;
                
            }

            _canLaunchAttack = true;
        }
        
        private void SpawnPlayerRef(Entity sEntity,Entity oEntity)
        {
            if (sEntity != null && sEntity is Player)
            {
                Player p = (Player)sEntity;

                p.OnLaunchAttack += OnLaunchAttack;

            }
            else
            {
                if (oEntity != null && oEntity is Player)
                {
                    Player p = (Player)oEntity;
                    p.OnLaunchAttack -= OnLaunchAttack;
                }
            }
        }


        private void OnLaunchAttack(EntityLiving attacker)
        {
            if (entityRef == attacker && _canLaunchAttack)
            {
                pool?.Pool.Get();
                StartCoroutine(WaitCooldown());
            }
        }
        
        private IEnumerator WaitCooldown()
        {
            _canLaunchAttack = false;
            yield return new WaitForSeconds(attackCooldown);
            _canLaunchAttack = true;
        }
        
    }
}
