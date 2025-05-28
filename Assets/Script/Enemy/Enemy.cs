using UnityEngine;

public class Enemy : Chara
{
    [SerializeField] int deathEnergyPlus = 3;
    [SerializeField] int deathScore = 100;

    public override void Die()
    {
        ScoreManager.instance.AddScore(deathScore);
        PlayerEnergy.instance.getEnergy(deathEnergyPlus);
        EnemyManager.instance.RemoveFromList(gameObject);
        base.Die();
    }
}
