using UnityEngine;

public class Spell : ScriptableObject 
{
    public string Name;
    public string Description;

    public Texture2D Icon;

    public RitualInfo Ritual;
    public TimelineSequence Sequence;
}
