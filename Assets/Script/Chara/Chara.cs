using System.Collections;
using UnityEngine;

public class Chara : MonoBehaviour
{
    [Header("-----DEATH--------")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioData[] deathSFX;
    [Header("-----HEALTH--------")]
    [SerializeField] protected float maxHealth;
    [SerializeField] StatsBar healthBar;
    [SerializeField] bool showHealthBar = true;
    protected float Health;

    protected virtual void OnEnable()
    {
        Health = maxHealth;
        if (showHealthBar)
        {
            ShowHealthBar();
        }
        else
        {
            HideHealthBar();
        }
    }

    public void ShowHealthBar()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.Initialize(Health, maxHealth);
    }

    public void HideHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (showHealthBar && gameObject.activeSelf)
        {
            healthBar.UpdateStats(Health, maxHealth);
        }
        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Health = 0f;
        AudioManager.instance.PlayRandomSFX(deathSFX);
        PoolManager.Release(deathVFX, transform.position);
        gameObject.SetActive(false);
    }

    public virtual void ResHealth(float value)
    {
        if (Health == maxHealth)
            return;
        //Health += value;
        //Health = Mathf.Clamp(Health,0f, maxHealth);
        Health = Mathf.Clamp(Health + value, 0f, maxHealth);
        if (showHealthBar)
        {
            healthBar.UpdateStats(Health, maxHealth);
        }
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
