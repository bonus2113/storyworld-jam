using UnityEngine;
using System.Collections;

public class CastingGameState : GameStateBase
{
    [SerializeField]
    private TimingManager timingManager;
    [SerializeField]
    private GameObject overlayLight;

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
        if (timer > 5)
        {
            GameManager.GoTo(GameStateType.ShowResult);
        }
    }

    private IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(1.0f);
        timingManager.gameObject.SetActive(true);
        timingManager.GetComponent<UIPlayTween>().Play(true);
        timingManager.PlaySequence(GameManager.ActiveModel.CurrentIllness.Sequence);
    }
}
