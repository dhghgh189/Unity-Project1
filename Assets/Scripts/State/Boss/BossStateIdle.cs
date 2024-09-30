using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossStateIdle : BossState
{
    /* todo : 
         * 1-1. 랜덤하게 스킬 하나 불러와서 doskill하고 skill 상태로 이동시키기
         * 1-2. 마나가 다 찬 경우 ultimate 불러와서 doskill하고 skill 상태로 이동시키기
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
        // 마나가 모두 찼으면 궁극기를 시전
        if (_owner.MP >= _owner.MaxMP)
        {
            skill = _owner.Skill.Ultimate;
            // 예외처리
            if (skill == null)
            {
                _owner.MP = 0;
                return;
            }

            skill.DoSkill();
        }
        else // 마나가 덜 찬 경우 일반 스킬중 하나를 시전
        {
            // 랜덤하게 스킬을 불러온다.
            skill = _owner.Skill.GetRandomSkill();
            // 사용할 수 없는 스킬이면 return 하고 다시 불러온다.
            if (skill == null || skill.CurrentCoolTime > 0)
            {
                return;
            }
        }

        // 스킬 시전
        skill.DoSkill();
        // 스킬 상태로 전이
        _owner.ChangeState(BossController.State.Skill);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
