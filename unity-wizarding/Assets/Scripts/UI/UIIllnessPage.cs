using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

public class UIIllnessPage : MonoBehaviour
{
    [SerializeField]
    private UILabel illnessNameLabel;
    [SerializeField]
    private UILabel illnessDescLabel;
    [SerializeField]
    private UITexture illnessIconTexture;

    [SerializeField] private GameObject candlePrefab;

    public void SetIllness(Illness illness)
    {
        illnessNameLabel.text = illness.Name;
        illnessDescLabel.text = illness.Description;
        SetupCandles(CandleconfigurationHelper.GetCandlePositions(illness.CandleConfig));
    }

    private void SetupCandles(List<Vector2> positions)
    {
        foreach (var position in positions)
        {
            
        }
    }

    private void InitCandle()
    {
        
    }
}
