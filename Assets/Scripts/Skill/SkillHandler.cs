using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    SkillBase[] _skills = new SkillBase[(int)Enums.ESkillSlot.Max];

    public void AddSkill(int skillID)
    {
        // ID �˻�
        if (DataManager.Instance.SkillDict.TryGetValue(skillID, out SkillSO data) == false)
        {
            Debug.LogError($"SkillHandler AddSkill failed... / ID : {skillID}");
            Debug.LogError("Please Check data");
        }

        // �ߺ� �˻�
        if (_skills[(int)data.Slot] != null)
        {
            Debug.LogWarning($"{data.Slot} is not null! / ID : {skillID}");
            Debug.Log($"skills[{(int)data.Slot}].ID : {_skills[(int)data.Slot].ID}");
            return;
        }

        SkillBase skill = Instantiate(data.Prefab);
        if (skill == null) 
        {
            Debug.LogError($"Can't find SkillBase Component! / ID : {skillID}");
            return;
        }

        // ��ų �߰�
        _skills[(int)data.Slot] = skill;
    }

    public void DoSkill(Enums.ESkillSlot slot)
    {
        _skills[(int)slot].DoSkill();
    }

    public void StopSkill(Enums.ESkillSlot slot)
    {
        _skills[(int)slot].StopSkill();
    }
}
