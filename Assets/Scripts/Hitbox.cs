using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hitbox : MonoBehaviour
{
    [SerializeField] Creature owner;

    CapsuleCollider2D _collider;

    public void Init(Vector2 offset, Vector2 size)
    {
        _collider = GetComponent<CapsuleCollider2D>();

        _collider.offset = offset;
        _collider.size = size;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner is BossController)
            return;

        BossController boss = other.GetComponent<BossController>();
        if (boss != null)
        {
            owner.TakeDamage(boss.CollisionDamage);
        } 
    }
}
