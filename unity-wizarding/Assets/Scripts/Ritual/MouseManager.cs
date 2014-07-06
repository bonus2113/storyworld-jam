using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

    private bool b_MouseDrag = false;

    private SymbolManager m_SymbolManager = null;
    private CandleManager m_CandleManager = null;
    private RitualGameManager m_RitualManager = null;

    private Candle m_ActiveCandle = null;
    private float m_OrigCandleScale = 1.0f;

    private RaycastHit m_RayHit = new RaycastHit();

    private Symbol m_ActiveSymbol = null;
    private float m_OrigSymbolScale = 1.0f;

    private bool b_CandleActive = false;
    private bool b_SymbolActive = false;

    private Symbol m_Symbol = null;

	// Use this for initialization
	void Start () 
    {
        this.m_SymbolManager = GameObject.FindObjectOfType<SymbolManager>();
        if (this.m_SymbolManager == null)
        {
            Debug.LogWarning("Null symbolManager.");
            Destroy(this);
        }

        this.m_CandleManager = GameObject.FindObjectOfType<CandleManager>();
        if (this.m_CandleManager == null)
        {
            Debug.Log("Null candleManager.");
            Destroy(this);
        }

        this.m_RitualManager = GameObject.FindObjectOfType<RitualGameManager>();
        if (this.m_RitualManager == null)
        {
            Debug.Log("Null m_RitualManager.");
            Destroy(this);
        }
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out m_RayHit);
        }

        if (this.m_RayHit.collider != null)
        {
            HandleSymbolPlacement();
            HandleCandlePlacement();
        }
	}

    private void HandleCandlePlacement()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Candle hitCandle = this.m_RayHit.collider.gameObject.GetComponent<Candle>();
            if(hitCandle != null && hitCandle.b_Placed == false)
            {
                this.m_ActiveCandle = ((GameObject)GameObject.Instantiate(hitCandle.gameObject)).GetComponent<Candle>();

                Vector3 candleWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                candleWorldPos.z = 0.0f;
                this.m_ActiveCandle.transform.position = candleWorldPos;
                this.m_ActiveCandle.transform.parent = this.m_RitualManager.m_PersistantRitual.transform;
                this.m_ActiveCandle.transform.localScale *= 1.5f;
                m_OrigCandleScale = this.m_ActiveCandle.transform.localScale.x;
                this.m_ActiveCandle.transform.localScale = this.m_OrigCandleScale * Vector3.one * (1.0f - PerspectiveScaleFactor() * 0.5f);
                this.b_MouseDrag = true;
                this.b_CandleActive = true;

            }
            return;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!this.b_CandleActive || m_ActiveCandle == null)
            {
                return;
            }

            if (this.b_MouseDrag)
            {
                //this.m_ActiveCandle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 candleWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                candleWorldPos.z = 0.0f;
                this.m_ActiveCandle.transform.position = candleWorldPos;
                this.m_ActiveCandle.transform.localScale = this.m_OrigCandleScale * Vector3.one * (1.0f - PerspectiveScaleFactor()*0.5f);

            }
        }
        else if (Input.GetMouseButtonUp(0) && this.b_CandleActive)
        {
            this.b_MouseDrag = false;
            this.b_CandleActive = false;

            this.m_CandleManager.AddCandleToList(this.m_ActiveCandle);
            this.m_ActiveCandle.GetComponent<AudioSource>().Play();
            this.m_ActiveCandle = null;
        }

    }

    private float PerspectiveScaleFactor()
    {
        return (Mathf.Clamp01((Input.mousePosition.y / Screen.height) - 0.3f) / 0.7f);//[0.0f,1.0f]
    }

    private void HandleSymbolPlacement()
    {
        if (Input.GetMouseButtonDown(0)) //Left click
        {
            Symbol hitSymbol = m_RayHit.collider.gameObject.GetComponent<Symbol>();
            if (hitSymbol != null)
            {
                if (this.m_ActiveSymbol != null && this.m_ActiveSymbol.gameObject != hitSymbol.gameObject)
                {
                    Destroy(this.m_ActiveSymbol.gameObject);
                    this.m_ActiveSymbol = null;
                }

                if (this.m_ActiveSymbol == null)
                {
                    this.m_ActiveSymbol = ((GameObject)GameObject.Instantiate(hitSymbol.gameObject)).GetComponent<Symbol>();
                    this.m_ActiveSymbol.transform.parent = this.m_RitualManager.m_PersistantRitual.transform;
                    this.m_ActiveSymbol.transform.localScale *= 1.5f;
                    m_OrigSymbolScale = this.m_ActiveSymbol.transform.localScale.x;
                    this.m_ActiveSymbol.transform.localScale = this.m_OrigSymbolScale * Vector3.one * (1.0f - PerspectiveScaleFactor() * 0.5f);

                }

                Vector3 symbolWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                symbolWorldPos.z = 0.0f;
                this.m_ActiveSymbol.transform.position = symbolWorldPos;
                this.b_MouseDrag = true;
                this.b_SymbolActive = true;
            }
            return;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!this.b_SymbolActive || m_ActiveSymbol == null)
            {
                return;
            }

            if (this.b_MouseDrag)
            {
                this.m_ActiveSymbol.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 symbolWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                symbolWorldPos.z = 0.0f;
                this.m_ActiveSymbol.transform.position = symbolWorldPos;
                this.m_ActiveSymbol.transform.localScale = this.m_OrigSymbolScale * Vector3.one * (1.0f - PerspectiveScaleFactor() * 0.5f);

            }
        }
        else if (Input.GetMouseButtonUp(0) && this.b_SymbolActive)
        {
            this.b_MouseDrag = false;
            this.b_SymbolActive = false;
            this.m_SymbolManager.UpdateSymbolPositionAndType(this.m_ActiveSymbol.SymbolType, Input.mousePosition);
            this.m_ActiveSymbol.GetComponent<AudioSource>().Play();
        }
    }
}
