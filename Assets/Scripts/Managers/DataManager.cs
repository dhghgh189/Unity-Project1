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
    [SerializeField] List<BossSO> listBossSO;

    Dictionary<int, PlayerSO> playerDict = new Dictionary<int, PlayerSO>();
    Dictionary<int, SkillSO> skillDict = new Dictionary<int, SkillSO>();
    Dictionary<int, BossSO> bossDict = new Dictionary<int, BossSO>();

    public Dictionary<int, PlayerSO> PlayerDict
    {
        get { return playerDict; }
    }

    public Dictionary<int, SkillSO> SkillDict
    {
        get { return skillDict; }
    }

    public Dictionary<int, BossSO> BossDict
    {
        get { return bossDict; }
    }

    protected override void Init()
    {
        LoadData();
    }

    void LoadData()
    {
        LoadPlayerData();
        LoadSkillData();
        LoadBossData();

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

    void LoadBossData()
    {
        BossSO bossData;
        for (int i = 0; i < listBossSO.Count; i++)
        {
            bossData = listBossSO[i];
            bossDict.Add(bossData.ID, bossData);
        }
    }
}
