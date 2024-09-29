using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
    }
}
