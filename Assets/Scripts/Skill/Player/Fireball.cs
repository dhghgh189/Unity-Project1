using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillBase
{
    [SerializeField] float speed;
    [SerializeField] Projectile fireballPrefab;
    [SerializeField] int poolSize;
    [SerializeField] float fireAngle;

    Coroutine _fireRoutine;

    public override void SetData(SkillSO data, Creature owner)
    {
        base.SetData(data, owner);

        // pooling
        PoolManager.Instance.CreatePool(fireballPrefab.gameObject, poolSize);
    }

    public override void DoSkill()
    {
        if (_fireRoutine != null)
        {
            StopCoroutine(_fireRoutine);
            _fireRoutine = null;
        }

        _fireRoutine = StartCoroutine(FireRoutine());
    }

    public override void StopSkill()
    {
        if (_fireRoutine != null)
        {
            StopCoroutine(_fireRoutine);
            _fireRoutine = null;
        }
    }

    IEnumerator FireRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_coolTime);

        while (true)
        {
            // ������ �Һ�Ǵ� ��ų�� ����Ǵ� ��� ���
            if (_owner.MP < _mpAmount)
            {
                yield return null;
                continue;
            }

            // pooling
            Projectile fireball = PoolManager.Instance.Pop<Projectile>(fireballPrefab.gameObject);
            if (fireball == null)
            {
                Debug.LogError($"{name} DoSkill Error!");
                yield break;
            }

            fireball.SetOwner(_owner);
            fireball.SetDamage(_damage);

            // ������ �߻� ������ ���� ���� ���
            Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            dir.x *= _owner.Direction;

            // ��ų �������� ��ġ�κ��� 0.5��ŭ ������� �������� �̵�
            fireball.transform.position = _owner.transform.position + ((Vector3.right * _owner.Direction) + Vector3.up) * 0.5f;
            fireball.Fire(dir, speed);

            base.DoSkill();
            yield return waitTime;
        }
    }
}
