using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TimingManager : MonoBehaviour
{
    [SerializeField]
    private TimeLine[] timeLines;

    [SerializeField]
    private KeyCode[] activateKeys;

    public TimelineSequence CurrentSequence = null;

    [SerializeField] 
    private float spawnTimeDifference = 5;

    private float currentTime = 0;
    private float currentSpawnTime = 0;
    private bool isPlaying = false;

    private int missedSymbols = 0;
    private int hitSymbols = 0;

    public void PlaySequence(TimelineSequence sequence)
    {
        isPlaying = true;
        currentTime = -spawnTimeDifference;
        currentSpawnTime = 0;
        CurrentSequence = sequence;
        CurrentSequence.StartPlayback();
        missedSymbols = 0;
        hitSymbols = 0;
    }

    public void StopPlayback()
    {
        isPlaying = false;
        currentTime = 0;
        currentSpawnTime = 0;

        if (CurrentSequence != null)
        {
            CurrentSequence.Restart();
        }

        CurrentSequence = null;
        missedSymbols = 0;
        hitSymbols = 0;
    }

    public bool HasSequence()
    {
        return CurrentSequence != null;
    }

    private void Start()
    {
        for (int i = 0; i < timeLines.Length; i++)
        {
            timeLines[i].ActivateKey = activateKeys[i];
            timeLines[i].HitSymbol += TimingManager_HitSymbol;
            timeLines[i].MissedSymbol += TimingManager_MissedSymbol;
        }
    }

    void TimingManager_MissedSymbol()
    {
        missedSymbols++;
    }

    void TimingManager_HitSymbol()
    {
        hitSymbols++;
    }

	private void Update () 
    {
	    if (isPlaying && CurrentSequence != null)
	    {
	        currentTime += Time.deltaTime;
            currentSpawnTime += Time.deltaTime;
            var triggeredRunes = CurrentSequence.AdvanceTimeTo(currentSpawnTime);
            SpawnRunes(triggeredRunes);

	        if (currentSpawnTime > CurrentSequence.GetLength())
	        {
	            CurrentSequence.Restart();
	            currentSpawnTime = 0;
	        }
	    }
	}

    private void SpawnRunes(List<SequencePoint> runes)
    {
        foreach (var sequencePoint in runes)
        {
            timeLines[sequencePoint.TimelineIndex].SpawnRune(sequencePoint.RuneType);
        }
    }
}
