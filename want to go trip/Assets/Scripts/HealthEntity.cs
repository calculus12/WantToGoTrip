using System;
using UnityEngine;

public class HealthEntity : MonoBehaviour, IDamageable 
{
    public float startingHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action onDeath;

    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void RestoreHealth(float restoreHealth)
    {
        if (dead)
        {
            return;
        }

        health += restoreHealth;
    }

    public virtual void Die()
    {
        if (onDeath != null)
        {
            onDeath();
        }
        dead = true;
    }
}
