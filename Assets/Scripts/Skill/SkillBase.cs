using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    // ��ų ������
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

    // ������ �����Ͱ� �ƴ� ���� �������� ��Ÿ��
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
        // ��ų ��� ���� �ൿ
        _owner.MP -= _mpAmount;
        _currentCoolTime = _coolTime;
    }

    public virtual void StopSkill()
    {
        // ��ų �ߴ� ���� �ൿ
    }
}
