using UnityEngine;
using System.Collections;

public class Illness : ScriptableObject 
{
    public string Name;

    public string Description;

    public GameObject HeadPrefab;
    public GameObject ArmPrefab;
    public GameObject BodyPrefab;
    public GameObject LegPrefab;

    public Spell CuringSpell;
    public TimelineSequence Sequence;
}
