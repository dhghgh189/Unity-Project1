using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime;

    float _damage;
    Rigidbody2D _rb;

    Creature _owner;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector2.zero;
    }

    public void SetOwner(Creature owner)
    {
        _owner = owner;

        // 자신을 생성한 오브젝트와 충돌하지 않도록 처리
        Physics2D.IgnoreLayerCollision(gameObject.layer, owner.gameObject.layer);
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Fire(Vector2 dir, float speed)
    {
        _rb.AddForce(dir * speed, ForceMode2D.Impulse);
        StartCoroutine(DestroyRoutine());
    }

    IEnumerator DestroyRoutine()
    {
        WaitForSeconds _destroyTime = new WaitForSeconds(destroyTime);

        yield return _destroyTime;

        // pooling
        if (PoolManager.Instance.Push(gameObject) == false)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Creature creature = other.GetComponent<Creature>();
        if (creature != null)
        {
            creature.TakeDamage(_damage);

            // pooling
            if (PoolManager.Instance.Push(gameObject) == false)
                Destroy(gameObject);
        }
    }
}
