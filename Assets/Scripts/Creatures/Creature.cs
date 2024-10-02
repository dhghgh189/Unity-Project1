using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    protected float _maxHP;
    protected float _hp;
    protected Creature _target;
    protected Rigidbody2D _rb;
    protected CapsuleCollider2D _collider;
    protected bool _isDead;

    public Rigidbody2D Rb { get { return _rb; } }
    public CapsuleCollider2D Collider { get { return _collider; } }
    public bool IsDead { get { return _isDead; } }

    public float HP 
    { 
        get { return _hp; }
        protected set 
        { 
            _hp = Mathf.Clamp(value, 0, _maxHP);
            OnChangedHP?.Invoke(_hp, _maxHP);
        }
    }
    public float MaxHP { get { return _maxHP; } }
    public virtual float MP { get; set; }
    public virtual float MaxMP { get; set; }
    public Creature Target { get { return _target; ; } set { _target = value; } }

    public UnityAction<float, float> OnChangedHP;
    public UnityAction OnDead;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    public float Direction 
    { 
        get 
        { 
            return transform.localScale.x < 0 ? -1 : 1; 
        }
    }

    public virtual void TakeDamage(float damage)
    {
        HP -= damage;
        if (_hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        _isDead = true;
        OnDead?.Invoke();
        gameObject.SetActive(false);
    }
}
