using UnityEngine;
using System.Collections;

public class RitualGameManager : MonoBehaviour {

    private SymbolManager m_SymbolManager = null;

	// Use this for initialization
	void Start () {

        this.m_SymbolManager = GameObject.FindObjectOfType<SymbolManager>();
        if (this.m_SymbolManager == null)
        {
            Debug.LogWarning("No symbolManager found!.");
            Destroy(this);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
