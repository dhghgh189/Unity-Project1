using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossStateIdle : BossState
{
    public BossStateIdle(BossController owner) : base(owner)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        SkillBase skill = null;
        // ������ ��� á���� �ñر⸦ ����
        if (_owner.MP >= _owner.MaxMP)
        {
            skill = _owner.Skill.Ultimate;
            // ����ó��
            if (skill == null)
            {
                _owner.MP = 0;
                return;
            }
        }
        else // ������ �� �� ��� �Ϲ� ��ų�� �ϳ��� ����
        {
            // �����ϰ� ��ų�� �ҷ��´�.
            skill = _owner.Skill.GetRandomSkill();
            // ����� �� ���� ��ų�̸� return �ϰ� �ٽ� �ҷ��´�.
            if (skill == null || skill.CurrentCoolTime > 0)
            {
                return;
            }
        }

        // ��ų ���·� ����
        _owner.ChangeState(BossController.State.Skill);
        // ��ų ����
        skill.DoSkill();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
