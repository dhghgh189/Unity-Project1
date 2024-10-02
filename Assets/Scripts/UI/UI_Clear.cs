using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Clear : MonoBehaviour
{
    public void OnClickedBtnTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnClickedBtnQuit()
    {
        Application.Quit();
    }
}
