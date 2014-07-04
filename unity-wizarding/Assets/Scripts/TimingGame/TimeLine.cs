using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    #region Properties
    public event Action TimelineActivated;
    #endregion

    #region Fields
    [SerializeField]
    private GameObject timelineBase;

    [SerializeField]
    private GameObject timelineBackground;

    [SerializeField]
    private KeyCode activateKey;

    private List<TimeLineSymbol> activeSymbols = new List<TimeLineSymbol>();
    #endregion

    public void SpawnRune(RuneType type)
    {
        var go = RunePrefabDatabase.Instance.GetData(type);
    }

    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(activateKey))
	    {
	        if (TimelineActivated != null)
	        {
	            TimelineActivated();
	        }
	    }
	}
}
