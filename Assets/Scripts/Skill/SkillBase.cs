using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    // 스킬 데이터
    protected int _id;
    protected string _name;
    protected string _description;
    protected float _coolTime;
    protected float _minCoolTime;
    protected float _mpAmount;
    protected float _damage;
    protected Sprite _icon;
    protected Creature _owner;

    // base stat
    private float _baseCoolTime;
    private float _baseDamage;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public float BaseCoolTime { get { return _baseCoolTime; } }
    public float CoolTime { get { return _coolTime; } set { _coolTime = value; } }
    public float MinCoolTime { get { return _minCoolTime; } }
    public float MPAmount { get { return _mpAmount; } }
    public float BaseDamage { get { return _baseDamage; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public Sprite Icon { get { return _icon; } }
    public Creature Owner { get { return _owner; } }

    // 고정된 데이터가 아닌 실제 진행중인 쿨타임
    protected float _currentCoolTime;
    public float CurrentCoolTime { get { return _currentCoolTime; } }

    public virtual void SetData(SkillSO data, Creature owner)
    {
        _id = data.ID;
        _name = data.Name;
        _description = data.Description;
        _baseCoolTime = data.CoolTime;
        _coolTime = _baseCoolTime;
        _minCoolTime = data.MinCoolTime;
        _mpAmount = data.MPAmount;
        _baseDamage = data.Damage;
        _damage = _baseDamage;
        _icon = data.Icon;

        _owner = owner;
    }

    private void Update()
    {
        if (_currentCoolTime > 0)
        {
            _currentCoolTime -= Time.deltaTime;
        }
    }

    public virtual void DoSkill()
    {
        // 스킬 사용 시의 행동
        _owner.MP -= _mpAmount;
        _currentCoolTime = _coolTime;
    }

    public virtual void StopSkill()
    {
        // 스킬 중단 시의 행동
    }
}
