using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    [SerializeField] BossController boss;
    int iStage;
    StageSO _stageData;
    GameObject _map;

    private void Awake()
    {
        // ¸Ê »ý¼º
        iStage = GameManager.Instance.Data.CurrentStageIndex;
        _stageData = DataManager.Instance.StageDict[iStage];
        _map = Instantiate(_stageData.MapPrefab);
    }

    // test
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Can't find Player!");
            return;
        }

        player.transform.position = _stageData.PlayerPos;

        FollowCam cam = Camera.main.GetComponent<FollowCam>();
        if (cam != null)
        {
            cam.SetTarget(player.transform);

            Transform camBound = _map.transform.Find("CameraBound");
            if (camBound != null)
                cam.SetBound(camBound.gameObject);
        }

        GameManager.Instance.ChangeState(GameManager.EState.InGame);

        boss.transform.position = _stageData.BossPos;
        boss.SetInfo(_stageData.BossID);
    }

    // test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Preparation");
        }
    }
}
