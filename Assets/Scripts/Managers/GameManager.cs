using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum EState { Idle, Init, Start, InGame, StageClear, Paused }

    EState _curState = EState.Idle;
    public EState CurState { get { return _curState; } }

    GameData _data = new GameData();
    public GameData Data { get { return _data; } }

    public UnityAction OnGamePause;
    public UnityAction OnGameResume;

    [SerializeField] float waitTime;
    float _timer;

    PlayerController _player;
    BossController _boss;

    public PlayerController Player { get { return _player; } }
    public BossController Boss { get { return _boss; } }

    public void SetPlayer(PlayerController player)
    {
        _player = player;
        _player.OnDead += GameOver;
    }

    public void SetBoss(BossController boss)
    {
        _boss = boss;
        _boss.OnDead += StageClear;
    }

    void Update()
    {
        switch (_curState)
        {
            case EState.Init:
                {
                    UpdateInit();
                }
                break;
            case EState.Start:
                {
                    UpdateStart();
                }
                break;
            case EState.InGame:
                {
                    UpdateInGame();
                }
                break;
            case EState.StageClear:
                {
                    UpdateStageClear();
                }
                break;
            case EState.Paused:
                {
                    UpdatePaused();
                }
                break;
        }
    }

    public void ChangeState(EState state)
    {
        if (_curState != state)
            _curState = state;
    }

    public void StartGame(int selectIndex)
    {
        _data.SelectedPlayerID = selectIndex;
        _curState = EState.Start;
    }

    void UpdateInit()
    {
        Time.timeScale = 1;

        _data.PlayerData = null;
        OnGamePause = null;
        OnGameResume = null;
    }

    void UpdateStart()
    {
        // block looping
        if (_data.PlayerData != null)
            return;

        _data.Coins = 0;
        _data.CurrentStageIndex = 0;

        PlayerData playerData = new PlayerData();
        if (playerData.SetInitData(_data.SelectedPlayerID) == false)
        {
            _curState = EState.Idle;
            return;
        }

        _data.PlayerData = playerData;

        ExSceneManager.Instance.LoadScene("Preparation");
    }

    void UpdateInGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void UpdateStageClear()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            if (_data.CurrentStageIndex < DataManager.Instance.StageDict.Count-1)
            {
                _data.CurrentStageIndex++;
                ChangeState(EState.InGame);
                ExSceneManager.Instance.LoadScene("Preparation");
            }
            else
            {
                ChangeState(EState.Idle);
                ExSceneManager.Instance.LoadScene("Clear");
            }
        }
    }

    void UpdatePaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void GameOver()
    {
        ChangeState(EState.Idle);
        Time.timeScale = 0;
    }

    public void StageClear()
    {
        _data.Coins += Boss.RewardCoinAmount;
        _timer = waitTime;
        ChangeState(EState.StageClear);
    }

    public void AddCoin(int amount)
    {
        _data.Coins += amount;
    }

    public void PauseGame()
    {
        _curState = EState.Paused;
        Time.timeScale = 0;
        OnGamePause?.Invoke();
    }

    public void ResumeGame()
    {
        _curState = EState.InGame;
        Time.timeScale = 1;
        OnGameResume?.Invoke();
    }

    public void Clear()
    {
        Time.timeScale = 1;

        if (_player != null)
        {
            _player.OnDead -= GameOver;
            _player = null;
        }

        if (_boss != null)
        {
            _boss.OnDead -= StageClear;
            _boss = null;
        }
    }
}
