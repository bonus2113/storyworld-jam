using UnityEngine;
using System.Collections;

public class GoToRitualInteraction : MonoBehaviour
{
    private GameManager gameManager;

	void Start ()
	{
	    gameManager = FindObjectOfType<GameManager>();
	}

    private void OnMouseDown()
    {
        gameManager.GoTo(GameStateType.Ritual);
    }
}
