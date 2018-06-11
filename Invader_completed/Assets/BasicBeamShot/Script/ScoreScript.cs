using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public static int score = 0;
    //    public GameObject player;
    public Text text1;

    // Use this for initialization
    /*void InitScore()
    {
        this.score = 0;
    }*/

    public void AddScore(int score_add)
    {
       score += score_add;
    }
    void Start()
    {
        //this.score = 0;
        this.text1 = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = "Score : " + score;
    }
}
