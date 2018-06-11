using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class attackBotController : MonoBehaviour
{
    private Vector3 m_pos;
    private Vector3 m_rotation;
    private static float yspeed = 1.0f;
    private int advcnt = 0;

    private int shotcount;
    public GameObject prefab;
    public AudioClip audioClip;
    AudioSource audioSource;
    const float GAMELEFT = -20.0f;
    const float GAMERIGHT = 20.0f;

    public int blockMAX = 40;
    private static int atbcount = 40;

    void Move()
    {
        if (m_pos.x >= GAMERIGHT)
        {
            for (int i = 0; i < blockMAX; i++)
            {
                string name = "Block" + i;
                GameObject obj = GameObject.Find(name);
                if (obj == null) continue;
                attackBotController abc = obj.GetComponent<attackBotController>();
                abc.advcnt = 30;
                m_pos.x = GAMERIGHT - 0.1f;
            }
            yspeed *= -1.0f;
        }
        if (m_pos.x <= GAMELEFT)
        {
            for (int i = 0; i < blockMAX; i++)
            {
                string name = "Block" + i;
                GameObject obj = GameObject.Find(name);
                if (obj == null) continue;
                attackBotController abc = obj.GetComponent<attackBotController>();
                abc.advcnt = 30;
                m_pos.x = GAMELEFT + 0.1f;
            }
            yspeed *= -1.0f;
        }

        if (advcnt >= 0)
        {
            advcnt--;
            m_pos.z -= 0.05f;
        }

        if (Mathf.Abs(yspeed) <= 1.5f)
        {
            if (atbcount <= 5)
            {
                yspeed *= 5.0f;
            }
        }
        m_pos.x += 0.01f * yspeed;
        transform.localPosition = m_pos;  // 形状位置を更新

    }

    void Shot()
    {
        //		GameObject obj = (GameObject)Instantiate (prefab, new Vector3(m_pos.x,0.0f,m_pos.z), Quaternion.identity);
        GameObject obj = (GameObject)Instantiate(prefab, new Vector3(0, 0.0f, 0), Quaternion.identity);
        //
        obj.tag = "Bullet";
        obj.transform.Rotate(0, 180, 0);
        //
        obj.name = "Bulletfrom" + this.name;
        //obj.transform.SetParent(transform, false);

        //
        obj.transform.localPosition = m_pos;//20161107
        //
        obj.AddComponent<BulletController>();

        shotcount = (int)(Random.value * 500.0f) + 120;
        audioSource.PlayOneShot(audioClip);
    }

    // Use this for initialization
    void Start()
    {
        m_pos = transform.localPosition;
        m_rotation = transform.localRotation.eulerAngles;
        m_rotation.y = 90.0f;
        transform.localRotation = Quaternion.Euler(m_rotation); // 回転を更新
        shotcount = (int)(Random.value * 500.0f) + 120;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        shotcount--;
        if (shotcount == 0)
            Shot();
    }

    void OnDestroy()
    {
        atbcount--;
        Debug.Log("atbcount : " + atbcount);
        if (atbcount == 0)
        {
           GameObject.Find("TopWall").SendMessage("gameClear");
        }
    }
}
