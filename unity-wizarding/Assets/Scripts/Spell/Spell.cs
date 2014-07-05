using UnityEditor;
using UnityEngine;
using System.Collections;

public class Spell : ScriptableObject 
{
    [MenuItem("Assets/Create/Spell")]
    public static void Create()
    {
        AssetUtils.CreateAsset(typeof(Spell), "Spell", "Assets/Spell.asset", true);
    }

    public string Name;
    public string Description;

    public Texture2D Icon;

    public RitualInfo Ritual;
    public TimelineSequence Sequence;
}
