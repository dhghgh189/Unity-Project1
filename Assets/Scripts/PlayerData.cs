using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
    AnimatorController _animController;
    Vector2 _colliderOffset;
    Vector2 _colliderSize;
    bool _needToFlip;

    public AnimatorController AnimController { get { return _animController; } }
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
    float _cooldownAmount;
    float _mpRegenAmount;
    //////////////////////
    
    float _hp;
    float _maxMP;
    float _mp;

    public UnityAction<Enums.EEvents, float> OnUpgradeStat;
    public UnityAction<float, float> OnChangedStat;

    public float Attack
    {
        get { return _attack; }
        set 
        {
            _attack = value;
            OnUpgradeStat?.Invoke(Enums.EEvents.ChangedAttackPoint, _attack);
        }
    }

    public float MaxHP
    {
        get { return _maxHP; }
        set
        {
            _maxHP = value;
            OnUpgradeStat?.Invoke(Enums.EEvents.ChangedMaxHP, _maxHP);
        }
    }

    public float CooldownAmount
    {
        get { return _cooldownAmount; }
        set
        {
            _cooldownAmount = value;
            OnUpgradeStat?.Invoke(Enums.EEvents.ChangedCooldownAmount, _cooldownAmount);
        }
    }

    public float MPRegenAmount
    {
        get { return _mpRegenAmount; }
        set
        {
            _mpRegenAmount = value;
            OnUpgradeStat?.Invoke(Enums.EEvents.ChangedMPRegenAmount, _mpRegenAmount);
        }
    }

    public float HP
    {
        get { return _hp; }
        set 
        {
            _hp = value;
            OnChangedStat?.Invoke(_hp, _maxHP);
        }
    }

    public float MP
    {
        get { return _mp; }
        set
        {
            _mp = value;
            OnChangedStat?.Invoke(_mp, _maxMP);
        }
    }
    #endregion

    public void SetData(PlayerSO data)
    {
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
        Attack = data.Attack;
        MaxHP = data.MaxHP;
        _maxMP = data.MaxMP;
        HP = MaxHP;
        MP = _maxMP;
        CooldownAmount = data.CooldownAmount;
        MPRegenAmount = data.MPRegenAmount;
    }
}
