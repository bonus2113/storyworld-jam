using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class BodyPartContainer : MonoBehaviour 
{
    public static BodyPartContainer Instance { get; private set; }

    [SerializeField]
    private List<BodyPartInfo> bodyParts = new List<BodyPartInfo>();

    public BodyPartInfo GetBodyPart(BodyPartType type)
    {
        return bodyParts.First(part => part.Type == type);
    }

    private void Awake()
    {
        Instance = this;
    }
}
