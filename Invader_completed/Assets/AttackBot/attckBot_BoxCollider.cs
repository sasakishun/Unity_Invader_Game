using UnityEngine;
using System.Collections;

public class attckBot_BoxCollider : MonoBehaviour {

    public GameObject attackbot;
	// Use this for initialization
	void Start () {
        attackbot.GetComponent<BoxCollider>().center = new Vector3(0, 0.5f, 0);
        attackbot.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
