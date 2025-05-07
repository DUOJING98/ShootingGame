using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void Awake()
    {
        if (moveDirection != Vector2.down)
        {
            transform.rotation = Quaternion.FromToRotation(Vector2.down, moveDirection);
        }
    }
}
