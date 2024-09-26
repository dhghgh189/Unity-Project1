using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Update()
    {
        // test
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Select");
        }
    }
}
