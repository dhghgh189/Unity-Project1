using System.Buffers;
using System.Collections;
using System.Collections.Generic;
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
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
