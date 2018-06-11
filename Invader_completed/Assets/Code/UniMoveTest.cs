/**
 * UniMove API - A Unity plugin for the PlayStation Move motion controller
 * Copyright (C) 2012, 2013, Copenhagen Game Collective (http://www.cphgc.org)
 * 					         Patrick Jarnfelt
 * 					         Douglas Wilson (http://www.doougle.net)
 * 
 * 
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *    1. Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *
 *    2. Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 **/

using UnityEngine;
using System;
using System.Collections.Generic;

public class UniMoveTest : MonoBehaviour
{
    //PSMOVEの初期セッティング
	// We save a list of Move controllers.
	public static List<UniMoveController> moves = new List<UniMoveController>();
	private GUIStyle style;
	public GUIStyleState styleState;
	public GameObject left, right, racket;
	public bool left_touch = false;
	public bool right_touch = false;
	public bool isCollide = false;
	public float timer = 0;
	public const float Collide_time = 0.15f;

    public static float trigger;
   public static Vector3 Gyro;

	void Start() 
	{
		/* NOTE! We recommend that you limit the maximum frequency between frames.
		 * This is because the controllers use Update() and not FixedUpdate(),
		 * and yet need to update often enough to respond sufficiently fast.
		 * Unity advises to keep this value "between 1/10th and 1/3th of a second."
		 * However, even 100 milliseconds could seem slightly sluggish, so you
		 * might want to experiment w/ reducing this value even more.
		 * Obviously, this should only be relevant in case your framerare is starting
		 * to lag. Most of the time, Update() should be called very regularly.
		 */

		//If you have changed these names, please replace below "names"
		left = GameObject.Find("LeftWall");
		right = GameObject.Find("RightWall");
//		racket = GameObject.Find("Cube(Racket)");
		style = new GUIStyle();
		style.fontSize = 20;

		Time.maximumDeltaTime = 0.1f;
		
		int count = UniMoveController.GetNumConnected();
		
		// Iterate through all connections (USB and Bluetooth)
		for (int i = 0; i < count; i++) 
		{
			UniMoveController move = gameObject.AddComponent<UniMoveController>();	// It's a MonoBehaviour, so we can't just call a constructor
			
			// Remember to initialize!
			if (move.Init(i) == PSMove_Connect_Status.MoveConnect_NoData) 
			{	
				Destroy(move);	// If it failed to initialize, destroy and continue on
				continue;
			}
			
			// This example program only uses Bluetooth-connected controllers
			PSMoveConnectionType conn = move.ConnectionType;
			if (conn == PSMoveConnectionType.Unknown || conn == PSMoveConnectionType.USB) 
			{
				Destroy(move);
			}
			else 
			{
				moves.Add(move);
				move.OnControllerDisconnected += HandleControllerDisconnected;
				
				// 球体LEDの基本セッティング　赤色
				move.SetLED(Color.red);
			}
		}
	}

    void OnCollisionEnter(Collision col)	//このオブジェクトが衝突する時実行される関数
	{
        if (col.gameObject.name == "Sphere")//衝突したオブジェクトの名前がSphereだったら
		{
            isCollide = true;	//このオブジェクトが衝突すると変数isCollideの状態trueに変える
		}
		else
		{
            isCollide = false;	//衝突しないときはfalseに
		}
	}

