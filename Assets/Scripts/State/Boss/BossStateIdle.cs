using System.Buffers;
using System.Collections;
using System.Collections.Generic;
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
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
