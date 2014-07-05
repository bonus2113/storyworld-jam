using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class SpellBookInfo : ScriptableObject
{
    [MenuItem("Assets/Create/Spellbook")]
    public static void Create()
    {
        AssetUtils.CreateAsset(typeof(SpellBookInfo), "Spellbook", "Assets/Spellbook.asset", true);
    }

    public List<Spell> Spells;
}
