using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Camera;

namespace LesserKnown.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController2D controller;

        [Header("Player Settings")]
        public float movement_speed = 10f;
        public float climbing_speed = 8f;
        public float jump_force = 30f;
        public float wall_jump_force = 8f;
        [Space(10)]
        [Header("Player Input Keys")]
        public KeyCode jump_key;

        public bool character_swap;
        private CameraFollow cam;

        private void Start()
        {
            controller = GetComponent<CharacterController2D>();
            cam = UnityEngine.Camera.main.GetComponent<CameraFollow>();

            Swap_Character();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Swap_Character();
            }

            if (!character_swap)
                return;

            if (Input.GetKeyDown(jump_key))
                controller.Jump(jump_force);

            if (controller.wall_jump)
                controller.Jump_Wall(new Vector2(wall_jump_force, jump_force));
        }

        private void FixedUpdate()
        {
            if (!character_swap)
                return;

            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            controller.Move(h * movement_speed);
            controller.Climb(v * climbing_speed);
        }

        public void Swap_Character()
        {
            character_swap = !character_swap;

            if(character_swap)
            cam.Set_Camera_Local(transform);
        }
    }
}
