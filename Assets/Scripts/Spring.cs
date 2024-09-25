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

            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player?.SpringJump(jumpPower);
        }
    }
}
