using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSO : MonoBehaviour
{
    [Header("Info")]
    public int ID;
    public string BossName;
    public float GameTime;

    [Header("Tilemap")]
    public GameObject MapPrefab;
}
