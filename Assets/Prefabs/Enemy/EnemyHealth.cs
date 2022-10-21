using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5; // max hit points an enemy can have.
    int currentHitPoints = 0; // the hitpoints enemy currently have.

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    void ProcessHit()
    {
        currentHitPoints--; // once hit the current hit points fall.

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false); // once dead we return the enemy to the ObjectPool to be reused.
        }
    }
}
