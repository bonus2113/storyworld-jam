using UnityEngine;
using System.Collections;

public class RitualGameState : GameStateBase
{
    [SerializeField] private GameObject gameplayRoot;

    public override GameStateType Type
    {
        get { return GameStateType.Ritual; }
    }

    protected override void OnEnterState()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ZoomIn");
        StartCoroutine(WaitForZoom());
    }

    protected override void OnExitState()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ZoomOut");
        gameplayRoot.SetActive(false);
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.GoTo(GameStateType.CastingSpell);
        }
    }

    private IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(2.0f);
        gameplayRoot.SetActive(true);
    }
}
