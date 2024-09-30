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
    protected float _mpAmount;
    protected Sprite _icon;
    protected Creature _owner;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public float CoolTime { get { return _coolTime; } }
    public float MPAmount { get { return _mpAmount; } }
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
        _coolTime = data.CoolTime;
        _mpAmount = data.MPAmount;
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
