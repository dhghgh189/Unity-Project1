using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BossStateWait : BossState
{
    // skill 사용 후 잠시 대기하는 상태
    // owner에서 설정한 waittime 만큼 대기한다음 idle로 돌아간다.

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
        // wait가 끝날때 까지는 처리하지 않는다.
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
        // 대기
        yield return _waitTime;

        // 대기 종료
        _waitRoutine = null;
    }
}
