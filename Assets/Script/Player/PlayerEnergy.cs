using UnityEngine;

public class PlayerEnergy : Singleton<PlayerEnergy>
{
    [Header("----------Energy------------")]
    [SerializeField] EnergyBar energyBar;
    public const int MAX_ENERGY = 100;
    public const int PERCENT = 1;
    int energy;

    private void Start()
    {
        energyBar.Initialize(energy, MAX_ENERGY);
        getEnergy(MAX_ENERGY);
    }
    public void getEnergy(int value)
    {
        if (energy == MAX_ENERGY) return;

        energy = Mathf.Clamp(energy + value, 0, MAX_ENERGY);
        energyBar.UpdateStats(energy, MAX_ENERGY);
    }

    public void useEnergy(int value)
    {
        energy -= value;
        energyBar.UpdateStats(energy, MAX_ENERGY);
    }

    public bool isEnough(int value) => energy >= value;
}
