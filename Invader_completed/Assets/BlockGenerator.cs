using UnityEngine;
using System.Collections;

public class BlockGenerator : MonoBehaviour {

    public GameObject block;
    private GameObject objBlock;
    private static int blockCount = 0;
    private static int blockMax = 40;
//    private static int blockMax = 24;
    private float timeCount;
    private float timeStep = 0.1f;

    private int blockXstep = 3;
    private int blockZstep = -3;
    private int blockXinit = -8;
    private int blockZinit = 28;

    void GenerateBlock()
    {
        if (blockCount >= blockMax)
            return;

        //        int xPos = blockCount % 6 * blockXstep + blockXinit;
        //        int zPos = blockCount / 6 * blockZstep + blockZinit;
        int xPos = blockCount % 10 * blockXstep + blockXinit;
        int zPos = blockCount / 10 * blockZstep + blockZinit;

        objBlock = (GameObject)Instantiate(block, new Vector3(xPos, 0, zPos), Quaternion.identity);

        //Collider_setting_this_is_important
        objBlock.GetComponent<BoxCollider>().center = new Vector3(0, 0.5f, 0);
        objBlock.GetComponent<BoxCollider>().size = new Vector3(1.5f, 1.5f, 1.5f);
        objBlock.layer = LayerMask.NameToLayer("beam_and_bot");
        //
        objBlock.tag = "Enemy";
        //
        objBlock.name = "Block" + blockCount;
        objBlock.transform.SetParent(transform, false);

        objBlock.AddComponent<BlockController>();

        blockCount++;
    }

    // Use this for initialization
	void Start () {
        timeCount = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < blockMax; i++)
            GenerateBlock();
	}
}
