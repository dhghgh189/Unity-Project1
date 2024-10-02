using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    //SkillBase[] _skills = new SkillBase[(int)Enums.ESkillSlot.Max];
    List<SkillBase> _skills = new List<SkillBase>();
    Creature _owner;

    // ���� ����
    SkillBase _ultimate;

    public SkillBase Ultimate { get { return _ultimate; } }

    public void SetOwner(Creature owner)
    {
        _owner = owner;
        if (_owner is PlayerController)
        {
            GameManager.Instance.Data.PlayerData.OnUpgradeStat += UpdateStat;
        }
    }

    public void UpdateStat(Enums.EEvents eEvent, int level)
    {
        PlayerController player = _owner as PlayerController;
        if (player == null)
        {
            Debug.LogError("SkillHandler Owner type mismatch!");
            return;
        }

        switch (eEvent)
        {
            case Enums.EEvents.UpgradeAttackPoint:
                {
                    float attackPercent = player.Data.Attack / 100f;
                    for (int i = 0; i < _skills.Count; i++)
                    {
                        if (_skills[i].BaseDamage > 0)
                        {
                            _skills[i].Damage = _skills[i].BaseDamage + (_skills[i].BaseDamage * attackPercent);
                        }                       
                    }
                }
                break;
            case Enums.EEvents.UpgradeUtil:
                {
                    float utilPercent = player.Data.UtilAmount / 100f;
                    for (int i = 0; i < _skills.Count; i++)
                    {
                        if (_skills[i].BaseCoolTime - (_skills[i].BaseCoolTime * utilPercent) < _skills[i].MinCoolTime)
                        {
                            _skills[i].CoolTime = _skills[i].MinCoolTime;
                        }
                        else
                        {
                            _skills[i].CoolTime = _skills[i].BaseCoolTime - (_skills[i].BaseCoolTime * utilPercent);
                        }
                    }
                }
                break;
        }
    }

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

        skill.SetData(data, _owner);
        skill.transform.SetParent(gameObject.transform);

        // ��ų �߰�
        //_skills[(int)data.Slot] = skill;
        if (data.Slot == Enums.ESkillSlot.BossUltimate)
            _ultimate = skill;
        else
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

        // ��ų��뿡 �ʿ��� ������ ������ ���
        if (_owner.MP < _skills[(int)slot].MPAmount)
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

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Data.PlayerData.OnUpgradeStat -= UpdateStat;
        }
    }
}
