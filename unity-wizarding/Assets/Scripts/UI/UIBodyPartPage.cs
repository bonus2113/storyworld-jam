using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UIBodyPartPage : MonoBehaviour
{
    [SerializeField]
    private UILabel bodyPartNameLabel;
    [SerializeField]
    private UITexture runeIconTexture;

    public void SetBodyPart(BodyPartType bodyPart)
    {
        var info = BodyPartContainer.Instance.GetBodyPart(bodyPart);
        bodyPartNameLabel.text = info.Name;
        runeIconTexture.mainTexture = info.RuneIcon;
    }
}
