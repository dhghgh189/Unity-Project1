using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Creature
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform leftFeet;
    [SerializeField] Transform rightFeet;

    [SerializeField] float moveSpeed;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] KeyCode jumpKey;

    [SerializeField] KeyCode[] skillKeys = new KeyCode[(int)Enums.ESkillSlot.PlayerSkill_Max];

    [SerializeField ]Hitbox hitbox;

    //Rigidbody2D _rb;
    Animator _anim;
    CapsuleCollider2D _collider;
    PlayerData _data;
    SpriteRenderer _sr;
    SkillHandler _skill;

    int _currentAnim;
    bool _isGrounded = true;
    bool _tryToJump = false;
    float x;

    public PlayerData Data { get { return _data; } }
    public override float MaxMP { get { return _data.MaxMP; } }
    public override float MP { get { return _data.MP; } set { _data.MP = value; } }

    protected override void Awake()
    {
        base.Awake();

        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _skill = GetComponent<SkillHandler>();

        SetData();
    }

    void SetData()
    {
        GameData gameData = GameManager.Instance.Data;

        // shape
        _anim.runtimeAnimatorController = gameData.PlayerData.AnimController;
        _collider.offset = gameData.PlayerData.ColliderOffset;
        _collider.size = gameData.PlayerData.ColliderSize;

        hitbox.Init(_collider.offset, _collider.size);

        _sr.flipX = gameData.PlayerData.NeedToFlip;

        _data = gameData.PlayerData;

        _skill.SetOwner(this);

        List<int> useSkillsID = DataManager.Instance.PlayerDict[_data.ID].useSkillsID;
        for (int i = 0; i < useSkillsID.Count; i++)
        {
            _skill.AddSkill(useSkillsID[i]);
        }

        // set health
        _maxHP = _data.MaxHP;
        HP = _maxHP;

        _data.OnHealthEvent += UpdateHP;
    }

    void FixedUpdate()
    {
        Move();

        // max fall speed clamp
        if (_rb.velocity.y < -maxFallSpeed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -maxFallSpeed);
        }
    }

    void Update()
    {
        // check ground
        GroundCheck();

        // check input
        x = Input.GetAxisRaw("Horizontal");

        // horizontal input detected
        if (!Mathf.Approximately(x, 0))
        {
            if (transform.localScale.x != x)
                Flip();
        }

        // jump input detected
        if (Input.GetKeyDown(jumpKey))
        {
            if (_isGrounded == true)
            {
                _tryToJump = true;
                Jump();
            }
        }

        // check exception(tryToJump)
        if (Input.GetKeyUp(jumpKey))
        {
            if (_tryToJump == true && _rb.velocity.y > 0)
            {
                _tryToJump = false;
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
            }
        }

        for (int i = 0; i < skillKeys.Length; i++)
        {
            if (Input.GetKeyDown(skillKeys[i]))
            {
                _skill.DoSkill((Enums.ESkillSlot)i);
            }

            if (Input.GetKeyUp(skillKeys[i])) 
            {
                _skill.StopSkill((Enums.ESkillSlot)i);
            }
        }

        if (MP < MaxMP)
        {
            MP += _data.MPGenPerSecond * Time.deltaTime;
        }

        // Animation
        UpdateAnim();
    }

    void GroundCheck()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFeet.position, Vector2.down, 0.1f, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFeet.position, Vector2.down, 0.1f, whatIsGround);

        // execute 2 raycast from real feet position
        // (for fix corner problem)
        if (leftHit.collider != null || rightHit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    void UpdateAnim()
    {
        int checkAnim = 0;

        if (_isGrounded == false)
        {
            if (_rb.velocity.y >= Mathf.Epsilon)
            {
                checkAnim = DefineAnim.HASH_JUMP;
            }
            else if (_rb.velocity.y < Mathf.Epsilon)
            {
                checkAnim = DefineAnim.HASH_FALL;
            }
        }
        else
        {
            if (Mathf.Abs(_rb.velocity.x) > 0.01f)
            {
                checkAnim = DefineAnim.HASH_RUN;
            }
            else
            {
                checkAnim = DefineAnim.HASH_IDLE;
            }
        }

        // if state changed
        if (checkAnim != _currentAnim)
        {
            _currentAnim = checkAnim;
            _anim.Play(_currentAnim);
        }       
    }

    void Move()
    {
        _rb.velocity = new Vector2(x * moveSpeed, _rb.velocity.y);
    }

    void Flip()
    {
        // flip (with scale)
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    public void SpringJump(float springPower)
    {
        _tryToJump = false;
        _rb.AddForce(Vector2.up * springPower, ForceMode2D.Impulse);
    }

    // Upgrade Player Stat
    public void UpgradeStat(Enums.EUpgradeType type)
    {
        // 업그레이드 레벨이 max인 경우 return 
        if (_data.GetLevel(type) >= Define.upgradeInfos[(int)type].maxLevel)
        {
            Debug.Log("Upgrade level is already max!");
            return;
        }

        // 코인이 부족한 경우 return
        if (GameManager.Instance.Data.Coins < Define.upgradeInfos[(int)type].price)
        {
            Debug.Log("not enough coins!");
            return;
        }

        switch (type)
        {
            case Enums.EUpgradeType.AttackPoint:
                _data.AttackLevel++;
                break;
            case Enums.EUpgradeType.HP:
                _data.HPLevel++;
                break;
            case Enums.EUpgradeType.Util:
                _data.UtilLevel++;
                break;
        }

        // 업그레이드 후 coin 차감
        GameManager.Instance.Data.Coins -= Define.upgradeInfos[(int)type].price;
    }

    // health와 data 연동을 위한 콜백
    public void UpdateHP(float maxHP)
    {
        _maxHP = maxHP;
        HP = _maxHP;
    }
}
