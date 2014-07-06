using UnityEngine;
using System.Collections;

public class Candle : MonoBehaviour
{

    public bool b_Placed = false;
    public bool b_Extinguished = false;
    public GameObject m_CandleFlame;
    public GameObject CandleEffect;

    private float m_TimeToExtinguish = 0.2f;
    private float m_InternalTimer = 0.0f;
    private Vector3 m_OrigCandleScale;

    // Use this for initialization
    void Start()
    {

        if (this.m_CandleFlame == null)
        {
            Debug.LogWarning("CandleFlame not attached.");
            Destroy(this);
        }
        this.m_OrigCandleScale = this.m_CandleFlame.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {

        if (this.m_CandleFlame != null && this.b_Extinguished)
        {
            this.m_InternalTimer += Time.deltaTime;

            if (this.m_InternalTimer > this.m_TimeToExtinguish)
            {
                Destroy(this.m_CandleFlame.gameObject);
                this.m_CandleFlame = null;
            }
            else
            {
                this.m_CandleFlame.transform.localScale = this.m_OrigCandleScale * (1.0f - this.m_InternalTimer / this.m_TimeToExtinguish);
            }
        }

    }

    public Vector3 Extinguish()
    {
        if (b_Extinguished)
        {
            return Vector3.zero;
        }

        b_Extinguished = true;
        GameObject.Instantiate(CandleEffect, this.m_CandleFlame.transform.position, Quaternion.identity);
        //Destroy(this.m_CandleFlame.gameObject);
        //this.m_CandleFlame = null;
        return this.transform.position;
    }
}
