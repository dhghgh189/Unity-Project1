using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform leftFeet;
    [SerializeField] Transform rightFeet;

    [SerializeField] float moveSpeed;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] KeyCode jumpKey;

    Rigidbody2D _rb;
    Animator _anim;
    CapsuleCollider2D _collider;
    PlayerData _data;
    SpriteRenderer _sr;

    int _currentAnim;
    bool _isGrounded = true;
    bool _tryToJump = false;
    float x;

    public PlayerData Data { get { return _data; } }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _sr = GetComponent<SpriteRenderer>();

        SetData();
    }

    void SetData()
    {
        GameData gameData = GameManager.Instance.Data;

        // shape
        _anim.runtimeAnimatorController = gameData.PlayerData.AnimController;
        _collider.offset = gameData.PlayerData.ColliderOffset;
        _collider.size = gameData.PlayerData.ColliderSize;
        _sr.flipX = gameData.PlayerData.NeedToFlip;

        // SetStat
        gameData.PlayerData.SetStat();

        _data = gameData.PlayerData;
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
        switch (type)
        {
            case Enums.EUpgradeType.AttackPoint:
                _data.Attack += Define.UPGRADE_ATTACK_AMOUNT;
                break;
            case Enums.EUpgradeType.HP:
                _data.MaxHP += Define.UPGRADE_HP_AMOUNT;
                break;
            case Enums.EUpgradeType.Util:
                _data.UtilAmount += Define.UPGRADE_UTIL_AMOUNT;
                break;
        }
    }
}
