using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    protected float _maxHp;
    protected float _hp;   
    
    public float HP { get { return _hp; } protected set { _hp = value; OnChangedHP?.Invoke(_hp, _maxHp); } }
    public float MaxHP { get { return _maxHp; } }

    public UnityAction<float, float> OnChangedHP;
    public UnityAction OnDead;

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
