using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman_Move : SkillBase
{
    [SerializeField] float speed;
    [SerializeField] float moveTime;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float checkDistance;

    float _elapsedTime;

    Coroutine _moveRoutine;

    float _radius;

    private void Start()
    {
        _radius = _owner.Collider.size.x;
    }

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

        _moveRoutine = StartCoroutine(MoveRoutine(toTarget.normalized));
    }

    IEnumerator MoveRoutine(Vector2 normalizedDir)
    {
        _elapsedTime = 0;

        while (true)
        {
            _owner.Rb.velocity = normalizedDir * speed;
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= moveTime)
                break;

            Vector3 offset = Vector2.up * _radius;
            Debug.DrawRay(_owner.transform.position + offset, normalizedDir * checkDistance, Color.red);
            if (Physics2D.Raycast(_owner.transform.position + offset, normalizedDir, checkDistance, whatIsGround).collider == null)
            {
                Debug.Log("collider null");
                break;
            }

            yield return null;
        }

        base.DoSkill();

        StopSkill();

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
        _owner.Rb.velocity = Vector2.zero;
    }
}
