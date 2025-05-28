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

        if (target.activeSelf)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
            target = GameObject.FindGameObjectWithTag("Player");
            // Debug.Log($"Target: {target.name}, ActiveSelf: {target.activeSelf}, Position: {target.transform.position}");

        }

        else
        {
            //Debug.LogError("未找到玩家对象！");
            moveDirection = Vector2.down;
        }
    }
}
