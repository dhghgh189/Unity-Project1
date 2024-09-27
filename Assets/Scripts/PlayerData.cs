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
    float _hp;
    float _maxMP;
    float _mp;

    public UnityAction<Enums.EEvents, float> OnUpgradeStat;
    public UnityAction<Enums.EEvents, float, float> OnChangedStat;

    public float Attack
    {
        get { return _attack; }
        set 
        {
            _attack = value;
            _attackLevel++;
            OnUpgradeStat?.Invoke(Enums.EEvents.UpgradeAttackPoint, _attack);
        }
    }

    public float MaxHP
    {
        get { return _maxHP; }
        set
        {
            _maxHP = value;
            _hpLevel++;
            OnUpgradeStat?.Invoke(Enums.EEvents.UpgradeMaxHP, _maxHP);
        }
    }

    public float UtilAmount
    {
        get { return _utilAmount; }
        set
        {
            _utilAmount = value;
            _utilLevel++;
            OnUpgradeStat?.Invoke(Enums.EEvents.UpgradeUtil, _utilAmount);
        }
    }

    public float HP
    {
        get { return _hp; }
        set 
        {
            _hp = value;
            OnChangedStat?.Invoke(Enums.EEvents.ChangedHP, _hp, _maxHP);
        }
    }

    public float MP
    {
        get { return _mp; }
        set
        {
            _mp = value;
            OnChangedStat?.Invoke(Enums.EEvents.ChangedMP, _mp, _maxMP);
        }
    }

    public float MaxMP { get { return _maxMP; } }
    #endregion

    // execute only once
    public bool SetInitData(int dataID)
    {
        if (DataManager.Instance.PlayerDict.TryGetValue(dataID, out PlayerSO data) == false)
        {
            Debug.LogError("Player Set Data Failed...");
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

        return true;
    }

    // execute when player instantiated
    public bool SetStat()
    {
        if (DataManager.Instance.PlayerDict.TryGetValue(_id, out PlayerSO data) == false)
        {
            Debug.LogError("Player Set Stat Failed...");
            Debug.LogError("Please Check data");
            return false;
        }

        // stat
        Attack = data.Attack;
        MaxHP = data.MaxHP;
        _maxMP = data.MaxMP;
        HP = MaxHP;
        MP = _maxMP;
        UtilAmount = data.UtilAmount;

        return true;
    }
}
