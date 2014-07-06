using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RitualInfo : ScriptableObject
{
    //these are in pixels now DARIO
    public const float MAX_SYMBOL_DISTANCE = 10.0f;
    public const float MAX_CANDLE_DISTANCE = 30.0f;

    //penalty candle deficit/excess
    public const float PENALTY_PER_CANDLE = 0.2f;

    public Vector2 SymbolPosition;
    public SymbolTypes.SymbolType SymbolType;
    public List<Vector2> CandlePositions = new List<Vector2>();

    public float GetHeuristicValue(RitualInfo compareInfo)
    {
        float symbolMatch = SymbolMatchValue(compareInfo.SymbolPosition, compareInfo.SymbolType);
        float candleMatch = CandleMatchvalue(new List<Vector2>(compareInfo.CandlePositions));

        return (symbolMatch + candleMatch)/2;
    }

    private float CandleMatchvalue(List<Vector2> compPositions)
    {
        var matchCandles = new List<Vector2>(CandlePositions);
        var heuristicValues = new List<float>();

        //penalty if not 0
        int candleDifference = Mathf.Abs(compPositions.Count - matchCandles.Count);

        for (int i = matchCandles.Count - 1; i >= 0 && compPositions.Count > 0; i--)
        {
            Vector2 currentMatch = matchCandles[i];
            int closestIndex = FindClosest(currentMatch, compPositions);

            heuristicValues.Add(DistanceHeuristic(currentMatch, compPositions[closestIndex], MAX_CANDLE_DISTANCE));
            matchCandles.RemoveAt(i);
            compPositions.RemoveAt(closestIndex);
        }

        if (heuristicValues.Count != 0)
        {
            return Mathf.Clamp01(heuristicValues.Average() - PENALTY_PER_CANDLE * candleDifference);
        }
        else
        {
            return 0;
        }
    }

    private int FindClosest(Vector2 matchPos, List<Vector2> compPositions)
    {
        float minDist = float.MaxValue;
        int minIndex = 0;

        for (int i = 0; i < compPositions.Count; i++)
        {
            float dist = Vector2.Distance(matchPos, compPositions[i]);
            if (dist < minDist)
            {
                minDist = dist;
                minIndex = i;
            }
        }
        return minIndex;
    }

    private float SymbolMatchValue(Vector2 pos, SymbolTypes.SymbolType type)
    {
        float isRightType = type == SymbolType ? 1 : 0;
        float distanceHeuristic = DistanceHeuristic(SymbolPosition, pos, MAX_SYMBOL_DISTANCE);

        return isRightType * distanceHeuristic;
    }

    private float DistanceHeuristic(Vector2 matchPos, Vector2 pos, float maxDist)
    {
        return Mathf.Clamp01(1.0f - Vector2.Distance(pos, matchPos) / maxDist);
    }
}
