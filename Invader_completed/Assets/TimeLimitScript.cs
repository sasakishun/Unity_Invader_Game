using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimeLimitScript : MonoBehaviour {

    public Text text1;
    public float time;
    public float startwait;
    public AudioSource[] voice;
    bool once = true;

    bool timerStarted = false;

    void startTimer()
    {
        timerStarted = true;
    }
    void stopTimer()
    {
        timerStarted = false;
    }
    // Use this for initialization
    void Start () {
        time = 30.9f;
        startwait = 5.9f;
        this.text1 = GetComponent<Text>();
        voice = GameObject.Find("SD_unitychan_humanoid").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            time -= Time.deltaTime;
            text1.text = "" + (int)time;
        }
        else
        {
            if (startwait <= 0)
            {
                startTimer();
            }
            else
            {
                text1.text = "" + (int)startwait;
                startwait -= Time.deltaTime;
            }
            if (startwait >= -0.2 && startwait < 1)
            {
                text1.text = "START";
                if (once)
                {
                    voice[8].PlayOneShot(voice[8].clip);
                    once = false;
                }
            }
            else
            {
                once = true;
            }
        }

    }
}
