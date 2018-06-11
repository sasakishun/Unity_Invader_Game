using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    GameObject[] life = new GameObject[3];
    static int i = 2;

    void lifeDamage()
    {
        life[i].GetComponent<Image>().enabled = false;
        i--;
        if(i == -1)
        {
            GameObject.Find("TopWall").SendMessage("gameOver");
        }
    }
    // Use this for initialization
    void Start()
    {
        life[0] = GameObject.Find("Life0");
        life[1] = GameObject.Find("Life1");
        life[2] = GameObject.Find("Life2");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
