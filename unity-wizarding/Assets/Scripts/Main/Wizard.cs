using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour
{

    private Animator anim;
	void Start ()
	{
	    anim = GetComponent<Animator>();
	    StartCoroutine(TurnSometimes());
	}

    IEnumerator TurnSometimes()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 8.0f));
            anim.SetTrigger("Turn");
        }
    }
}
