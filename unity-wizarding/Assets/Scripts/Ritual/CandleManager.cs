using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandleManager : MonoBehaviour {

    [SerializeField]
    private GameObject Candle = null;

    private List<Candle> m_CandleList = null;

	// Use this for initialization
	void Start () {

        if (Candle == null)
        {
            Debug.LogWarning("NUll candle gameObject attached.");
            Destroy(this);
        }

        this.m_CandleList = new List<Candle>();

        InitialiseCandle();

	}
	
    private void InitialiseCandle()
    {
        Vector3 CandlePosition = new Vector3(6.6f, 3.0f, 0);

        GameObject.Instantiate(Candle, CandlePosition, Quaternion.identity);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void AddCandleToList(Candle candle)
    {
        this.m_CandleList.Add(candle);
        candle.b_Placed = true;
    }
}
