using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);

        GameManager.Instance.OnGamePause += ShowMenu;
        GameManager.Instance.OnGameResume += ShowMenu;
    }

    public void ShowMenu()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    public void OnClickedBtnTitle()
    {
        ExSceneManager.Instance.LoadScene("Title");
    }

    public void OnClikedBtnQuit()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePause -= ShowMenu;
        GameManager.Instance.OnGameResume -= ShowMenu;
    }
}
