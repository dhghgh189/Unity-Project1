using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    // ��ų ������
    int _id;
    string _name;
    string _description;
    float _coolTime;
    Sprite _icon;

    // ������ �����Ͱ� �ƴ� ���� �������� ��Ÿ��
    float _currentCoolTime;

    public float CurrentCoolTime { get { return _currentCoolTime; } }

    public virtual void SetData(int skillID)
    {
        if (DataManager.Instance.SkillDict.TryGetValue(skillID, out SkillSO data) == false)
        {
            Debug.LogError("SkillBase SetData failed...");
            Debug.LogError("Please check data");
            return;
        }

        _id = data.ID;
        _name = data.Name;
        _description = data.Description;
        _coolTime = data.CoolTime;
        _icon = data.Icon;
    }

    public virtual void DoSkill()
    {
        // ��ų ��� ���� �ൿ
        _currentCoolTime = _coolTime;
    }
}
