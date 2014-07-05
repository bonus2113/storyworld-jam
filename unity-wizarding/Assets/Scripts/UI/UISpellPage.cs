using UnityEngine;
using System.Collections;

public class UISpellPage : MonoBehaviour
{
    [SerializeField] private UILabel spellNameLabel;
    [SerializeField] private UILabel spellDescLabel;
    [SerializeField] private UITexture spellIconTexture;

    public void SetSpell(Spell spell)
    {
        spellNameLabel.text = spell.Name;
        spellDescLabel.text = spell.Description;
        spellIconTexture.mainTexture = spell.Icon;
    }
}
