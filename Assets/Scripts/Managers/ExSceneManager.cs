using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExSceneManager : Singleton<ExSceneManager>
{
    public void LoadScene(int sceneBuildIndex)
    {
        Clear();
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void LoadScene(string sceneName)
    {
        Clear();
        SceneManager.LoadScene(sceneName);
    }

    void Clear()
    {
        GameManager.Instance.Clear();
        PoolManager.Instance.Clear();
    }
}
