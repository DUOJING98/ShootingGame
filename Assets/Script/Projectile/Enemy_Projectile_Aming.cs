using System.Collections;
using UnityEngine;

public class Enemy_Projectile_Aming : Projectile
{
    protected override void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
            //Debug.Log($"׷���ӵ�����{moveDirection}, ���λ�ã�{target.transform.position}, �ӵ�λ�ã�{transform.position}");
        }
        else
        {
            Debug.LogError("δ�ҵ���Ҷ���");
            moveDirection = Vector2.down;
        }

        StartCoroutine(MoveDirectly());
    }
}
