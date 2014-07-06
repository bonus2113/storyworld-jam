using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandleManager : MonoBehaviour {

    [SerializeField]
    private GameObject Candle = null;

    private RitualGameManager m_RitualManager;

    [SerializeField]
    private GameObject m_DebugGraphic = null;

    private List<Candle> m_CandleList = null;

	// Use this for initialization
	void Start () {

        if (Candle == null)
        {
            Debug.LogWarning("NUll candle gameObject attached.");
            Destroy(this);
        }

        this.m_RitualManager = GameObject.FindObjectOfType<RitualGameManager>();
        if (this.m_RitualManager == null)
        {
            Debug.LogWarning("Null ritualmanager.");
            Destroy(this);
        }

        this.m_CandleList = new List<Candle>();

        InitialiseCandle();

	}
	
    private void InitialiseCandle()
    {
        //Vector3 CandlePosition = new Vector3(6.6f, 3.0f, 0);
        Vector3 CandlePosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width / 8.0f, Screen.height - Screen.height / 8.0f, 0));
        CandlePosition.z = 0.0f;

        var candle = (GameObject)GameObject.Instantiate(Candle, CandlePosition, Quaternion.identity);
        candle.transform.parent = transform;
    }

	// Update is called once per frame
	void Update () {
	}

    public List<Candle> GetCandleList()
    {
        return this.m_CandleList;
    }

    public List<Vector2> GetCandlePositionsList()
    {
        return this.m_CandleList.ConvertAll<Vector2>(candle => Camera.main.WorldToScreenPoint(candle.transform.position));
    }

    public void AddCandleToList(Candle candle)
    {
        this.m_CandleList.Add(candle);
        candle.b_Placed = true;
    }

    public void EnableDebug()
    {
        Vector3 debugSymbolWorldPos = new Vector3();

        GameObject candleDebug = new GameObject();

        Debug.Log(this.m_RitualManager);
        for (int i = 0; i < this.m_RitualManager.m_TargetRitualInfo.CandlePositions.Count; i++)
        {
            debugSymbolWorldPos = Camera.main.ScreenToWorldPoint(this.m_RitualManager.m_TargetRitualInfo.CandlePositions[i]);
            debugSymbolWorldPos.z = 0.0f;

            candleDebug = (GameObject)GameObject.Instantiate(this.m_DebugGraphic, debugSymbolWorldPos, Quaternion.identity);
            candleDebug.GetComponent<SpriteRenderer>().sortingOrder = 1;
            candleDebug.GetComponent<SpriteRenderer>().color = Color.yellow;
            candleDebug.transform.parent = transform;
        }
    }

    public void ExtinguishCandle()
    {
        for (int i = 0; i < this.m_CandleList.Count; i++)
        {
            if (!this.m_CandleList[i].b_Extinguished)
            {
                this.m_CandleList[i].Extinguish();
                return;
            }
        }
        //all candles are already extinguished - spell fail condition 
    }
}
