using UnityEngine;
using System.Collections;

public class RitualGameManager : MonoBehaviour {

    private SymbolManager m_SymbolManager = null;

    public bool b_Debug = false;

	// Use this for initialization
	void Start () {

        this.m_SymbolManager = GameObject.FindObjectOfType<SymbolManager>();
        if (this.m_SymbolManager == null)
        {
            Debug.LogWarning("No symbolManager found!.");
            Destroy(this);
        }

        if (b_Debug)
        {
            this.m_SymbolManager.EnableDebug();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