    void Update() //1フレームが更新されるたび呼び出される関数
	{

        foreach (UniMoveController move in moves) //PSMoveが正常的に繋がっている時に実行される
		{
			// Instead of this somewhat kludge-y check, we'd probably want to remove/destroy
			// the now-defunct controller in the disconnected event handler below.
			if (move.Disconnected) continue;
			
			//ボタンイベント　UnityのInput.GetButtonと同じ仕組み
			if (move.GetButtonDown(PSMoveButton.Circle)){
				Debug.Log("Circle Down");
			}
			if (move.GetButtonUp(PSMoveButton.Circle)){
				Debug.Log("Circle UP");
			}
			
			// 押すボタンによって球体のLEDの色が変わる
			if (move.GetButtonDown(PSMoveButton.Circle)) 		move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Cross)) 	move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Square)) 	move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Triangle)) 	move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Move)) {
				move.SetLED(Color.black);
			}

            // トリガーボタンが押されたら振動する
            move.SetRumble(move.Trigger);
            trigger = move.Trigger;

			//transform.localPosition = move.Position;
			transform.localRotation = move.Orientation;
			
			//PSMoveの球体の座標によってオブジェクトの座標が変わる
			if ((float)PSMoveDetect.Sx > left.transform.position.x + racket.transform.localScale.x/2 
			    && (float)PSMoveDetect.Sx < right.transform.position.x - racket.transform.localScale.x/2) //ゲーム内ではみ出さないようにオブジェクトの移動範囲を制限
            {
				transform.position = new Vector3(((float)PSMoveDetect.Sx), GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
				left_touch = false;
				right_touch = false;
			}
			else if ((float)PSMoveDetect.Sx <= left.transform.position.x)
			{
				transform.position = new Vector3(left.transform.position.x + racket.transform.localScale.x/2, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
				left_touch = true; 
			}

			else if ((float)PSMoveDetect.Sx >= right.transform.position.x)
			{
				transform.position = new Vector3(right.transform.position.x - racket.transform.localScale.x/2, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
				right_touch = true;
			}

			//PSMoveの球体の座標によってオブジェクトに掛かる力量が変わる
			//rigidbody.AddForce( 
			//                 transform.right * Input.GetAxisRaw( "Horizontal" ) * Accel, 
			//               ForceMode.Impulse );

            //if( (float)PSMoveDetect.Sx / 3 > 0)
            //	rigidbody.AddForce(transform.right * ((float)PSMoveDetect.Sx), ForceMode.Impulse );

            
            if (isCollide == true) //関数OnCollisionEnterでisCollideの値がtrueに変わった場合
            {
                timer += Time.deltaTime; //0から時間を計る
                if (timer < Collide_time) //計った時間が0からCollide_time以内の場合
				{
					move.SetLED(Color.magenta); //LEDの色を赤色にする
                    move.SetRumble(1f); //1の硬度で振動する
				}
                else //計った時間が15msを超える場合
				{
                    timer = 0; //タイマーを0に戻す
					move.SetLED(Color.red);
					isCollide = false;
				}
			}
            Gyro = moves[0].Gyro;
//            Debug.Log("UniMoveTest Gyro: " + moves[0].Gyro);
		}
    }
	
	void HandleControllerDisconnected (object sender, EventArgs e) //
	{
		// TODO: Remove this disconnected controller from the list and maybe give an update to the player
	}
	
	void OnGUI() 
	{
        /*
		string display = "";
		
		if (moves.Count > 0) 
		{
			for (int i = 0; i < moves.Count; i++) 
			{
				display += string.Format("PS Move {0}: ax:{1:0.000}, ay:{2:0.000}, az:{3:0.000} gx:{4:0.000}, gy:{5:0.000}, gz:{6:0.000}, posx:{7:0.000}, posy:{8:0.000}, posz:{9:0.000}\n", 
				                         i+1, moves[i].Acceleration.x, moves[i].Acceleration.y, moves[i].Acceleration.z,
                                         moves[i].Gyro.x, moves[i].Gyro.y, moves[i].Gyro.z, PSMoveDetect.Sx, PSMoveDetect.Sy, PSMoveDetect.Sz);
                                //PSmoveの値などを画面にテキストで表示 ax,ay,az:加速度センサーのx,y,z値 gx,gy,gz:ジャイロセンサーのx,y,z値 posx,posy,posz:球体のx,y,z値
			}
		}
		else display = "No Bluetooth-connected controllers found. Make sure one or more are both paired and connected to this computer.";
		GUI.Label(new Rect(10, Screen.height-100, 500, 100), display);

		style.normal = styleState;
		styleState.textColor = Color.white;

		if (left_touch){
			GUI.Label( new Rect(Screen.width/4, Screen.height/4, Screen.width/4, Screen.height/4), "   Racket is touching Left wall!" ,style);
		}
		if (right_touch) {
			GUI.Label( new Rect(Screen.width/4, Screen.height/4, Screen.width/4, Screen.height/4), "  Racket is touching Right wall!" ,style);
		}*/
	}
}
