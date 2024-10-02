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
    [SerializeField] List<StageSO> listStageSO;

    Dictionary<int, PlayerSO> playerDict = new Dictionary<int, PlayerSO>();
    Dictionary<int, SkillSO> skillDict = new Dictionary<int, SkillSO>();
    Dictionary<int, BossSO> bossDict = new Dictionary<int, BossSO>();
    Dictionary<int, StageSO> stageDict = new Dictionary<int, StageSO>();

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

    public Dictionary<int, StageSO> StageDict
    {
        get { return stageDict; }
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
        LoadStageData();

        Debug.Log("Data Load Complete");
    }

    void LoadPlayerData()
    {
        PlayerSO playerData;
        for (int i = 0; i < listPlayerSO.Count; i++)
        {
            playerData = listPlayerSO[i];
            if (playerDict.ContainsKey(playerData.ID))
            {
                Debug.LogError($"Player ID duplicate! / ID : {playerData.ID}");
                Debug.LogError($"old value : {playerDict[playerData.ID].Name}");
                Debug.LogError($"new value : {playerData.Name}");
                Debug.LogError("Please Check Data!");
                return;
            }
            playerDict.Add(playerData.ID, playerData);
        }
    }

    void LoadSkillData()
    {
        SkillSO skillData;
        for (int i = 0; i < listSkillSO.Count; i++)
        {
            skillData = listSkillSO[i];
            if (skillDict.ContainsKey(skillData.ID))
            {
                Debug.LogError($"Skill ID duplicate! / ID : {skillData.ID}");
                Debug.LogError($"old value : {skillDict[skillData.ID].Name}");
                Debug.LogError($"new value : {skillData.Name}");
                Debug.LogError("Please Check Data!");
                return;
            }
            skillDict.Add(skillData.ID, skillData);
        }
    }

    void LoadBossData()
    {
        BossSO bossData;
        for (int i = 0; i < listBossSO.Count; i++)
        {
            bossData = listBossSO[i];
            if (bossDict.ContainsKey(bossData.ID))
            {
                Debug.LogError($"Boss ID duplicate! / ID : {bossData.ID}");
                Debug.LogError($"old value : {bossDict[bossData.ID].Name}");
                Debug.LogError($"new value : {bossData.Name}");
                Debug.LogError("Please Check Data!");
                return;
            }
            bossDict.Add(bossData.ID, bossData);
        }
    }

    void LoadStageData()
    {
        StageSO stageData;
        for (int i = 0; i < listStageSO.Count; i++)
        {
            stageData = listStageSO[i];
            if (stageDict.ContainsKey(stageData.ID))
            {
                Debug.LogError($"Stage ID duplicate! / ID : {stageData.ID}");
                Debug.LogError("Please Check Data!");
                return;
            }
            stageDict.Add(stageData.ID, stageData);
        }
    }
}
