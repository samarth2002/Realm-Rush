using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    Transform target;
    [SerializeField] Transform Weapon;
    [SerializeField] float Range = 15f;

    [SerializeField] ParticleSystem bullets;
    Enemy[] enemies;
    void Start()
    {
        target = FindObjectOfType<Enemy>().transform;
    }



    void Update()
    {
        TargetClosestEnemy();
        AimWeapon();
    }

    void TargetClosestEnemy()
    {
        enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        float targetDistance;
        Transform closestDistance = null;

        foreach(Enemy enemy in enemies)
        {
            targetDistance = Vector3.Distance(transform.position , enemy.transform.position);
            if(targetDistance < maxDistance)
            {
                closestDistance = enemy.transform;
                maxDistance = targetDistance;
            }

        }
        target = closestDistance;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position , target.position);
        Weapon.LookAt(target.transform);
        if(targetDistance > Range)
        {
            Attack(false);
        }
        else
        {
            Attack(true);
        }
        
    }

    void Attack(bool Shoot)
    {
        var emissionModule = bullets.emission;
        emissionModule.enabled = Shoot;
    
    }
}
