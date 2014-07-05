using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UISpellbook : MonoBehaviour
{
    [SerializeField] private SpellBookInfo spellBook;

    [SerializeField] private GameObject spellPagePrefab;

    private List<UISpellPage> spellPages = new List<UISpellPage>();

    private UIPlayTween tween;

    private bool isShown = false;

    private int activePage = 0;

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

    public void ShowNextSpell()
    {
        if (activePage < spellPages.Count - 1)
        {
            SetActivePage(activePage + 1);
        }
    }

    public void ShowPrevSpell()
    {
        if (activePage > 0)
        {
            SetActivePage(activePage - 1);
        }
    }

	private void Start () 
    {
	    foreach (var spell in spellBook.Spells)
	    {
	        CreatePage(spell);
	    }

	    tween = GetComponent<UIPlayTween>();
        SetActivePage(0);
    }

    private void SetActivePage(int index)
    {
        spellPages[activePage].gameObject.SetActive(false);
        spellPages[index].gameObject.SetActive(true);
        activePage = index;
    }

    private void CreatePage(Spell spell)
    {
        var pageGo = (GameObject) GameObject.Instantiate(spellPagePrefab);
        pageGo.transform.parent = transform;
        pageGo.transform.localPosition = Vector2.zero;
        pageGo.transform.localScale = Vector3.one;
        pageGo.SetActive(false);

        var spellpage = pageGo.GetComponent<UISpellPage>();
        spellpage.SetSpell(spell);

        spellPages.Add(spellpage);
    }
}
