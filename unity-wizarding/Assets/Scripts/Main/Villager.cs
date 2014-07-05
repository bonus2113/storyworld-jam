using System;
using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour
{
    private Animator anim;
    private Action introEndCallback;

    public void SetIllness(Illness illness, BodyPartType bodyPart)
    {
        
    }

    public void StartMove(Action callback)
    {
        introEndCallback = callback;
        anim.Play("VillagerIntro");
    }

    public void OnIntroEnd()
    {
        introEndCallback();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
