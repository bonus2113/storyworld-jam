using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class RitualInfo : ScriptableObject
{
    public const float MAX_SYMBOL_DISTANCE = 1.0f;
    public const float MAX_CANDLE_DISTANCE = 0.5f;

    [MenuItem("Assets/Create/Ritual Information")]
    public static void Create()
    {
        AssetUtils.CreateAsset(typeof(RitualInfo), "RitualInfo", "Assets/RitualInfo.asset", true);
    }

    public Vector2 SymbolPosition;
    public SymbolTypes.SymbolType SymbolType;
    public List<Vector2> CandlePositions;

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
        for (int i = matchCandles.Count - 1; i >= 0 && compPositions.Count > 0; i--)
        {
            Vector2 currentMatch = matchCandles[i];
            int closestIndex = FindClosest(currentMatch, compPositions);

            heuristicValues.Add(DistanceHeuristic(currentMatch, compPositions[closestIndex], MAX_CANDLE_DISTANCE));
            matchCandles.RemoveAt(i);
            compPositions.RemoveAt(closestIndex);
        }

        return heuristicValues.Average();
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
