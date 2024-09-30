using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    protected float _maxHP;
    protected float _hp;   
    
    public float HP { get { return _hp; } protected set { _hp = value; OnChangedHP?.Invoke(_hp, _maxHP); } }
    public float MaxHP { get { return _maxHP; } }
    public virtual float MP { get; set; }
    public virtual float MaxMP { get; set; }

    public UnityAction<float, float> OnChangedHP;
    public UnityAction OnDead;

    public float Direction { get { return transform.localScale.x; } }

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
