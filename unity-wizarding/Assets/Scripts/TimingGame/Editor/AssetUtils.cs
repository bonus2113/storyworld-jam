using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

public class AssetUtils : MonoBehaviour 
{
    [MenuItem("Assets/Create/Timeline Sequence")]
    public static void CreateTimelineSequence()
    {
        AssetUtils.CreateAsset(typeof(TimelineSequence), "timelineSequence", "Assets/timelineSequence.asset", true);
    }

    [MenuItem("Assets/Create/Spellbook")]
    public static void CreateSpellbook()
    {
        AssetUtils.CreateAsset(typeof(SpellBookInfo), "Spellbook", "Assets/Spellbook.asset", true);
    }

    [MenuItem("Assets/Create/Ritual Information")]
    public static void CreateRitual()
    {
        AssetUtils.CreateAsset(typeof(RitualInfo), "RitualInfo", "Assets/RitualInfo.asset", true);
    }

    [MenuItem("Assets/Create/Spell")]
    public static void CreateSpell()
    {
        AssetUtils.CreateAsset(typeof(Spell), "Spell", "Assets/Spell.asset", true);
    }

    [MenuItem("Assets/Create/Illness")]
    public static void CreateIllness()
    {
        AssetUtils.CreateAsset(typeof(Illness), "Illness", "Assets/Illness.asset", true);
    }

    /// <summary> will create the asset file if it does not exist and add the object and save the file</summary>
    public static void AddObjectToAssetFile(Object obj, string assetFile)
    {
        if (!File.Exists(assetFile))
        {
            AssetDatabase.CreateAsset(obj, assetFile);
        }
        else
        {
            AssetDatabase.AddObjectToAsset(obj, assetFile);
            AssetDatabase.ImportAsset(assetFile); // force a save
        }
    }

    /// <summary>
    /// will create a new asset of the type, save it and return a reference to it. 
    /// assetFile should be a relative path to the file to store the new asset in
    /// if createNewIfExist then a unique name will be generated if the file allready
    /// exist and the asset will be saved there
    /// </summary>
    public static UnityEngine.Object CreateAsset(System.Type type, string objectName, string assetFile, bool createNewIfExist)
    {
        Object obj = ScriptableObject.CreateInstance(type);
        obj.name = objectName;

        if (!File.Exists(assetFile))
        {
            AssetDatabase.CreateAsset(obj, assetFile);
        }
        else
        {
            if (createNewIfExist)
            {
                string fn = AssetDatabase.GenerateUniqueAssetPath(assetFile);
                AssetDatabase.CreateAsset(obj, fn);
            }
            else
            {
                AssetDatabase.AddObjectToAsset(obj, assetFile);
                AssetDatabase.ImportAsset(assetFile); // force a save
            }
        }

        return obj;
    }
}
