using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualGameManager : MonoBehaviour {

    private SymbolManager m_SymbolManager = null;
    private CandleManager m_CandleManager = null;

    private RitualInfo m_CurrentRitualInfo;
    public RitualInfo m_TargetRitualInfo;

    private const int MAX_CANDLE_DISTANCE_FROM_SYMBOL = 100;


    public bool b_Debug = false;

	// Use this for initialization
	void Start () {

        this.m_CurrentRitualInfo = ScriptableObject.CreateInstance<RitualInfo>();

        this.m_SymbolManager = GameObject.FindObjectOfType<SymbolManager>();
        if (this.m_SymbolManager == null)
        {
            Debug.LogWarning("No symbolManager found!.");
            Destroy(this);
        }

        this.m_CandleManager = GameObject.FindObjectOfType<CandleManager>();
        if (this.m_CandleManager == null)
        {
            Debug.LogWarning("Null candlemanager.");
            Destroy(this);
        }

        if (b_Debug)
        {
            this.m_TargetRitualInfo = ScriptableObject.CreateInstance<RitualInfo>();

            //random symbol type
            this.m_TargetRitualInfo.SymbolType = (SymbolTypes.SymbolType)Random.Range(0, System.Enum.GetValues(typeof(SymbolTypes.SymbolType)).Length); // random target symbol from symbol enum list
            this.m_TargetRitualInfo.SymbolPosition = new Vector2(Screen.width/2.0f + Screen.width / 8.0f, Screen.height / 2.0f);

            this.m_TargetRitualInfo.CandlePositions = new List<Vector2>();

            //random candle configuration

            CandleConfigurations.CandleConfig canConfig = (CandleConfigurations.CandleConfig)Random.Range(0, System.Enum.GetValues(typeof(CandleConfigurations.CandleConfig)).Length);

            this.m_TargetRitualInfo.CandlePositions = CandleconfigurationHelper.GetCandlePositions(canConfig);

            //convert relative normalised candle configuration positions to screen space
            for (int i = 0; i < this.m_TargetRitualInfo.CandlePositions.Count; i++)
            {
                Vector2 candlePos = this.m_TargetRitualInfo.CandlePositions[i];
                this.m_TargetRitualInfo.CandlePositions[i] = this.m_TargetRitualInfo.SymbolPosition + new Vector2(candlePos.x * MAX_CANDLE_DISTANCE_FROM_SYMBOL, candlePos.y * MAX_CANDLE_DISTANCE_FROM_SYMBOL);
            }

            this.DebugPrintTargetRitualInfo();

            this.m_SymbolManager.EnableDebug();

            this.m_CandleManager.EnableDebug();
        }

	}

    public float GetHeuristicValue()
    {
        //get candle pos
        this.m_CurrentRitualInfo.CandlePositions = this.m_CandleManager.GetCandlePositionsList();
        var val = this.m_TargetRitualInfo.GetHeuristicValue(this.m_CurrentRitualInfo);
        Debug.Log("Ritual heuristic value: " + val);
        return val;
    }

    public void SetTargetRitual(RitualInfo target)
    {
        this.m_TargetRitualInfo = target;
    }

    public void SetSymbolInfo(SymbolTypes.SymbolType type, Vector2 pos)
    {
        this.m_CurrentRitualInfo.SymbolType = type;
        this.m_CurrentRitualInfo.SymbolPosition = pos;
    }

    private void DebugPrintTargetRitualInfo()
    {
        Debug.Log("TargetSymbolType: " + this.m_TargetRitualInfo.SymbolType);
        Debug.Log("TargetSymbolPos: " + this.m_TargetRitualInfo.SymbolPosition);
        Debug.Log("TargetCandleNum: " + this.m_TargetRitualInfo.CandlePositions.Count);

        /*
        for (int i = 0; i < this.m_TargetRitualInfo.CandlePositions.Count; i++)
        {
            Debug.Log("CandlePos" + i + ": " + this.m_TargetRitualInfo.CandlePositions[i]);
        }
         * */
    }
}
