using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SymbolManager : MonoBehaviour {

    private int m_NumSymbols = 0;

    [SerializeField]
    private Vector2 m_SymbolTarget = new Vector2(0, 0);

    [SerializeField]
    private List<Symbol> m_SymbolList = null;

	// Use this for initialization
	void Start () {

        this.m_NumSymbols = this.m_SymbolList.Count;
        if (this.m_NumSymbols == 0)
        {
            Debug.LogWarning("No symbols in list");
            Destroy(this);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitialiseSymbols()
    {
        const int SymbolYOffset = 10;

        for(int i = 0; i < this.m_NumSymbols; i++)
        {

        }
    }
}
