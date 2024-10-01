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
    
    public Rigidbody2D Rb { get { return _rb; } }

    public float HP { get { return _hp; } protected set { _hp = value; OnChangedHP?.Invoke(_hp, _maxHP); } }
    public float MaxHP { get { return _maxHP; } }
    public virtual float MP { get; set; }
    public virtual float MaxMP { get; set; }
    public Creature Target { get { return _target; ; } set { _target = value; } }

    public UnityAction<float, float> OnChangedHP;
    public UnityAction OnDead;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        OnDead?.Invoke();
        gameObject.SetActive(false);
    }
}
