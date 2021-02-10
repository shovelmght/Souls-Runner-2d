using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Player
{
    public class AnimManager : MonoBehaviour
    {
        private CharacterController2D controller;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            controller = GetComponent<CharacterController2D>();
        }

        private void Update()
        {
            if (!controller.is_wall_running)
                anim.SetBool("InAir", !controller.IsGrounded());
            else if (controller.is_wall_running)
                anim.SetBool("InAir", false);



            Wall_Run(controller.is_wall_running);
        }

        public void Move(bool move)
        {
            anim.SetBool("Run", move);
        }

        public void Jump_Anim()
        {
            anim.SetTrigger("Jump");
        }

        public void Die_Anim()
        {
            anim.SetTrigger("Die");
        }

        public void Wall_Run(bool wall_run)
        {
            anim.SetBool("WallRun", wall_run);
        }
    }
}
