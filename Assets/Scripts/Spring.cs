using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] float jumpPower;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // check surface normal
            if (other.contacts[0].normal.y > -1f)
                return;

            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb?.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
