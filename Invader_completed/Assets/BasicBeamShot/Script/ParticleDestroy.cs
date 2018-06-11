using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

    public GameObject effect;
    private float time = 0;
	// Use this for initialization
	void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((time += Time.deltaTime) > 3.0f)
        {
            effect.GetComponent<ParticleSystem>().Stop();
            effect.GetComponent<ParticleSystem>().Clear();
            Destroy(effect);
            time = 0f;
        }
    }
}
