using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    #region Properties
    public const float MAX_DIST = 1.5f;

    public event Action HitSymbol;
    public event Action MissedSymbol;

    [HideInInspector]
    public KeyCode ActivateKey;
    #endregion

    #region Fields
    [SerializeField]
    private GameObject timelineBase;

    [SerializeField]
    private GameObject timelineBackground;

    [SerializeField]
    private float spawnOffsetY;

    private List<TimeLineSymbol> activeSymbols = new List<TimeLineSymbol>();
    #endregion

    public void SpawnRune(RuneType type)
    {
        var go = (GameObject)GameObject.Instantiate(RunePrefabDatabase.Instance.GetData(type));
        go.transform.parent = transform;
        go.transform.localPosition = Vector2.up * spawnOffsetY;

        var symbol = go.GetComponent<TimeLineSymbol>();
        symbol.MissedSymbol += symbol_MissedSymbol;
        if (MissedSymbol != null)
        {
            MissedSymbol();
        }

        activeSymbols.Add(symbol);
    }

    void symbol_MissedSymbol(TimeLineSymbol symbol)
    {
        symbol.MissedSymbol -= symbol_MissedSymbol;
        activeSymbols.Remove(symbol);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (activeSymbols.Count > 0 && Input.GetKeyDown(ActivateKey))
	    {
	        var firstSymbol = activeSymbols[0];
	        float dist = firstSymbol.transform.localPosition.y;
            float normDist = dist / MAX_DIST;

	        if (normDist < 1.0f)
	        {
                firstSymbol.MissedSymbol -= symbol_MissedSymbol;
                activeSymbols.RemoveAt(0);
                Destroy(firstSymbol.gameObject);

	            if (HitSymbol != null)
	            {
	                HitSymbol();
	            }
	        }
	    }
	}
}
