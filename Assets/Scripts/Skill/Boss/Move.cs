using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SkillBase
{
    [SerializeField] float speed;
    [SerializeField] float moveTime;

    float _elapsedTime;

    Coroutine _moveRoutine;

    public override void DoSkill()
    {
        Vector3 toTarget = _owner.Target.transform.position - _owner.transform.position;

        float direction = toTarget.x < 0 ? -1 : 1;
        if (direction != _owner.Direction)
            _owner.transform.localScale = new Vector3(_owner.transform.localScale.x * -1, _owner.transform.localScale.y, _owner.transform.localScale.z);

        if (_moveRoutine != null)
        {
            StopCoroutine(_moveRoutine);
            _moveRoutine = null;
        }

        _moveRoutine = StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        _elapsedTime = 0;

        while (true)
        {
            // test
            _owner.Rb.velocity = new Vector2(_owner.Direction * speed, _owner.Rb.velocity.y);
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= moveTime)
                break;

            yield return null;
        }

        base.DoSkill();

        BossController boss = _owner.GetComponent<BossController>();
        if (boss == null)
        {
            Debug.LogError("_owner is not boss!");
            yield break;
        }

        boss.ChangeState(BossController.State.Wait);
    }

    public override void StopSkill()
    {
        
    }
}
