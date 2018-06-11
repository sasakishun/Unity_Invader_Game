using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GenerateBeam : MonoBehaviour
{
    public static bool firstbeam = true;
    public GameObject anchor;
    public GameObject beam;
    private GameObject objBeam;
    private GameObject objBeam2;
    private GameObject objBeam3;
    private GameObject objBeam4;
    private GameObject objBeam5;
    private bool beamIsFired = false;
    public AudioSource[] voice;
    //vioce[4]->garasu_no_wareru_oto

    //public UniMoveController move;

    private float beam_time_limit = 1;
    private bool isOnce = false;

    public GameObject timelimit;
    enum guiBeam {Circle = 0, Cross, Square, Triangle/*, Select*/ };
    public static int state = (int)guiBeam.Circle;

    public GameObject circle;
//    public GameObject select;
    public GameObject cross;
    public GameObject square;
    public GameObject triangle;

    public GameObject circle_back;
    public GameObject cross_back;
    public GameObject square_back;
    public GameObject triangle_back;

    public GameObject recast_circle;
    public GameObject recast_cross;
    public GameObject recast_square;
    public GameObject recast_triangle;

    public struct recast_time {
        public float circle;
        public float cross;
        public float square;
        public float triangle;
        public bool cir;
        public bool cro;
        public bool squ;
        public bool tri;
    };

    public recast_time recast = new recast_time();

    public Text text_circle;
    public Text text_cross;
    public Text text_square;
    public Text text_triangle;

    public GameObject circle_black;
    public GameObject cross_black;
    public GameObject square_black;
    public GameObject triangle_black;

    void DestroyBeam()
    {
        Destroy(objBeam);
        Destroy(objBeam2);
        Destroy(objBeam3);
        Destroy(objBeam4);
        Destroy(objBeam5);
        if (beamIsFired)
        {
            beamIsFired = false;
        }
        beam_time_limit = 1;
    }

    void GenerateBeams()
    {
        if (beamIsFired)
        {
            DestroyBeam();
            return;
        }

        objBeam = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
        objBeam.name = "instant beam1";
        objBeam.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
        objBeam.transform.SetParent(transform, false);
        beamIsFired = true;
        if (state == (int)guiBeam.Circle || (state == (int)guiBeam.Square))
        {
            if ((state == (int)guiBeam.Square))
            {
                objBeam2 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
                objBeam2.name = "instant beam2";
                objBeam2.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
                objBeam2.GetComponent<GeroBeam>().attach_position = new Vector3(1f, -0.3f, 0);
                objBeam2.transform.SetParent(transform, false);
                objBeam3 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
                objBeam3.name = "instant beam3";
                objBeam3.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
                objBeam3.GetComponent<GeroBeam>().attach_position = new Vector3(-1f, -0.3f, 0);
                objBeam3.transform.SetParent(transform, false);
                beamIsFired = true;
            }
        }
        if (state == (int)guiBeam.Cross)
        {
            DestroyBeam();
            objBeam2 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
            objBeam2.name = "instant beam2";
            objBeam2.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
            objBeam2.GetComponent<GeroBeam>().attach_position = new Vector3(1f, -0.3f, 0);
            objBeam2.GetComponent<GeroBeam>().attach_rotation = new Quaternion(0, 1, 0, 5f);
            objBeam2.transform.SetParent(transform, false);
            objBeam3 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
            objBeam3.name = "instant beam3";
            objBeam3.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
            objBeam3.GetComponent<GeroBeam>().attach_position = new Vector3(-1f, -0.3f, 0);
            objBeam3.GetComponent<GeroBeam>().attach_rotation = new Quaternion(0, 1, 0, -5f);
            objBeam3.transform.SetParent(transform, false);
            beamIsFired = true;
        }
        if (state == (int)guiBeam.Triangle)
        {
            objBeam2 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
            objBeam2.name = "instant beam2";
            objBeam2.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
            objBeam2.GetComponent<GeroBeam>().attach_position = new Vector3(1f, -0.3f, 0);
            objBeam2.GetComponent<GeroBeam>().attach_rotation = new Quaternion(0, 1, 0, 10f);
            objBeam2.transform.SetParent(transform, false);
            objBeam3 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
            objBeam3.name = "instant beam3";
            objBeam3.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
            objBeam3.GetComponent<GeroBeam>().attach_position = new Vector3(-1f, -0.3f, 0);
            objBeam3.GetComponent<GeroBeam>().attach_rotation = new Quaternion(0, 1, 0, -10f);
            objBeam3.transform.SetParent(transform, false);

            objBeam4 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
            objBeam4.name = "instant beam2";
            objBeam4.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
            objBeam4.GetComponent<GeroBeam>().attach_position = new Vector3(1.2f, -0.3f, 0);
            objBeam4.GetComponent<GeroBeam>().attach_rotation = new Quaternion(0, 1, 0, 5f);
            objBeam4.transform.SetParent(transform, false);
            objBeam5 = (GameObject)Instantiate(beam, GameObject.Find("SD_unitychan_humanoid").transform.position, Quaternion.identity);
            objBeam5.name = "instant beam3";
            objBeam5.GetComponent<GeroBeam>().anchor = GameObject.Find("SD_unitychan_humanoid");
            objBeam5.GetComponent<GeroBeam>().attach_position = new Vector3(-1.2f, -0.3f, 0);
            objBeam5.GetComponent<GeroBeam>().attach_rotation = new Quaternion(0, 1, 0, -5f);
            objBeam5.transform.SetParent(transform, false);
            beamIsFired = true;
            voice[3].PlayOneShot(voice[3].clip);
            voice[5].PlayOneShot(voice[5].clip);
        }
        else {
            voice[0].PlayOneShot(voice[0].clip);
        }
        voice[5].PlayOneShot(voice[5].clip);
    }

    public void changeBeam(int newstate)
    {
        if (state == newstate /*&& state != (int)guiBeam.Select*/)
        {
/*            circle.GetComponent<Image>().enabled = false;
            cross.GetComponent<Image>().enabled = false;
            triangle.GetComponent<Image>().enabled = false;
            square.GetComponent<Image>().enabled = false;
//            select.GetComponent<Image>().enabled = true;
            state = (int)guiBeam.Select;
            */
            return;
        }

        if (newstate == (int)guiBeam.Circle)
        {
            circle.GetComponent<Image>().enabled = true;
            cross.GetComponent<Image>().enabled = false;
            triangle.GetComponent<Image>().enabled = false;
            square.GetComponent<Image>().enabled = false;
//            select.GetComponent<Image>().enabled = false;
            state = (int)guiBeam.Circle;
            return;
        }
        if (newstate == (int)guiBeam.Cross)
        {
            circle.GetComponent<Image>().enabled = false;
            cross.GetComponent<Image>().enabled = true;
            triangle.GetComponent<Image>().enabled = false;
            square.GetComponent<Image>().enabled = false;
//            select.GetComponent<Image>().enabled = false;
            state = (int)guiBeam.Cross;
            return;
        }
        if (newstate == (int)guiBeam.Square)
        {
            circle.GetComponent<Image>().enabled = false;
            cross.GetComponent<Image>().enabled = false;
            triangle.GetComponent<Image>().enabled = false;
            square.GetComponent<Image>().enabled = true;
//            select.GetComponent<Image>().enabled = false;
            state = (int)guiBeam.Square;
            return;
        }
        if (newstate == (int)guiBeam.Triangle)
        {
            circle.GetComponent<Image>().enabled = false;
            cross.GetComponent<Image>().enabled = false;
            triangle.GetComponent<Image>().enabled = true;
            square.GetComponent<Image>().enabled = false;
//            select.GetComponent<Image>().enabled = false;
            state = (int)guiBeam.Triangle;
            voice[2].PlayOneShot(voice[2].clip);
            return;
        }
        return;
    }
    public void destroyVoice()
    {
        voice[6].PlayOneShot(voice[6].clip);
    }

    // Use this for initialization
    void Start()
    {
        if (firstbeam)
        {
            Destroy(this.GetComponent<GameObject>());
            firstbeam = false;
        }
        this.anchor = GameObject.Find("SD_unitychan_humanoid");
        voice = anchor.GetComponents<AudioSource>();
//       voice2 = anchor.GetComponent<AudioSource>();
        //move = gameObject.AddComponent<UniMoveController>();
        circle = GameObject.Find("Circle");
        cross = GameObject.Find("Cross");
        square = GameObject.Find("Square");
        triangle = GameObject.Find("Triangle");
//        select = GameObject.Find("Select_beam");

        circle_back = GameObject.Find("Circle_back");
        cross_back = GameObject.Find("Cross_back");
        square_back = GameObject.Find("Square_back");
        triangle_back = GameObject.Find("Triangle_back");
        circle_back.GetComponent<Image>().enabled = true;
        cross_back.GetComponent<Image>().enabled = true;
        square_back.GetComponent<Image>().enabled = true;
        triangle_back.GetComponent<Image>().enabled = true;

        recast_circle = GameObject.Find("recast_circle");
        recast_cross = GameObject.Find("recast_cross");
        recast_square = GameObject.Find("recast_square");
        recast_triangle = GameObject.Find("recast_triangle");

        recast.circle = 0f;
        recast.cross = 0f;
        recast.square = 0f;
        recast.triangle = 0f;
        recast.cir = true;
        recast.cro = true;
        recast.squ = true;
        recast.tri = true;

        text_circle = recast_circle.GetComponent<Text>();
        text_cross = recast_cross.GetComponent<Text>();
        text_square = recast_square.GetComponent<Text>();
        text_triangle = recast_triangle.GetComponent<Text>();

        text_circle.text = "";
        text_cross.text = "";
        text_square.text = "";
        text_triangle.text = "";

        if (state == (int)guiBeam.Circle)
            circle.GetComponent<Image>().enabled = true;
        else
            circle.GetComponent<Image>().enabled = false;
        if (state == (int)guiBeam.Cross)
            cross.GetComponent<Image>().enabled = true;
        else
            cross.GetComponent<Image>().enabled = false;
        if (state == (int)guiBeam.Square)
            square.GetComponent<Image>().enabled = true;
        else
            square.GetComponent<Image>().enabled = false;
        if (state == (int)guiBeam.Triangle)
            triangle.GetComponent<Image>().enabled = true;
        else
            triangle.GetComponent<Image>().enabled = false;
        /*        if (state == (int)guiBeam.Select)
                    select.GetComponent<Image>().enabled = true;
                else
                    select.GetComponent<Image>().enabled = false;
        */

        circle_black = GameObject.Find("Circle_black");
        cross_black = GameObject.Find("Cross_black");
        square_black = GameObject.Find("Square_black");
        triangle_black = GameObject.Find("Triangle_black");
        circle_black.GetComponent<Image>().enabled = false;
        cross_black.GetComponent<Image>().enabled = false;
        square_black.GetComponent<Image>().enabled = false;
        triangle_black.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        /*Debug.Log(UniMoveTest.trigger);
        if (Input.GetKeyDown(KeyCode.LeftShift) || UniMoveTest.trigger > 0.1f || UniMoveTest.moves[0].GetButtonDown(PSMoveButton.Circle))
        {
            if (isOnce)
            {
                isOnce = false;
                this.GenerateBeams();
            }
        }*/
        if (Input.GetKeyDown(KeyCode.LeftShift) /*|| UniMoveTest.trigger > 0.1f*/)
        {
            if (isOnce)
            {
                isOnce = false;
                if (recast.cir && state == (int)guiBeam.Circle)
                {
                    recast.circle = 1.9f;
                    recast.cir = false;
                    circle_black.GetComponent<Image>().enabled = true;
                    this.GenerateBeams();
                }
                else if (recast.cro && state == (int)guiBeam.Cross)
                {
                    recast.cross = 2.9f;
                    recast.cro = false;
                    cross_black.GetComponent<Image>().enabled = true;
                    this.GenerateBeams();
                }
                else if (recast.squ && state == (int)guiBeam.Square)
                {
                    recast.square = 5.9f;
                    recast.squ = false;
                    square_black.GetComponent<Image>().enabled = true;
                    this.GenerateBeams();
                }
                else if (recast.tri && state == (int)guiBeam.Triangle)
                {
                    recast.triangle = 20.9f;
                    recast.tri = false;
                    triangle_black.GetComponent<Image>().enabled = true;
                    this.GenerateBeams();
                }
            }
        }
        else if (Input.GetKeyDown("1"))
        {
            this.DestroyBeam();
        }
        else if (Input.GetKeyDown("2") /*|| UniMoveTest.moves[0].GetButtonDown(PSMoveButton.Circle)*/)
        {
            changeBeam((int)guiBeam.Circle);
            voice[4].PlayOneShot(voice[4].clip);
        }
        else if (Input.GetKeyDown("3") /*|| UniMoveTest.moves[0].GetButtonDown(PSMoveButton.Cross)*/)
        {
            changeBeam((int)guiBeam.Cross);
            voice[4].PlayOneShot(voice[4].clip);
        }
        else if (Input.GetKeyDown("4") /*|| UniMoveTest.moves[0].GetButtonDown(PSMoveButton.Square)*/)
        {
            changeBeam((int)guiBeam.Square);
            voice[4].PlayOneShot(voice[4].clip);
        }
        else if (Input.GetKeyDown("5") /*|| UniMoveTest.moves[0].GetButtonDown(PSMoveButton.Triangle)*/)
        {
            changeBeam((int)guiBeam.Triangle);
            voice[4].PlayOneShot(voice[4].clip);
        }
        else {
            isOnce = true;
        }
        /*        if(move.GetButtonDown(PSMoveButton.Circle)){
                }
        */
        if (beamIsFired) {
            beam_time_limit -= Time.deltaTime;
        }
        if(beam_time_limit < 0)
        {
            DestroyBeam();
        }

        if (recast.circle >= 1)
        {
            recast.circle -= Time.deltaTime;
        }
        if (recast.cross >= 1)
        {
            recast.cross -= Time.deltaTime;
        }
        if (recast.square >= 1)
        {
            recast.square -= Time.deltaTime;
        }
        if (recast.triangle >= 1)
        {
            recast.triangle -= Time.deltaTime;
        }

        if (recast.circle < 1)
        {
            recast.cir = true;
            circle_black.GetComponent<Image>().enabled = false;
        }
        if (recast.cross < 1)
        {
            recast.cro = true;
            cross_black.GetComponent<Image>().enabled = false;
        }
        if (recast.square < 1)
        {
            recast.squ = true;
            square_black.GetComponent<Image>().enabled = false;
        }
        if (recast.triangle < 1)
        {
            recast.tri = true;
            triangle_black.GetComponent<Image>().enabled = false;
        }

        if (recast.cir)
        {
            text_circle.text = " ";
        }
        else
        {
            text_circle.text = "" + (int)recast.circle;
        }
        if (recast.cro)
        {
            text_cross.text = " ";
        }
        else
        {
            text_cross.text = "" + (int)recast.cross;
        }
        if (recast.squ)
        {
            text_square.text = " ";
        }
        else
        {
            text_square.text = "" + (int)recast.square;
        }
        if (recast.tri)
        {
            text_triangle.text = " ";
        }
        else
        {
            text_triangle.text = "" + (int)recast.triangle;
        }

    }
}
