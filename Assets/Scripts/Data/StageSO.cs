using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new StageSO", menuName = "Scriptable Objects/StageSO")]
public class StageSO : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public int BossID;
    public Vector2 PlayerPos;
    public Vector2 BossPos;

    [Header("Stage Map")]
    public GameObject MapPrefab;
}
