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
        if(collision.gameObject.TryGetComponent<Chara>(out Chara chara))    //bool��Getcomponent������ܤ���
        {
            chara.TakeDamage(damage);

            //var contactPoint = collision.GetContact(0); //������r�ε�һ�Ӵ���
            //PoolManager.Release(hitVFX, contactPoint.point,Quaternion.LookRotation(contactPoint.normal));
            PoolManager.Release(hitVFX,collision.GetContact(0).point,Quaternion.LookRotation(collision.GetContact(0).normal));
            AudioManager.instance.PlayRandomSFX(hitSFX);
            gameObject.SetActive(false);
        }
    }
}