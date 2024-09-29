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
    protected Sprite _icon;
    protected GameObject _ownerObject;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public float CoolTime { get { return _coolTime; } }
    public Sprite Icon { get { return _icon; } }

    // ������ �����Ͱ� �ƴ� ���� �������� ��Ÿ��
    protected float _currentCoolTime;
    public float CurrentCoolTime { get { return _currentCoolTime; } }

    public virtual void SetData(SkillSO data, GameObject ownerObject)
    {
        _id = data.ID;
        _name = data.Name;
        _description = data.Description;
        _coolTime = data.CoolTime;
        _icon = data.Icon;

        _ownerObject = ownerObject;
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
        _currentCoolTime = _coolTime;
    }

    public virtual void StopSkill()
    {
        // ��ų �ߴ� ���� �ൿ
    }
}
