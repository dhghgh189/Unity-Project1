using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    void Update()
    {
        // test
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.StartGame(0);
        }
    }
}
