using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] GameObject hitVFX;
    [SerializeField] AudioData[] hitSFX;
    [SerializeField] float moveSpeed;

    [SerializeField] protected Vector2 moveDirection;

    protected GameObject target;
    protected virtual void OnEnable()
    {
        StartCoroutine(MoveDirectly());
    }

    protected virtual IEnumerator MoveDirectly()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Chara>(out Chara chara))    //bool，Getcomponentより性能いい
        {
            chara.TakeDamage(damage);

            //var contactPoint = collision.GetContact(0); //当たり時の第一接触点
            //PoolManager.Release(hitVFX, contactPoint.point,Quaternion.LookRotation(contactPoint.normal));
            PoolManager.Release(hitVFX,collision.GetContact(0).point,Quaternion.LookRotation(collision.GetContact(0).normal));
            AudioManager.instance.PlayRandomSFX(hitSFX);
            gameObject.SetActive(false);
        }
    }
}