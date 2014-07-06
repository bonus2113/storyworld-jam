using System;
using UnityEngine;
using System.Collections;

public class TimeLineSymbol : MonoBehaviour
{
    public event Action<TimeLineSymbol> MissedSymbol;

    private const float SPEED = 1.0f;

    public static float Difficulty = 0;



	private void Update () 
    {
        if(renderer.enabled)
            return;

	    transform.Translate(-Vector2.up *  Time.deltaTime * SPEED * (1 + Difficulty * 2));
	    if (transform.localPosition.y < 0)
	    {
	        if (MissedSymbol != null)
	        {
	            MissedSymbol(this);
	        }
	        enabled = false;
            Destroy(gameObject, 2);
	        renderer.enabled = false;
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Missed");
	    }
	}
}
