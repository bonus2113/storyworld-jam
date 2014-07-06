using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{
    public float Multiplier = 1.0f;

    private SpriteRenderer spriteRenderer;

	void Start ()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
	    var val = Mathf.Sin(Time.timeSinceLevelLoad)*Mathf.Cos(Time.timeSinceLevelLoad*0.4f + 2)*0.6f + 0.4f;
	    val = Mathf.Lerp(1, val, Multiplier);
        spriteRenderer.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), val);
	}
}
