using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : BaseState
{
    BossController _owner;

    public BossState(BossController owner)
    {
        _owner = owner;
    }
}
