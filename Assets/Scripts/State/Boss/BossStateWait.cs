using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BossStateWait : BossState
{
    // skill ��� �� ��� ����ϴ� ����
    // owner���� ������ waittime ��ŭ ����Ѵ��� idle�� ���ư���.

    WaitForSeconds _waitTime;
    Coroutine _waitRoutine;

    public BossStateWait(BossController owner) : base(owner)
    {
        _waitTime = new WaitForSeconds(owner.WaitTime);
        _waitRoutine = null;
    }

    public override void OnEnter()
    {
        if (_waitRoutine != null)
        {
            _owner.StopCoroutine(_waitRoutine);
            _waitRoutine = null;
        }

        _waitRoutine = _owner.StartCoroutine(WaitRoutine());
    }

    public override void OnUpdate()
    {
        // wait�� ������ ������ ó������ �ʴ´�.
        if (_waitRoutine != null)
            return;

        _owner.ChangeState(BossController.State.Idle);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    IEnumerator WaitRoutine()
    {
        // ���
        yield return _waitTime;

        // ��� ����
        _waitRoutine = null;
    }
}
