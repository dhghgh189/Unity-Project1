using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] List<PlayerSO> listPlayerSO;

    Dictionary<int, PlayerSO> playerDict = new Dictionary<int, PlayerSO>();

    public Dictionary<int, PlayerSO> PlayerDict
    {
        get { return playerDict; }
    }

    protected override void Init()
    {
        LoadData();
    }

    void LoadData()
    {
        LoadPlayerData();

        Debug.Log("Data Load Complete");
    }

    void LoadPlayerData()
    {
        PlayerSO playerData;
        for (int i = 0; i < listPlayerSO.Count; i++)
        {
            playerData = listPlayerSO[i];
            playerDict.Add(playerData.ID, playerData);
        }
    }
}
