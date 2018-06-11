using UnityEngine;
using System.Collections;

public class Voice : MonoBehaviour {
    public AudioSource voice1;

	// Use this for initialization
	void Start () {
        voice1 = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            voice1.PlayOneShot(voice1.clip);
        }
    }
}
