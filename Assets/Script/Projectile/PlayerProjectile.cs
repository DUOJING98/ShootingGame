using UnityEngine;

public class PlayerProjectile : Projectile
{
    TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnDisable()
    {
        trail.Clear();
    }
}
