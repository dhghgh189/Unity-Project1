using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.ChangeState(GameManager.EState.Init);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            ExSceneManager.Instance.LoadScene("Select");
        }
    }
}
