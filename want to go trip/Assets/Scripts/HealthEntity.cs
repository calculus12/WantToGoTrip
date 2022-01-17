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

    // Health Entitiy 체력 감소는 무조건 OnDamage 함수 이용할 것 -> 상속받은 스크립트에서 명시적으로 Die() 사용 X.
    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die(hitPosition);
        }
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // Health Entity 체력 증가는 무조건 RestoreHealth 함수 이용할 것!
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

    public virtual void Die(Vector3 diePosition)
    {
        if (onDeath != null)
        {
            onDeath();
        }
        dead = true;
    }
}
