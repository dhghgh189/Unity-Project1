using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData
{
    #region Info
    int _id;
    string _name;
    string _description;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    #endregion

    #region Shape
    RuntimeAnimatorController _animController;
    Vector2 _colliderOffset;
    Vector2 _colliderSize;
    bool _needToFlip;

    public RuntimeAnimatorController AnimController { get { return _animController; } }
    public Vector2 ColliderOffset { get { return _colliderOffset; } }
    public Vector2 ColliderSize { get { return _colliderSize; } }
    public bool NeedToFlip { get { return _needToFlip; } }
    #endregion

    #region Stats
    ///////////////////////
    //    can upgrade    //
    //////////////////////
    float _attack;
    float _maxHP;
    float _utilAmount;
    //////////////////////
    int _attackLevel;
    int _hpLevel;
    int _utilLevel;
    /// //////////////////
    float _maxMP;
    float _mp;

    public UnityAction<Enums.EEvents, int> OnUpgradeStat;
    public UnityAction<Enums.EEvents, float, float> OnChangedStat;

    // player health 연동을 위한 전용 이벤트
    public UnityAction<float> OnHealthEvent;

    public int AttackLevel 
    { 
        get { return _attackLevel; }
        set
        {
            _attackLevel++;

            // calc attack power (percent)
            // current attack * (current attack level * percent per level)
            float attackIncrease = _attack * (_attackLevel * Define.upgradeInfos[(int)Enums.EUpgradeType.AttackPoint].amount);
            _attack += attackIncrease;

            OnUpgradeStat?.Invoke(Enums.EEvents.UpgradeAttackPoint, _attackLevel);
        }
    }
    public float Attack 
    { 
        get { return _attack; }
    }

    public int HPLevel 
    { 
        get { return _hpLevel; } 
        set
        {
            _hpLevel++;

            // calc maxHp
            MaxHP += Define.upgradeInfos[(int)Enums.EUpgradeType.HP].amount;
            OnUpgradeStat?.Invoke(Enums.EEvents.UpgradeMaxHP, _hpLevel);
        }
    }
    public float MaxHP 
    { 
        get { return _maxHP; }
        private set { _maxHP = value; OnHealthEvent?.Invoke(_maxHP); }
    }

    public int UtilLevel
    {
        get { return _utilLevel; }
        set
        {
            _utilLevel++;

            // calc util
            _utilAmount += Define.upgradeInfos[(int)Enums.EUpgradeType.Util].amount;

            OnUpgradeStat?.Invoke(Enums.EEvents.UpgradeUtil, _utilLevel);
        }
    }
    public float UtilAmount
    {
        get 
        {
            // util amount는 쿨다운, 마나 리젠 상황에 맞춰 별도로 사용한다.
            // 쿨다운에 적용시 : 쿨타임 - (쿨타임 * utilAmount),
            // 마나 리젠에 적용시 : 마나 리젠 수치 * (마나 리젠 수치 * utilAmount) 
            return _utilAmount;
        }
    }

    public float MP
    {
        get { return _mp; }
        set
        {
            _mp = value;
            OnChangedStat?.Invoke(Enums.EEvents.ChangedMP, _mp, MaxMP);
        }
    }

    public float MaxMP { get { return _maxMP; } }
    #endregion

    // execute only once
    public bool SetInitData(int dataID)
    {
        if (DataManager.Instance.PlayerDict.TryGetValue(dataID, out PlayerSO data) == false)
        {
            Debug.LogError($"Player Set Data Failed... / ID : {dataID}");
            Debug.LogError("Please Check data");
            return false;
        }

        // info
        _id = data.ID;
        _name = data.Name;
        _description = data.Description;

        // shape
        _animController = data.AnimController;
        _colliderOffset = data.ColliderOffset;
        _colliderSize = data.ColliderSize;
        _needToFlip = data.needToFilp;

        // stat
        _attack = data.Attack;
        _maxHP = data.MaxHP;
        _maxMP = data.MaxMP;
        _mp = _maxMP;
        _utilAmount = data.UtilAmount;

        return true;
    }

    public int GetLevel(Enums.EUpgradeType type)
    {
        int level = -1;

        switch (type)
        {
            case Enums.EUpgradeType.AttackPoint:
                level = _attackLevel;
                break;
            case Enums.EUpgradeType.HP:
                level = _hpLevel;
                break;
            case Enums.EUpgradeType.Util:
                level = _utilLevel;
                break;
        }

        return level;
    }
}
