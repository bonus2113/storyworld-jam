using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour
{
    public float DestroyTime = 0;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
