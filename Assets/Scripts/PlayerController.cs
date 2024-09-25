using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool _isGrounded = true;
    bool _tryToJump = false;
    float x;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
}
