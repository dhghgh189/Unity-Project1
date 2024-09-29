using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    // ��ų ������
    int _id;
    string _name;
    string _description;
    float _coolTime;
    Sprite _icon;
    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public float CoolTime { get { return _coolTime; } }
    public Sprite Icon { get { return _icon; } }

    // ������ �����Ͱ� �ƴ� ���� �������� ��Ÿ��
    float _currentCoolTime;
    public float CurrentCoolTime { get { return _currentCoolTime; } }

    public virtual void SetData(int skillID)
    {
        if (DataManager.Instance.SkillDict.TryGetValue(skillID, out SkillSO data) == false)
        {
            Debug.LogError($"SkillBase SetData failed... / ID : {skillID}");
            Debug.LogError("Please check data");
            return;
        }

        _id = data.ID;
        _name = data.Name;
        _description = data.Description;
        _coolTime = data.CoolTime;
        _icon = data.Icon;
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
