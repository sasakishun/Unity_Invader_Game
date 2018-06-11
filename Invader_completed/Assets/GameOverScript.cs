using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public void gameOver()
    {
        //        Application.LoadLevel("GameOver");
        //        SceneManager.LoadScene("GameOver");
        GameObject.Find("GameOver").GetComponent<Image>().enabled = true;
        GameObject.Find("GameOverText").GetComponent<Text>().enabled = true;
    }

    public void gameClear()
    {
        GameObject.Find("Clear").GetComponent<Image>().enabled = true;
        GameObject.Find("ClearText").GetComponent<Text>().enabled = true;
    }
    void Awake()
    {
        GameObject.Find("GameOver").GetComponent<Image>().enabled = false;
        GameObject.Find("Clear").GetComponent<Image>().enabled = false;
        GameObject.Find("ClearText").GetComponent<Text>().enabled = false;
        GameObject.Find("GameOverText").GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
/*        for (int i = 0; i < 40; i++ )
        {
            if (GameObject.Find("Block" + i) != null)
            {
                return;
            }
        }
        gameClear();
        */
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
               if(collision.gameObject.tag == "Enemy")
                {
                    gameOver();
                }
        //gameOver();
    }
}
