using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    //SkillBase[] _skills = new SkillBase[(int)Enums.ESkillSlot.Max];
    List<SkillBase> _skills = new List<SkillBase>();

    // ���� ����
    SkillBase _ultimate;

    public void AddSkill(int skillID)
    {
        // ID �˻�
        if (DataManager.Instance.SkillDict.TryGetValue(skillID, out SkillSO data) == false)
        {
            Debug.LogError($"SkillHandler AddSkill failed... / ID : {skillID}");
            Debug.LogError("Please Check data");
            return;
        }

        // �ߺ� �˻�
        //if (_skills[(int)data.Slot] != null)
        if (_skills.Count > (int)data.Slot && _skills[(int)data.Slot] != null)
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

        skill.SetData(data, gameObject);
        skill.transform.SetParent(gameObject.transform);

        // ��ų �߰�
        //_skills[(int)data.Slot] = skill;
        _skills.Add(skill);
    }

    public SkillBase GetRandomSkill()
    {
        if (_skills.Count <= 0)
            return null;

        int index = Random.Range(0, _skills.Count);
        return _skills[index];
    }

    public void DoSkill(Enums.ESkillSlot slot)
    {
        //if (_skills[(int)slot] == null)
        //    return;

        // ���Կ� ��ų�� �����ϴ��� ��
        if (_skills.Count <= (int)slot)
            return;

        if (_skills[(int)slot].CurrentCoolTime > 0)
            return;

        _skills[(int)slot].DoSkill();
    }

    public void StopSkill(Enums.ESkillSlot slot)
    {
        //if (_skills[(int)slot] == null)
        //    return;

        if (_skills.Count <= (int)slot)
            return;

        _skills[(int)slot].StopSkill();
    }
}
