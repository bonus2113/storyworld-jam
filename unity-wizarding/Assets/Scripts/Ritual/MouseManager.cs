using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

    public bool b_Debug = false;

    private bool b_MouseDrag = false;

    private SymbolManager m_SymbolManager = null;

    private Symbol m_ActiveSymbol = null;

    [HideInInspector]
    public bool b_SymbolActive = false;

    private Symbol m_Symbol = null;

	// Use this for initialization
	void Start () 
    {
        this.m_SymbolManager = GameObject.FindObjectOfType<SymbolManager>();
        if (this.m_SymbolManager = null)
        {
            Debug.LogWarning("Null symbolManager.");
            Destroy(this);
        }
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0)) //Left click
        {
            RaycastHit rayHit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit))
            {
                Debug.Log("Symbol clicked.");
                Symbol hitSymbol = rayHit.collider.gameObject.GetComponent<Symbol>();
                if (hitSymbol != null)
                {
                    if (this.m_ActiveSymbol != null)
                    {
                        Destroy(this.m_ActiveSymbol.gameObject);
                    }
                    this.m_ActiveSymbol = ((GameObject)GameObject.Instantiate(hitSymbol.gameObject)).GetComponent<Symbol>();
                    Vector3 symbolWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    symbolWorldPos.z = 0.0f;
                    this.m_ActiveSymbol.transform.position = symbolWorldPos;
                    this.b_MouseDrag = true;
                }
            }
            return;
        }
        else if(Input.GetMouseButton(0))
        {
            if (this.b_MouseDrag)
            {
                this.m_ActiveSymbol.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 symbolWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                symbolWorldPos.z = 0.0f;
                this.m_ActiveSymbol.transform.position = symbolWorldPos;

                if(Input.GetMouseButtonUp(0))
                {
                    this.b_MouseDrag = false;

                    //check symbol heuristic here
                }
            }
        }
	}
}
