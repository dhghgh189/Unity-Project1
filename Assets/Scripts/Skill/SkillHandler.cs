using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    // 전체 스킬 저장
    List<SkillBase> _allSkills = new List<SkillBase>();
    Creature _owner;

    // 플레이어 전용
    SkillBase[] _playerSkills = new SkillBase[(int)Enums.ESkillSlot.PlayerSkillLength];

    // 보스 전용
    SkillBase _ultimate;

    public List<SkillBase> AllSkills { get { return _allSkills; } }
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
                    for (int i = 0; i < _allSkills.Count; i++)
                    {
                        if (_allSkills[i].BaseDamage > 0)
                        {
                            _allSkills[i].Damage = _allSkills[i].BaseDamage + (_allSkills[i].BaseDamage * attackPercent);
                        }                       
                    }
                }
                break;
            case Enums.EEvents.UpgradeUtil:
                {
                    float utilPercent = player.Data.UtilAmount / 100f;
                    for (int i = 0; i < _allSkills.Count; i++)
                    {
                        if (_allSkills[i].BaseCoolTime - (_allSkills[i].BaseCoolTime * utilPercent) < _allSkills[i].MinCoolTime)
                        {
                            _allSkills[i].CoolTime = _allSkills[i].MinCoolTime;
                        }
                        else
                        {
                            _allSkills[i].CoolTime = _allSkills[i].BaseCoolTime - (_allSkills[i].BaseCoolTime * utilPercent);
                        }
                    }
                }
                break;
        }
    }

    public void AddSkill(int skillID)
    {
        // ID 검사
        if (DataManager.Instance.SkillDict.TryGetValue(skillID, out SkillSO data) == false)
        {
            Debug.LogError($"SkillHandler AddSkill failed... / ID : {skillID}");
            Debug.LogError("Please Check data");
            return;
        }

        // 플레이어 스킬 슬롯 중복 검사
        if (data.Slot < Enums.ESkillSlot.PlayerSkillLength &&
            _playerSkills[(int)data.Slot] != null)
        {
            Debug.LogWarning($"{data.Slot} is Duplicated! / Add ID : {skillID}");
            Debug.Log($"skills[{(int)data.Slot}].ID : {_allSkills[(int)data.Slot].ID}");
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

        // 스킬 추가
        switch (data.Slot)
        {
            case Enums.ESkillSlot.BossUltimate:
                _ultimate = skill;
                break;
            case Enums.ESkillSlot.PlayerSkill1:
            case Enums.ESkillSlot.PlayerSkill2:
            case Enums.ESkillSlot.PlayerSkill3:
                _playerSkills[(int)data.Slot] = skill;
                break;
        }

        // 전체 스킬 목록에 추가
        _allSkills.Add(skill);
    }

    public SkillBase GetRandomSkill()
    {
        if (_allSkills.Count <= 0)
            return null;

        int index = Random.Range(0, _allSkills.Count);
        return _allSkills[index];
    }

    // 플레이어의 스킬 사용 함수
    public void DoSkill(Enums.ESkillSlot slot)
    {
        // 슬롯에 스킬이 존재하는지 비교
        if (_playerSkills[(int)slot] == null)
        {
            Debug.Log($"{slot} is null!");
            return;
        }

        if (_playerSkills[(int)slot].CurrentCoolTime > 0)
            return;

        // 스킬사용에 필요한 마나가 부족한 경우
        if (_owner.MP < _playerSkills[(int)slot].MPAmount)
            return;

        _playerSkills[(int)slot].DoSkill();
    }

    // 플레이어 스킬 중지 함수
    public void StopSkill(Enums.ESkillSlot slot)
    {
        if (_playerSkills[(int)slot] == null)
            return;

        _playerSkills[(int)slot].StopSkill();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Data.PlayerData.OnUpgradeStat -= UpdateStat;
        }
    }
}
