using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime;

    float _damage;
    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector2.zero;
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

        // 풀링 필요
        Destroy(gameObject);
    }
}
