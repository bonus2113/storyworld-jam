using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour
{
    private Action introEndCallback;

    public GameObject CureEffect;
    public GameObject FailEffect;

    public List<GameObject> m_NormalHeads;
    public List<GameObject> m_NormalBody;
    public List<GameObject> m_NormalArms;
    public List<GameObject> m_NormalLegs;

    private GameObject m_HeadObject;
    private GameObject m_ArmObject;
    private GameObject m_BodyObject;
    private GameObject m_LegsObject;

    private BodyPartType diseasedPart;

    private Animator anim;

    void Start()
    {
        Vector3 tempVec = this.transform.localScale;
        tempVec.x *= -1;
        this.transform.localScale = tempVec;

        m_HeadObject = (GameObject)GameObject.Instantiate(this.m_NormalHeads[UnityEngine.Random.Range(0, this.m_NormalHeads.Count)]);
        this.m_HeadObject.transform.position = this.transform.position;
        this.m_HeadObject.transform.parent = this.transform;
        this.m_HeadObject.tag = "Head";
        tempVec = this.m_HeadObject.transform.localScale;
        tempVec.x *= -1;
        this.m_HeadObject.transform.localScale = tempVec;

        this.m_ArmObject = (GameObject)GameObject.Instantiate(this.m_NormalArms[UnityEngine.Random.Range(0, this.m_NormalArms.Count)]);
        this.m_ArmObject.transform.position = this.transform.position;
        this.m_ArmObject.transform.parent = this.transform;
        this.m_ArmObject.tag = "Arms";
        tempVec = this.m_ArmObject.transform.localScale;
        tempVec.x *= -1;
        this.m_ArmObject.transform.localScale = tempVec;

        this.m_BodyObject = (GameObject)GameObject.Instantiate(this.m_NormalBody[UnityEngine.Random.Range(0, this.m_NormalBody.Count)]);
        this.m_BodyObject.transform.position = this.transform.position;
        this.m_BodyObject.transform.parent = this.transform;
        this.m_BodyObject.tag = "Body";
        tempVec = this.m_BodyObject.transform.localScale;
        tempVec.x *= -1;
        this.m_BodyObject.transform.localScale = tempVec;

        this.m_LegsObject = (GameObject)GameObject.Instantiate(this.m_NormalLegs[UnityEngine.Random.Range(0, this.m_NormalLegs.Count)]);
        this.m_LegsObject.transform.position = this.transform.position;
        this.m_LegsObject.transform.parent = this.transform;
        this.m_LegsObject.tag = "Legs";
        tempVec = this.m_LegsObject.transform.localScale;
        tempVec.x *= -1;
        this.m_LegsObject.transform.localScale = tempVec;
    }

    public void SetIllness(Illness illness, BodyPartType bodyPart)
    {
        GameObject switchedPart = new GameObject();
        this.diseasedPart = bodyPart;
        switch (bodyPart)
        {
            case BodyPartType.Arms:
                {
                    Destroy(this.m_ArmObject.gameObject);
                    this.m_ArmObject = (GameObject)GameObject.Instantiate(illness.ArmPrefab, this.transform.position, Quaternion.identity);
                    this.m_ArmObject.tag = "Arms";
                    switchedPart = this.m_ArmObject;
                }
                break;
            case BodyPartType.Body:
                {
                    Destroy(this.m_BodyObject.gameObject);
                    this.m_BodyObject = (GameObject)GameObject.Instantiate(illness.BodyPrefab, this.transform.position, Quaternion.identity);
                    this.m_BodyObject.tag = "Body";
                    switchedPart = this.m_BodyObject;
                }
                break;
            case BodyPartType.Head:
                {
                    Destroy(this.m_HeadObject.gameObject);
                    this.m_HeadObject = (GameObject)GameObject.Instantiate(illness.HeadPrefab, this.transform.position, Quaternion.identity);
                    this.m_HeadObject.tag = "Head";
                    switchedPart = this.m_HeadObject;
                }
                break;
            case BodyPartType.Legs:
                {
                    Destroy(this.m_LegsObject.gameObject);
                    this.m_LegsObject = (GameObject)GameObject.Instantiate(illness.LegPrefab, this.transform.position, Quaternion.identity);
                    this.m_LegsObject.tag = "Legs";
                    switchedPart = this.m_LegsObject;
                }
                break;
        }

        switchedPart.transform.parent = this.transform;
        switchedPart.GetComponent<SpriteRenderer>().color = Color.yellow;

        Vector3 tempVec = switchedPart.transform.localScale;
        tempVec.x *= -1;
        switchedPart.transform.localScale = tempVec;

    }

    public void Cure(BodyPartType bodyPart)
    {
        GameObject switchedPart = new GameObject();

        switch (bodyPart)
        {
            case BodyPartType.Arms:
                {
                    Destroy(this.m_ArmObject.gameObject);
                    this.m_ArmObject = (GameObject)GameObject.Instantiate(this.m_NormalArms[UnityEngine.Random.Range(0, this.m_NormalArms.Count)], this.transform.position, Quaternion.identity);
                    this.m_ArmObject.tag = "Arms";
                    switchedPart = this.m_ArmObject;
                }
                break;
            case BodyPartType.Body:
                {
                    Destroy(this.m_BodyObject.gameObject);
                    this.m_BodyObject = (GameObject)GameObject.Instantiate(this.m_NormalBody[UnityEngine.Random.Range(0, this.m_NormalArms.Count)], this.transform.position, Quaternion.identity);
                    this.m_BodyObject.tag = "Body";
                    switchedPart = this.m_BodyObject;
                }
                break;
            case BodyPartType.Head:
                {
                    Destroy(this.m_HeadObject.gameObject);
                    this.m_HeadObject = (GameObject)GameObject.Instantiate(this.m_NormalHeads[UnityEngine.Random.Range(0, this.m_NormalHeads.Count)], this.transform.position, Quaternion.identity);
                    this.m_HeadObject.tag = "Head";
                    switchedPart = this.m_HeadObject;

                }
                break;
            case BodyPartType.Legs:
                {
                    Destroy(this.m_LegsObject.gameObject);
                    this.m_LegsObject = (GameObject)GameObject.Instantiate(this.m_NormalLegs[UnityEngine.Random.Range(0, this.m_NormalArms.Count)], this.transform.position, Quaternion.identity);
                    this.m_LegsObject.tag = "Legs";
                    switchedPart = this.m_LegsObject;

                }
                break;
        }
        switchedPart.transform.parent = this.transform;
        Vector3 tempVec = switchedPart.transform.localScale;
        tempVec.x *= -1;
        switchedPart.transform.localScale = tempVec;
        GameObject.Instantiate(CureEffect, switchedPart.transform.position + Vector3.right, Quaternion.identity);
    }

    public void DiseaseRandomPart(Illness illness)
    {
        BodyPartType partToDisease = (BodyPartType)UnityEngine.Random.Range(0, (int)BodyPartType.ENUM_COUNT);
        while (partToDisease == diseasedPart)
        {
            partToDisease = (BodyPartType)UnityEngine.Random.Range(0, (int)BodyPartType.ENUM_COUNT);
        }
        SetIllness(illness, partToDisease);
        GameObject.Instantiate(FailEffect, this.transform.position+Vector3.right, Quaternion.identity);
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
