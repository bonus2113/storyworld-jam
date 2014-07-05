using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimelineSequence : ScriptableObject
{
    [SerializeField]
    private List<SequencePoint> Sequence;

    private int currentIndex = 0;

    private bool wasSorted = false;

    public void StartPlayback()
    {
        currentIndex = 0;
        if (!wasSorted)
        {
            Sequence = Sequence.OrderBy(s => s.Time).ToList();
            wasSorted = true;
        }
    }

    public void Restart()
    {
        currentIndex = 0;
    }

    public float GetLength()
    {
        if (Sequence.Count == 0)
        {
            return 0;
        }

        if (!wasSorted)
        {
            Sequence = Sequence.OrderBy(s => s.Time).ToList();
            wasSorted = true;
        }

        return Sequence[Sequence.Count - 1].Time;
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