using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] List<PlayerSO> listPlayerSO;
    [SerializeField] List<SkillSO> listSkillSO;

    Dictionary<int, PlayerSO> playerDict = new Dictionary<int, PlayerSO>();
    Dictionary<int, SkillSO> skillDict = new Dictionary<int, SkillSO>();

    public Dictionary<int, PlayerSO> PlayerDict
    {
        get { return playerDict; }
    }

    public Dictionary<int, SkillSO> SkillDict
    {
        get { return skillDict; }
    }

    protected override void Init()
    {
        LoadData();
    }

    void LoadData()
    {
        LoadPlayerData();
        LoadSkillData();

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

    void LoadSkillData()
    {
        SkillSO skillData;
        for (int i = 0; i < listSkillSO.Count; i++)
        {
            skillData = listSkillSO[i];
            skillDict.Add(skillData.ID, skillData);
        }
    }
}
