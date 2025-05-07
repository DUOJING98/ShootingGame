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
            //Debug.Log($"追踪子弹方向：{moveDirection}, 玩家位置：{target.transform.position}, 子弹位置：{transform.position}");
        }
        else
        {
            Debug.LogError("未找到玩家对象！");
            moveDirection = Vector2.down;
        }

        StartCoroutine(MoveDirectly());
    }
}
