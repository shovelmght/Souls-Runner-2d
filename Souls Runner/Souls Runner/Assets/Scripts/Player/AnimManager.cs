using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Player
{
    public class AnimManager : MonoBehaviour
    {
        private CharacterController2D controller;
        private Animator anim;
        private bool climbing;

        private void Start()
        {
            anim = GetComponent<Animator>();
            controller = GetComponent<CharacterController2D>();
        }

        private void Update()
        {
            Wall_Fall(controller.IsWallFalling());



            anim.SetBool("InAir", (!controller.IsGrounded() && !controller.IsWallFalling() && !climbing) && !controller.IsOnPlatform());
        }

        public void Climb(bool climb)
        {
            climbing = climb;
            anim.SetBool("Climb", climb);
        }

        public void Climb_Stay(bool climb_stay)
        {
            climbing = climb_stay;
            anim.SetBool("ClimbStay", climb_stay);
        }
        
        public void Move(bool move)
        {
            anim.SetBool("Run", move);
        }

        public void Jump_Anim()
        {
            anim.SetTrigger("Jump");
        }

        public void Double_Jump_Anim()
        {
            anim.SetTrigger("DoubleJump");
        }

        public void Die_Anim()
        {
            anim.SetTrigger("Die");
        }

        public void Wall_Run(bool wall_run)
        {
            anim.SetBool("WallRun", wall_run);
        }

        public void Wall_Fall(bool wall_falling)
        {
            anim.SetBool("WallFall", wall_falling);
        }

        public void Get_Hit()
        {
            anim.SetTrigger("Hit");
        }
    }
}
