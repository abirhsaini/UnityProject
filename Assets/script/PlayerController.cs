using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 3;//0:left, 1:middle, 2:right
    public float laneDistance = 4;//The distance between tow lanes

    public float JumpForce;
    public float Gravity=-20;


    // Start is called before the first frame update
    void Start()
    {
        controller =GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z=forwardSpeed;
        
        if (controller.isGrounded)
        {
            direction.y=-1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
                }
        
        }
        else {
            direction.y+=Gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            desiredLane++;
            if (desiredLane==3) {
                desiredLane=2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            desiredLane--;
            if (desiredLane==-1) {
                desiredLane=0;
            }
        }
        Vector3 targetPosition=transform.position.z* transform.forward + transform.position.y * transform.up; 
        if (desiredLane==0){
            targetPosition+= Vector3.left * laneDistance;
        }else if (desiredLane==2){
            targetPosition+= Vector3.right *laneDistance;
        }
        if (transform.position == targetPosition)
        {
            return;
        }
        
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 24 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        
        //transform.position=targetPosition;
        
    }
    private void FixedUpdate(){
        controller.Move(direction*Time.deltaTime);

    }
    private void  Jump(){
        direction.y=JumpForce;

    }
    private void  OnControllerColliderHit (ControllerColliderHit  hit)
    {
        PlayerManager.gameOver= false;
        if (hit.transform.tag=="obstacle")
        {
            PlayerManager.gameOver= true;
        }
    }
}
