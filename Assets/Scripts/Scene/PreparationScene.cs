using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationScene : MonoBehaviour
{
    void Start()
    {
        Vector3 startPos = new Vector3(-8, -2, 0);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = startPos;

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
}
