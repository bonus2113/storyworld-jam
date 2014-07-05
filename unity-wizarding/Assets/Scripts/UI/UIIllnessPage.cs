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

    [SerializeField] private GameObject candlePrefab;
    [SerializeField] private GameObject candleRoot;

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
            InitCandle(position);
        }
    }

    private void InitCandle(Vector2 pos)
    {
        var candleObj = (GameObject) GameObject.Instantiate(candlePrefab);
        candleObj.transform.parent = candleRoot.transform;
        candleObj.transform.localPosition = pos*100;
        candleObj.transform.localScale = candleObj.transform.localScale * 0.1f;
    }
}
