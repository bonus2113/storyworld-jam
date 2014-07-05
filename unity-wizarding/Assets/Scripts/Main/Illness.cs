using UnityEditor;
using UnityEngine;
using System.Collections;

public class Illness : ScriptableObject 
{
    [MenuItem("Assets/Create/Illness")]
    public static void Create()
    {
        AssetUtils.CreateAsset(typeof(Illness), "Illness", "Assets/Illness.asset", true);
    }

    public string Name;

    public string Description;

    public GameObject HeadPrefab;
    public GameObject ArmPrefab;
    public GameObject BodyPrefab;
    public GameObject LegPrefab;

    public Spell CuringSpell;
}
