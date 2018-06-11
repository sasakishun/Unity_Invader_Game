using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float Speed = 20.0f;
        GetComponent<Rigidbody>().AddForce(
            (       
            transform.forward + transform.right) * Speed,
            ForceMode.VelocityChange
            );
    }
	
	// Update is called once per frame
	void Update () {
	}
}
