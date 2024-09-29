using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : SkillBase
{
    protected PlayerController _owner;
    public PlayerController Owner { get { return _owner; } }

    private void Start()
    {
        _owner = _ownerObject.GetComponent<PlayerController>();
        if (_owner == null)
        {
            Debug.LogError($"PlayerSkill Owner type incorrect!");
        }
    }
}
