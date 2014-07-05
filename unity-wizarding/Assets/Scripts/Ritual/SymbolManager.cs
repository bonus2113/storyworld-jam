using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolManager : MonoBehaviour {

    private int m_NumSymbols = 0;
    private bool b_Debug = false;

    [SerializeField]
    private GameObject m_DebugGraphic = null;

    private GameObject m_SymbolDebug = null;

    [SerializeField]
    private Vector2 m_SymbolTargetPosition = new Vector2(0, 0);
    private SymbolTypes.SymbolType m_TargetSymbolType;
    private float m_SymbolDistanceFromTarget = 0.0f;

    private bool b_SymbolActive = false;
    private SymbolTypes.SymbolType m_CurType = SymbolTypes.SymbolType.Symbol0;

    [SerializeField]
    private List<Symbol> m_SymbolList = null;

    private Vector2 m_SymbolPosition = Vector2.zero;

	// Use this for initialization
	void Start () {

        this.m_NumSymbols = this.m_SymbolList.Count;
        if (this.m_NumSymbols == 0)
        {
            Debug.LogWarning("No symbols in list");
            Destroy(this);
        }

        InitialiseSymbols();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitialiseSymbols()
    {
        const float SymbolYOffset = 2.5f;
        Vector3 SymbolPosition = new Vector3(6.6f,1.0f,0);

        for(int i = 0; i < this.m_NumSymbols; i++)
        {
            GameObject.Instantiate(this.m_SymbolList[i], SymbolPosition - i * SymbolYOffset * Vector3.up, Quaternion.identity);
        }

        this.m_TargetSymbolType = (SymbolTypes.SymbolType)Random.Range(0, System.Enum.GetValues(typeof(SymbolTypes.SymbolType)).Length); // random target symbol from symbol enum list
        Debug.Log("Target symbol type: " + this.m_TargetSymbolType);

    }

    public void EnableDebug()
    {
        this.b_Debug = true;

        this.m_SymbolDebug = (GameObject)GameObject.Instantiate(this.m_DebugGraphic, m_SymbolTargetPosition, Quaternion.identity);
        this.m_SymbolDebug.GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.m_SymbolDebug.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void UpdateSymbolPositionAndType(SymbolTypes.SymbolType type, Vector2 pos)
    {
        this.b_SymbolActive = true;
        this.m_SymbolPosition = pos;
        this.m_CurType = type;
        this.m_SymbolDistanceFromTarget = (this.m_SymbolPosition - this.m_SymbolTargetPosition).magnitude;


        if (this.b_Debug)
        {
            this.m_SymbolDebug.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (this.m_CurType == this.m_TargetSymbolType)
        {
            if ( m_SymbolDistanceFromTarget < 1.0f)
            {
                if (this.b_Debug)
                {
                    this.m_SymbolDebug.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }


        //Debug.Log("Dist from target: " + this.m_SymbolDistanceFromTarget);
    }
}
