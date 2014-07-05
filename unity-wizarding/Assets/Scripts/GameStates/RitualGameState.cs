using UnityEngine;
using System.Collections;

public class RitualGameState : GameStateBase 
{
    public override GameStateType Type
    {
        get { return GameStateType.Ritual; }
    }

    protected override void OnEnterState()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ZoomIn");
    }

    protected override void OnExitState()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ZoomOut");
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.GoTo(GameStateType.CastingSpell);
        }
    }
}
