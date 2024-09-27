using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum EState { None, Start, InGame, Paused }

    EState _curState = EState.None;
    public EState CurState { get { return _curState; } }

    GameData _data = new GameData();
    public GameData Data { get { return _data; } }

    public UnityAction OnGamePause;
    public UnityAction OnGameResume;

    protected override void Init()
    {
        
    }

    void Update()
    {
        switch (_curState)
        {
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
            case EState.Paused:
                {
                    UpdatePaused();
                }
                break;
        }
    }

    public void ChangeState(EState state)
    {
        _curState = state;
    }

    public void StartGame(int selectIndex)
    {
        _data.SelectedPlayerID = selectIndex;
        _curState = EState.Start;
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
            _curState = EState.None;
            return;
        }

        _data.PlayerData = playerData;

        SceneManager.LoadScene("Preparation");
    }

    void UpdateInGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void UpdatePaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
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
        _curState = EState.InGame;
        Time.timeScale = 1;
    }
}
