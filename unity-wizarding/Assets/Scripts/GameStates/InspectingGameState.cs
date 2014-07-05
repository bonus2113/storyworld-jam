using UnityEngine;
using System.Collections;

public class InspectingGameState : GameStateBase 
{
    public override GameStateType Type
    {
        get { return GameStateType.InspectingVillager; }
    }
}
