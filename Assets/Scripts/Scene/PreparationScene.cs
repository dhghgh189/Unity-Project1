using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreparationScene : MonoBehaviour
{
    [SerializeField] Vector3 playerStartPos;

    void Start()
    {      
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerStartPos;

            FollowCam cam = Camera.main.GetComponent<FollowCam>();
            if (cam != null)
            {
                cam.SetTarget(player.transform);
            }

            GameManager.Instance.ChangeState(GameManager.EState.InGame);
        }
        else
        {
            Debug.LogError("Can't find Player!");
        }
    }

    // test
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
