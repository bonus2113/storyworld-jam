using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour {

    public bool b_Placed = false;
    public bool b_Extinguished = false;
    public GameObject m_CandleFlame;
	// Use this for initialization
	void Start () {

        if (this.m_CandleFlame == null)
        {
            Debug.LogWarning("CandleFlame not attached.");
            Destroy(this);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Extinguish()
    {
        if (b_Extinguished)
        {
            return;
        }

        b_Extinguished = true;
        Destroy(this.m_CandleFlame.gameObject);
    }
}
