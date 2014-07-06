using UnityEngine;
using System.Collections;

public class CastingGameState : GameStateBase
{
    [SerializeField]
    private TimingManager timingManager;
    [SerializeField]
    private GameObject overlayLight;
    [SerializeField]
    private float survivalTime = 20.0f;

    private float timer = 0;

    public override GameStateType Type
    {
        get { return GameStateType.CastingSpell; }
    }

    protected override void OnEnterState()
    {
        StartCoroutine(WaitForZoom());
        overlayLight.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    protected override void OnExitState()
    {
        overlayLight.GetComponent<Animator>().SetTrigger("FadeIn");
        timingManager.GetComponent<UIPlayTween>().Play(false);
        GameManager.ActiveModel.SpellHeuristicValue += timingManager.GetHeuristicValue();
        timingManager.StopPlayback();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        timer += Time.deltaTime;
        Debug.Log(timingManager.MissedSymbols);
        if (timer > survivalTime)
        {
            GameManager.ActiveModel.SuccededCasting = true;
            GameManager.GoTo(GameStateType.ShowResult);
        }
        else if (timingManager.MissedSymbols > GameManager.ActiveModel.PlacedCandlesCount)
        {
            GameManager.ActiveModel.SuccededCasting = false;
            GameManager.GoTo(GameStateType.ShowResult);
        }
    }

    private IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(1.0f);
        timingManager.gameObject.SetActive(true);
        timingManager.GetComponent<UIPlayTween>().Play(true);
        timingManager.PlaySequence(GameManager.ActiveModel.CurrentIllness.Sequence, 1.0f - GameManager.ActiveModel.SpellHeuristicValue);
    }
}
