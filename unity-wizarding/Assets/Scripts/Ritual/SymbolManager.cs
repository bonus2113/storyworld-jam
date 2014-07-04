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
    }

    public void OnClick()
    {
        Debug.Log("Clickwd.");
    }
}
