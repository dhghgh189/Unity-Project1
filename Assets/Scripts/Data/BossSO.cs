using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new BossSO", menuName = "Scriptable Objects/BossSO")]
public class BossSO : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public string Name;
    [TextArea(1, 3)] public string Description;

    [Header("Shape")]
    public Vector3 Scale;
    public RuntimeAnimatorController AnimController;
    public Vector2 ColliderOffset;
    public Vector2 ColliderSize;
    public Sprite Icon;
    public bool needToFilp = false;
    public bool isKinematic;

    [Header("Stats")]
    public float MaxHP;
    public float MaxMP;
    // 초당 마나 젠 양
    public float MPGenPerSecond;

    [Header("Reward")]
    public int RewardCoinAmount;

    [Header("Skills")]
    public List<int> useSkillsID;
}
