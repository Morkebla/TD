using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5; // max hit points an enemy can have.

    [Tooltip("Adds amount to max hitpoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;

    int currentHitPoints = 0; // the hitpoints enemy currently have.

    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
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
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
