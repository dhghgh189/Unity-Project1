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
        //ChangedHP, -> HP는 따로 관리되도록 분리
        ChangedMP,
        ChangedCoin,
    }

    public enum EUpgradeType
    {
        AttackPoint,
        HP,
        Util
    }

    public enum ESkillSlot
    {
        Slot1,
        Slot2,
        Slot3,
        Max,
    }
}
