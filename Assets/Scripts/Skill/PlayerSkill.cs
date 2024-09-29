using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : SkillBase
{
    PlayerController _owner;

    public PlayerController Owner { get { return _owner; } }

    private void Awake()
    {
        _owner = GetComponent<PlayerController>();
    }
}
