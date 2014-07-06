using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{
    public float Multiplier = 1.0f;
    public float TimeOffset = 0.0f;
    public float TimeMultiplier = 1.0f;

    private SpriteRenderer spriteRenderer;

    private Color baseColor;
    private Color transparentColor;

	void Start ()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>();
	    baseColor = spriteRenderer.color;
	    baseColor.a = 1;

	    transparentColor = baseColor;
	    transparentColor.a = 0;
	}
	
	void Update ()
	{
	    float time = Time.timeSinceLevelLoad + TimeOffset;
	    time *= TimeMultiplier;

        var val = Mathf.Sin(time) * Mathf.Cos(time * 0.4f + 2) * 0.3f + 0.7f;
	    val = Mathf.Lerp(1, val, Multiplier);
        spriteRenderer.color = Color.Lerp(baseColor, transparentColor, val);
	}
}
