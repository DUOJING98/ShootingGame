using System.Collections;
using UnityEngine;

public class Enemy_Projectile_Aming : Projectile
{
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void OnEnable()
    {
        //Debug.DrawLine(transform.position, transform.position + (Vector3)(moveDirection * 2f), Color.red, 1f);
        StartCoroutine(MoveDirectlyCoroutine());
        base.OnEnable();
    }

    IEnumerator MoveDirectlyCoroutine()
    {
        yield return null;

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        if (target != null && target.activeSelf)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
        }

        else
        {
            moveDirection = Vector2.down;
        }
    }
}
