using UnityEngine;
using System.Collections;

public class Symbol : MonoBehaviour {

    [SerializeField]
    private SymbolTypes.SymbolType m_Type = SymbolTypes.SymbolType.Symbol0;


    public SymbolTypes.SymbolType SymbolType
    {
        get
        {
            return this.m_Type;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
