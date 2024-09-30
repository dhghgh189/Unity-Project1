using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SkillBase
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] Projectile fireballPrefab;
    [SerializeField] float fireAngle;

    Coroutine _fireRoutine;

    public override void DoSkill()
    {
        if (_fireRoutine != null)
            StopCoroutine(_fireRoutine);

        _fireRoutine = StartCoroutine(FireRoutine());
    }

    public override void StopSkill()
    {
        if (_fireRoutine != null)
        {
            StopCoroutine(_fireRoutine);
        }
    }

    IEnumerator FireRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_coolTime);

        while (true)
        {
            // 풀링 적용 필요
            Projectile fireball = Instantiate(fireballPrefab);
            if (fireball == null)
            {
                Debug.LogError($"{name} DoSkill Error!");
                yield break;
            }

            fireball.SetDamage(damage);

            // 설정한 발사 각도에 따라 방향 계산
            Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            dir.x *= _owner.Direction;

            // 스킬 시전자의 위치로부터 0.5만큼 우상향한 지점으로 이동
            fireball.transform.position = _owner.transform.position + ((Vector3.right * _owner.Direction) + Vector3.up) * 0.5f;
            fireball.Fire(dir, speed);

            base.DoSkill();
            yield return waitTime;
        }       
    }
}
