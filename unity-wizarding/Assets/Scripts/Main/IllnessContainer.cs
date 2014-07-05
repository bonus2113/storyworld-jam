using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Collections;

public class IllnessContainer : MonoBehaviour
{
    [SerializeField] private List<Illness> illnesses;

    public Illness GetRandomIllness()
    {
        return illnesses.Random();
    }

    public ReadOnlyCollection<Illness> GetIllnesses()
    {
        return illnesses.AsReadOnly();
    }
}
