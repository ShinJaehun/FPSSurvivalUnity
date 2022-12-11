using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_Controller;
    private Vector3 move_Direction;
    public float speed = 5f;
    private float gravity = 20f;
    public float jump_Force = 10f;
    private float vertical_Velocity;

    void Awake() {
        character_Controller = GetComponent<CharacterController>();

    }
    
    
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer() {
        // X, Z 방향으로 이동키 적용
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        
        // print("HORIZONTAL: " + Input.GetAxis("Horizontal"));
        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();

        character_Controller.Move(move_Direction);

    }

    void ApplyGravity() {
        // Y 방향으로 받는 힘(중력) 적용
        // if (character_Controller.isGrounded) {
        //     vertical_Velocity -= gravity * Time.deltaTime;
        //     PlayerJump();
        // } else {
        //     vertical_Velocity -= gravity * Time.deltaTime;
        // }

        // 어차피 점프할 때 isGrounded 확인을 하므로...
        vertical_Velocity -= gravity * Time.deltaTime;
        PlayerJump();
        move_Direction.y = vertical_Velocity * Time.deltaTime;
        // move_Direction.y = vertical_Velocity;
        // Time.deltaTime 적용하지 않으면 부자연스러운 움직임
    }

    void PlayerJump(){
        // 그래서 더블 점프는 없는 거지?
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;
        }
    }
}
