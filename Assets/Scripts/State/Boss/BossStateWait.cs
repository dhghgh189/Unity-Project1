using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateWait : BossState
{
    // skill ��� �� ��� ����ϴ� ����
    // owner���� ������ waittime ��ŭ ����Ѵ��� idle�� ���ư���.

    public BossStateWait(BossController owner) : base(owner)
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
