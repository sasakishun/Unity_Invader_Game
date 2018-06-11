using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public AudioSource[] voice;

    public void ToMainScene()
    {
        Application.LoadLevel("The Game");
//        SceneManager.LoadScene("The Game");
    }
	// Use this for initialization
	void Start () {
        voice = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (Application.loadedLevelName == "Title"/*SceneManager.GetActiveScene().name == "Title"*/)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || UniMoveTest.trigger > 0.1f)
            {
                voice[0].PlayOneShot(voice[0].clip);
                ToMainScene();
            }
        }
    }
}
