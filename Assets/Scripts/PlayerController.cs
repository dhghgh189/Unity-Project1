using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;

    [SerializeField] float moveSpeed;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] KeyCode jumpKey;

    Rigidbody2D _rb;

    bool _isGrounded = true;
    float x;

    // test
    [SerializeField] float maxJumpTime;
    float _elapsedTime;
    bool _needToJump = false;

    bool _tryToJump = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();

        //Jump2();

        // max fall speed clamp
        if (_rb.velocity.y < -maxFallSpeed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -maxFallSpeed);
        }
    }

    void Update()
    {
        // check ground
        if (_rb.velocity.y != 0)
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

        // check exception (tryToJump)
        if (Input.GetKeyUp(jumpKey))
        {
            if (_tryToJump == true && _rb.velocity.y > 0)    
            {
                _tryToJump = false;
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
            }
        }
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, whatIsGround);
        if (hit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
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
}
