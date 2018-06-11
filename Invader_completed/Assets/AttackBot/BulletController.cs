using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
/*	private Vector3 m_pos;
	// Use this for initialization
	void Start () {
        m_pos += new Vector3(0f, 0.7f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
		m_pos.x += 0.1f;
        if (m_pos.z <= -30.0f)
            Destroy(this);
        transform.position = m_pos;  // 形状位置を更新
        //transform.localPosition = m_pos;  // 形状位置を更新

	}
*/
    private Vector3 m_pos;
    // Use this for initialization
    void Start()
    {
        m_pos = transform.position;
        m_pos += new Vector3(0f, 0.7f, 0f);

    }


    // Update is called once per frame
    void Update()
    {
        m_pos.z -= 0.1f;
        if (m_pos.z <= -30.0f)
            Destroy(this);
        transform.position = m_pos;  // 形状位置を更新
    }

}
