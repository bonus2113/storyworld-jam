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

    private CandleManager m_CandleManager = null;

    public int MissedSymbols { get { return missedSymbols; } }

    [SerializeField] 
    private float spawnTimeDifference = 5;

    private float currentTime = 0;
    private float currentSpawnTime = 0;
    private bool isPlaying = false;

    private int missedSymbols = 0;
    private int hitSymbols = 0;

    public void PlaySequence(TimelineSequence sequence, float difficulty = 0)
    {
        isPlaying = true;
        currentTime = -spawnTimeDifference;
        currentSpawnTime = 0;
        CurrentSequence = sequence;
        CurrentSequence.StartPlayback();
        missedSymbols = 0;
        hitSymbols = 0;
        TimeLineSymbol.Difficulty = difficulty;
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

    public float GetHeuristicValue()
    {
        var val = -missedSymbols*0.05f;
        Debug.Log("WizardHero heuristic value: " + val);
        return val;
    }

    private void Start()
    {
        for (int i = 0; i < timeLines.Length; i++)
        {
            timeLines[i].ActivateKey = activateKeys[i];
            timeLines[i].HitSymbol += TimingManager_HitSymbol;
            timeLines[i].MissedSymbol += TimingManager_MissedSymbol;
        }

        this.m_CandleManager = GameObject.FindObjectOfType<CandleManager>();
        if (this.m_CandleManager == null)
        {
            Debug.LogWarning("Null candlemanager.");
            Destroy(this);
        }
    }

    void TimingManager_MissedSymbol()
    {
        if(!isPlaying)
            return;

        missedSymbols++;
        var pos = this.m_CandleManager.ExtinguishCandle();
        AudioManager.PlaySound("RuneFail", pos, 0.1f);
    }

    void TimingManager_HitSymbol()
    {
        hitSymbols++;
        AudioManager.PlaySound("RuneSuccess", Camera.main.transform.position, 0.1f);
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
