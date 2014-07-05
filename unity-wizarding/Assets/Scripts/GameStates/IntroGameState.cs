using UnityEngine;
using System.Collections;

public class IntroGameState : GameStateBase
{
    public override GameStateType Type
    {
        get { return GameStateType.Intro; }
    }

    private IllnessContainer illnessContainer;
    private Villager villager;

    protected override void OnAwake()
    {
        illnessContainer = FindObjectOfType<IllnessContainer>();
        villager = FindObjectOfType<Villager>();
    }

    protected override void OnEnterState()
    {
        CreateVillager();
    }

    protected override void OnExitState()
    {

    }

    private void CreateVillager()
    {
        GameManager.ActiveModel.CurrentIllness = illnessContainer.GetRandomIllness();
        villager.SetIllness(GameManager.ActiveModel.CurrentIllness, GetRandomBodyPartType());
        villager.StartMove(OnIntroFinished);
    }

    private BodyPartType GetRandomBodyPartType()
    {
        return (BodyPartType)Random.Range(0, (int)BodyPartType.ENUM_COUNT);
    }

    private void OnIntroFinished()
    {
        GameManager.GoTo(GameStateType.InspectingVillager);
    }
}
