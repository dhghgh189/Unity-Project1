using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossStateIdle : BossState
{
    /* todo : 
         * 1-1. �����ϰ� ��ų �ϳ� �ҷ��ͼ� doskill�ϰ� skill ���·� �̵���Ű��
         * 1-2. ������ �� �� ��� ultimate �ҷ��ͼ� doskill�ϰ� skill ���·� �̵���Ű��
    */

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

            skill.DoSkill();
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

        // ��ų ����
        skill.DoSkill();
        // ��ų ���·� ����
        _owner.ChangeState(BossController.State.Skill);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
