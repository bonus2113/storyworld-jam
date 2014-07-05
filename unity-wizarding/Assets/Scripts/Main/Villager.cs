using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour
{
    private Action introEndCallback;

    public List<GameObject> m_NormalHeads;
    public List<GameObject> m_NormalBody;
    public List<GameObject> m_NormalArms;
    public List<GameObject> m_NormalLegs;

    private GameObject m_HeadObject;
    private GameObject m_ArmObject;
    private GameObject m_BodyObject;
    private GameObject m_LegsObject;

    private Animator anim;

    void Start()
    {
        m_HeadObject = (GameObject)GameObject.Instantiate(this.m_NormalHeads[UnityEngine.Random.Range(0, this.m_NormalHeads.Count)]);
        this.m_HeadObject.transform.position = this.transform.position;
        this.m_HeadObject.transform.parent = this.transform;
        this.m_HeadObject.tag = "Head";

        this.m_ArmObject = (GameObject)GameObject.Instantiate(this.m_NormalArms[UnityEngine.Random.Range(0, this.m_NormalArms.Count)]);
        this.m_ArmObject.transform.position = this.transform.position;
        this.m_ArmObject.transform.parent = this.transform;
        this.m_ArmObject.tag = "Arms";

        this.m_BodyObject = (GameObject)GameObject.Instantiate(this.m_NormalBody[UnityEngine.Random.Range(0, this.m_NormalBody.Count)]);
        this.m_BodyObject.transform.position = this.transform.position;
        this.m_BodyObject.transform.parent = this.transform;
        this.m_BodyObject.tag = "Body";

        this.m_LegsObject = (GameObject)GameObject.Instantiate(this.m_NormalLegs[UnityEngine.Random.Range(0, this.m_NormalLegs.Count)]);
        this.m_LegsObject.transform.position = this.transform.position;
        this.m_LegsObject.transform.parent = this.transform;
        this.m_LegsObject.tag = "Legs";
    }

    public void SetIllness(Illness illness, BodyPartType bodyPart)
    {
        switch (bodyPart)
        {
            case BodyPartType.Arms:
                {
                    Destroy(this.m_ArmObject.gameObject);
                    this.m_ArmObject = (GameObject)GameObject.Instantiate(illness.ArmPrefab, this.transform.position, Quaternion.identity);
                    this.m_ArmObject.tag = "Arms";
                    this.m_ArmObject.transform.parent = this.transform;
                    this.m_ArmObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                break;
            case BodyPartType.Body:
                {
                    Destroy(this.m_BodyObject.gameObject);
                    this.m_BodyObject = (GameObject)GameObject.Instantiate(illness.BodyPrefab, this.transform.position, Quaternion.identity);
                    this.m_BodyObject.tag = "Body";
                    this.m_BodyObject.transform.parent = this.transform;
                    this.m_BodyObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                break;
            case BodyPartType.Head:
                {
                    Destroy(this.m_HeadObject.gameObject);
                    this.m_HeadObject = (GameObject)GameObject.Instantiate(illness.HeadPrefab, this.transform.position, Quaternion.identity);
                    this.m_HeadObject.tag = "Head";
                    this.m_HeadObject.transform.parent = this.transform;
                    this.m_HeadObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                break;
            case BodyPartType.Legs:
                {
                    Destroy(this.m_LegsObject.gameObject);
                    this.m_LegsObject = (GameObject)GameObject.Instantiate(illness.LegPrefab, this.transform.position, Quaternion.identity);
                    this.m_LegsObject.tag = "Legs";
                    this.m_LegsObject.transform.parent = this.transform;
                    this.m_LegsObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                break;
        }
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
