using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    // game events
    public enum EEvents
    {
        // Upgrade
        UpgradeAttackPoint,
        UpgradeMaxHP,
        UpgradeUtil,

        // Change
        ChangedHP,
        ChangedMP,
    }

    public enum EUpgradeType
    {
        AttackPoint,
        HP,
        Util
    }
}
