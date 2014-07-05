using UnityEngine;
using System.Collections;

public class CastingGameState : GameStateBase 
{
    public override GameStateType Type
    {
        get { return GameStateType.CastingSpell; }
    }
}
