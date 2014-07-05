using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class IllnessContainer : MonoBehaviour
{
    [SerializeField] private List<Illness> illnesses;

    public Illness GetRandomIllness()
    {
        return illnesses.Random();
    }
}
