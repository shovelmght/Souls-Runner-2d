using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController2D controller;

        public float movement_speed = 10f;
        public float jump_force = 30f;

        private float h;
        private float v;

        private void Start()
        {
            controller = GetComponent<CharacterController2D>();
        }



        private void Update()
        {

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");


            if (h > 0)
                controller.Flip(false);
            else if (h < 0)
                controller.Flip(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("JUMP");
                controller.Jump(new Vector2(0, jump_force));
            }
        }

        private void FixedUpdate()
        {
            controller.Move(h * Time.deltaTime * movement_speed);

            if (v != 0) 
                controller.Wall_Run(v * Time.deltaTime * movement_speed);
            else
                controller.is_wall_running = false;
        }
    }
}
