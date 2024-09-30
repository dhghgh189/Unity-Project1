using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : SkillBase
{
    protected BossController _owner;
    public BossController Owner { get { return _owner; } }

    private void Start()
    {
        _owner = _ownerObject.GetComponent<BossController>();
        if (_owner == null)
        {
            Debug.LogError($"BossSkill Owner type incorrect!");
        }
    }
}
