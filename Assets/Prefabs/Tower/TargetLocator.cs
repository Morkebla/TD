using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon; // used to set our weapon a.k.a tower prefab.
    Transform target;

    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform; // here we find our target in the world.
    }

    void Update()
    {
        AimWeapon();
    }
    void AimWeapon()
    {
        weapon.LookAt(target); // here we tell our weapon to look at the found Target.
        
    }
}
