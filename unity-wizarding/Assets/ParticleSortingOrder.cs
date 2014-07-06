using System.Net.Security;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParticleSortingOrder : MonoBehaviour
{
    public int SortingOrder;
    public string Layer;

    private ParticleSystem particles;
	// Use this for initialization
	void Start ()
	{
	    particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (particles == null)
	        particles = GetComponent<ParticleSystem>();

	    if (particles != null)
	    {
            particles.renderer.sortingOrder = SortingOrder;
            particles.renderer.sortingLayerName = Layer;
	    }
	}
}
