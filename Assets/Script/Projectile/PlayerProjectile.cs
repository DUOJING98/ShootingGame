using UnityEngine;

public class PlayerProjectile : Projectile
{
    TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        if (moveDirection != Vector2.up)
        {
            transform.GetChild(0).rotation = Quaternion.FromToRotation(Vector2.up, moveDirection);
        }
    }

    private void OnDisable()
    {
        trail.Clear();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        PlayerEnergy.instance.getEnergy(PlayerEnergy.PERCENT); 
    }
}
