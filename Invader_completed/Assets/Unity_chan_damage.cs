using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unity_chan_damage : MonoBehaviour {
    public int life = 3;
    private bool isOnce = true;
    public AudioSource[] voice;

    // Use this for initialization
    void Start () {
        voice = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
    }
    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (isOnce && collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Cube(Racket)").SendMessage("SetRumble", 1f);
            isOnce = false;
//            GameObject.Find("Score").GetComponent<ScoreScript>().AddScore(-5);
            life--;
            GameObject.Find("Panel").SendMessage("lifeDamage");
            voice[7].PlayOneShot(voice[7].clip);

        }
        else
        {
            isOnce = true;
        }

        //.GetComponent<>().SendMessage("LifeDamage");
        /*
        if (collision.gameObject.tag == "Bullet")
                {
                    GameObject.Find("Score").GetComponent<ScoreScript>().AddScore(-5);
                }*/
        //        Destroy(gameObject);
    }

}
