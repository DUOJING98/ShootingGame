using System.Collections;
using UnityEngine;

public class Chara : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [Header("-----HEALTH--------")]
    [SerializeField] protected float maxHealth;
    protected float Health;

    protected virtual void OnEnable()
    {
        Health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Health = 0f;
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    public virtual void ResHealth(float value)
    {
        if (Health == maxHealth)
            return;
        Health += value;
        //Health = Mathf.Clamp(Health,0f, maxHealth);
        Health = Mathf.Clamp(Health + value, 0f, maxHealth);
    }

    protected IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (Health < maxHealth)
        {
            yield return waitTime;

            ResHealth(maxHealth * percent);
        }
    }

    protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (Health > 0f)
        {
            yield return waitTime;

            TakeDamage(maxHealth * percent);
        }
    }
}
