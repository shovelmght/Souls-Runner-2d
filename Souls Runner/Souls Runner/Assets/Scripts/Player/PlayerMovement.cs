using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController2D controller;

        [Header("Player Settings")]
        public float movement_speed = 10f;
        public float jump_force = 30f;
        [Space(10)]
        [Header("Player Input Keys")]
        public KeyCode jump_key;

        private float h;
        private float v;
        private Joystick joystick;

        private void Start()
        {
            controller = GetComponent<CharacterController2D>();
            joystick = FindObjectOfType<Joystick>();
        }



        private void Update()
        {
            
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
            }else if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                h = joystick.Horizontal;
                v = joystick.Vertical;
            }

            if (h > 0)
                controller.Flip(false);
            else if (h < 0)
                controller.Flip(true);

            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetKeyDown(jump_key))
                    Jump();
            }
        }

    

        public void Jump()
        {
            controller.Jump(new Vector2(0, jump_force));
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
