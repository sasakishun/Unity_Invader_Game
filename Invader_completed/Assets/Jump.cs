using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    CharacterController controller;
    Animator animator;
    Vector3 velocity;
    Vector3 movedirection = Vector3.zero;
    public float speedJump = 1.0f;
    public float jumpingSpeed;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
            if (Input.GetKeyDown("space")){
            animator.SetTrigger("Jump");
            Vector3 Jump_power = new Vector3(0, 9.8f, 0);
            controller.Move(Jump_power * jumpingSpeed * Time.deltaTime);
        }
        //ジャンプ中にも移動を可能にする
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("TopToGround"))
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
            controller.Move(stickDirection * speedJump * Time.deltaTime);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop"))
            {
                Vector3 Jump_power = new Vector3(0, -9.8f, 0);
                controller.Move(Jump_power * Time.deltaTime);
            }
        }
    }

  }
