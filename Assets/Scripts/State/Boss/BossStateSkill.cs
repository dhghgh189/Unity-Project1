using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateSkill : BossState
{
    // skill이 사용 중인 동안 대기할 상태
    // 각 bossSkill에서 스킬 시전 끝나면 wait 상태로 change 해준다

    public BossStateSkill(BossController owner) : base(owner)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
