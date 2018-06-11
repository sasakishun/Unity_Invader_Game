 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
  
[RequireComponent(typeof(Animator))]  

//Name of class must be name of file as well

public class LocomotionPlayer : MonoBehaviour {

    protected Animator animator;
//    public CharacterController charactercontroller;

    private float speed = 0;
    private float direction = 0;
    private Locomotion locomotion = null;

	// Use this for initialization
	void Start () 
	{
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
//        charactercontroller = GetComponent<CharacterController>();
	}
    
	void Update () 
	{
        //Debug.Log("aaa" + UniMoveTest.Gyro.x + UniMoveTest.Gyro.y + UniMoveTest.Gyro.z);
        if (animator && Camera.main)
		{
            JoystickToEvents.Do(transform,Camera.main.transform, ref speed, ref direction);
            /*display += string.Format("PS Move {0}: ax:{1:0.000}, ay:{2:0.000}, az:{3:0.000} gx:{4:0.000}, gy:{5:0.000}, gz:{6:0.000}, posx:{7:0.000}, posy:{8:0.000}, posz:{9:0.000}\n",
                                         1, moves[0].Acceleration.x, moves[0].Acceleration.y, moves[0].Acceleration.z,
                                         moves[0].Gyro.x, moves[0].Gyro.y, moves[0].Gyro.z, PSMoveDetect.Sx, PSMoveDetect.Sy, PSMoveDetect.Sz);
            */
           // GameObject.Find("SD_unitychan_humanoid").GetComponent<>().
            locomotion.Do(speed * 6, direction * 180);
    //        locomotion.Do(speed * 6, UniMoveTest.Gyro.y * 360);
        }
//        charactercontroller.transform.position.Set(charactercontroller.transform.position.x,GameObject.Find("Character1_Hips").GetComponent<Transform>().position.y, charactercontroller.transform.position.z);
	}
}
