using System;
using UnityEngine.Events;

public class GameData
{
    int _selectedPlayerID;
    int _currentStageIndex;
    PlayerData _playerData;
    int _coins;

    public int SelectedPlayerID
    {
        get { return _selectedPlayerID; }
        set { _selectedPlayerID = value; }
    }

    public int CurrentStageIndex
    {
        get { return _currentStageIndex; }
        set { _currentStageIndex = value; }
    }

    public PlayerData PlayerData
    {
        get { return _playerData; }
        set { _playerData = value; }
    }

    public UnityEvent<int> OnChangeCoin;
    public int Coins
    {
        get { return _coins; }
        set
        {
            _coins = value;
            OnChangeCoin?.Invoke(_coins);
        }
    }
}
