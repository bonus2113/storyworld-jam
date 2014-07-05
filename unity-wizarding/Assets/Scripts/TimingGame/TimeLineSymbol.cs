using System;
using UnityEngine;
using System.Collections;

public class TimeLineSymbol : MonoBehaviour
{
    public event Action<TimeLineSymbol> MissedSymbol;

    private const float SPEED = 1.0f;

	private void Update () 
    {
	    transform.Translate(-Vector2.up *  Time.deltaTime * SPEED);
	    if (transform.localPosition.y < 0)
	    {
	        if (MissedSymbol != null)
	        {
	            MissedSymbol(this);
	        }
            Destroy(gameObject);
	    }
	}
}
