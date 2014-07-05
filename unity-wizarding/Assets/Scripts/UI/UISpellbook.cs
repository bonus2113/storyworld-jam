using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UISpellbook : MonoBehaviour
{
    private enum TabMode
    {
        Illnesses,
        BodyParts
    }

    [SerializeField] private GameObject illnessPagePrefab;
    [SerializeField] private GameObject bodyPartPagePrefab;

    private List<UIIllnessPage> illnessPages = new List<UIIllnessPage>();
    private List<UIBodyPartPage> bodyPartPages = new List<UIBodyPartPage>();

    private IllnessContainer illnessContainer;

    private UIPlayTween tween;

    private bool isShown = false;

    private int activeIllnessPage = 0;
    private int activeBodyPartPage = 0;

    private TabMode activeMode = TabMode.Illnesses;

    public void Toggle()
    {
        if (isShown)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        tween.Play(true);
        isShown = true;
    }

    public void Hide()
    {
        tween.Play(false);
        isShown = false;
    }

    public void SetModeBodyParts()
    {
        illnessPages.ForEach(page => page.gameObject.SetActive(false));
        SetActiveBodyPartPage(activeBodyPartPage);
        activeMode = TabMode.BodyParts;
    }

    public void SetModeIllnesses()
    {
        bodyPartPages.ForEach(page => page.gameObject.SetActive(false));
        SetActiveIllnessPage(activeIllnessPage);
        activeMode = TabMode.Illnesses;
    }

    public void ShowNextSpell()
    {
        switch (activeMode)
        {
            case TabMode.BodyParts:
                if (activeBodyPartPage < bodyPartPages.Count - 1)
                {
                    SetActiveBodyPartPage(activeBodyPartPage + 1);
                }
                break;

            case TabMode.Illnesses:
                if (activeIllnessPage < illnessPages.Count - 1)
                {
                    SetActiveIllnessPage(activeIllnessPage + 1);
                }
                break;
        }
        
    }

    public void ShowPrevSpell()
    {
        switch (activeMode)
        {
            case TabMode.BodyParts:
                if (activeBodyPartPage > 0)
                {
                    SetActiveBodyPartPage(activeBodyPartPage - 1);
                }
                break;

            case TabMode.Illnesses:
                if (activeIllnessPage > 0)
                {
                    SetActiveIllnessPage(activeIllnessPage - 1);
                }
                break;
        }
    }

	private void Start ()
	{
	    illnessContainer = FindObjectOfType<IllnessContainer>();
        foreach (var illness in illnessContainer.GetIllnesses())
	    {
            CreateIllnessPage(illness);
	    }

	    for (int i = 0; i < (int)BodyPartType.ENUM_COUNT; i++)
	    {
	        CreateBodyPartPage((BodyPartType)i);    
	    }

	    tween = GetComponent<UIPlayTween>();
        SetActiveIllnessPage(0);
        SetActiveBodyPartPage(0);
        SetModeIllnesses();
    }

    private void SetActiveBodyPartPage(int index)
    {
        bodyPartPages[activeBodyPartPage].gameObject.SetActive(false);
        bodyPartPages[index].gameObject.SetActive(true);
        activeBodyPartPage = index;
    }

    private void SetActiveIllnessPage(int index)
    {
        illnessPages[activeIllnessPage].gameObject.SetActive(false);
        illnessPages[index].gameObject.SetActive(true);
        activeIllnessPage = index;
    }

    private void CreateIllnessPage(Illness illness)
    {
        var pageGo = (GameObject) GameObject.Instantiate(illnessPagePrefab);
        pageGo.transform.parent = transform;
        pageGo.transform.localPosition = Vector2.zero;
        pageGo.transform.localScale = Vector3.one;
        pageGo.SetActive(false);

        var illnessPage = pageGo.GetComponent<UIIllnessPage>();
        illnessPage.SetIllness(illness);
        illnessPages.Add(illnessPage);
    }

    private void CreateBodyPartPage(BodyPartType bodyPart)
    {
        var pageGo = (GameObject)GameObject.Instantiate(bodyPartPagePrefab);
        pageGo.transform.parent = transform;
        pageGo.transform.localPosition = Vector2.zero;
        pageGo.transform.localScale = Vector3.one;
        pageGo.SetActive(false);

        var bodyPartPage = pageGo.GetComponent<UIBodyPartPage>();
        bodyPartPage.SetBodyPart(bodyPart);
        bodyPartPages.Add(bodyPartPage);
    }
}
