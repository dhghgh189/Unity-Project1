using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BossController : Creature
{
    [SerializeField] float waitTime;

    //Rigidbody2D _rb;
    Animator _anim;
    //CapsuleCollider2D _collider;
    SpriteRenderer _sr;
    SkillHandler _skill;

    int _id;
    float _collisionDamage;

    float _maxMP;
    float _mp;
    float _mpGenPerSecond;

    public enum State { Idle, Skill, Wait, Max }
    State _curState;

    BossState[] _states = new BossState[(int)State.Max];

    int _currentAnim;

    public int ID { get { return _id; } }
    public float WaitTime { get { return waitTime; } }
    public SkillHandler Skill { get { return _skill; } }
    public override float MaxMP { get { return _maxMP; } }
    public override float MP { get { return _mp; } set { _mp = value; OnChangedStat?.Invoke(Enums.EEvents.ChangedMP, _mp, _maxMP); } }
    public float CollisionDamage { get { return _collisionDamage; } }

    public UnityAction<Enums.EEvents, float, float> OnChangedStat;

    protected override void Awake()
    {
        base.Awake();

        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _skill = GetComponent<SkillHandler>();

        _states[(int)State.Idle] = new BossStateIdle(this);
        _states[(int)State.Skill] = new BossStateSkill(this);
        _states[(int)State.Wait] = new BossStateWait(this);

        // State Init
        _curState = State.Wait;
        _states[(int)_curState].OnEnter();

        GameManager.Instance.SetBoss(this);
    }

    private void Start()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Boss Can't find Player!");
            return;
        }

        _target = player;
    }

    public void SetInfo(int bossID)
    {
        if (DataManager.Instance.BossDict.TryGetValue(bossID, out BossSO data) == false)
        {
            Debug.LogError($"BossController SetInfo Failed... / ID : {bossID}");
            Debug.LogError("Please check data");
        }

        _id = data.ID;

        // shape
        _anim.runtimeAnimatorController = data.AnimController;
        _collider.offset = data.ColliderOffset;
        _collider.size = data.ColliderSize;
        _sr.flipX = data.needToFilp;

        if (data.isKinematic)
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
        }

        transform.localScale = data.Scale;

        _maxMP = data.MaxMP;
        _mp = 0;
        _mpGenPerSecond = data.MPGenPerSecond;

        _skill.SetOwner(this);

        List<int> useSkillsID = data.useSkillsID;
        for (int i = 0; i < useSkillsID.Count; i++)
        {
            _skill.AddSkill(useSkillsID[i]);
        }

        // only boss have it
        _collisionDamage = 0.5f;

        // set health
        _maxHP = data.MaxHP;
        HP = _maxHP;
    }

    void Update()
    {
        _states[(int)_curState].OnUpdate();

        if (_mp < _maxMP)
        {
            // ÃÊ´ç ¸¶³ª Á¨
            MP += _mpGenPerSecond * Time.deltaTime;
        }

        UpdateAnim();
    }

    void UpdateAnim()
    {
        int checkAnim = 0;

        switch (_curState)
        {
            case State.Idle:
            case State.Wait:
                checkAnim = DefineAnim.HASH_IDLE;
                break;
            case State.Skill:
                checkAnim = DefineAnim.HASH_SKILL;
                break;
        }

        // if state changed
        if (checkAnim != _currentAnim)
        {
            _currentAnim = checkAnim;
            _anim.Play(_currentAnim);
        }
    }

    public void ChangeState(State state)
    {
        _states[(int)_curState].OnExit();
        _curState = state;
        _states[(int)_curState].OnEnter();
    }
}
