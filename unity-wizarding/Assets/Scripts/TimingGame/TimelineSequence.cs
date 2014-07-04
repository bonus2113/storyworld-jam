using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class TimelineSequence : ScriptableObject
{
    [MenuItem("Assets/Create/Timeline Sequence")]
    public static void Create()
    {
        AssetUtils.CreateAsset(typeof(TimelineSequence), "timelineSequence", "Assets/timelineSequence.asset", true);
    }

    [SerializeField]
    private List<SequencePoint> Sequence;

    private int currentIndex = 0;

    public void StartPlayback()
    {
        currentIndex = 0;
        Sequence = Sequence.OrderBy(s => s.Time).ToList();
    }

    public List<SequencePoint> AdvanceTimeTo(float time)
    {
        var passedElements = new List<SequencePoint>();
        while (currentIndex < Sequence.Count && Sequence[currentIndex].Time <= time)
        {
            passedElements.Add(Sequence[currentIndex]);
            currentIndex++;
        }
        return passedElements;
    }
}

[System.Serializable]
public struct SequencePoint
{
    public float Time;
    public RuneType RuneType;
    public int TimelineIndex;
}