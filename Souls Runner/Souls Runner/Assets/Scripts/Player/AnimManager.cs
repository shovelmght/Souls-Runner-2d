using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Player
{
    /// <summary>
    /// This script controlls all the player animations
    /// </summary>
    public class AnimManager : MonoBehaviour
    {
        private CharacterController2D controller;
        private Animator anim;
        private bool climbing;

        /// <summary>
        /// The ID for the Attack animation
        /// </summary>
        private int attack_hash = Animator.StringToHash("Base Layer.Attack");
        private AnimatorStateInfo anim_state;

        private void Start()
        {
            anim = GetComponent<Animator>();
            controller = GetComponent<CharacterController2D>();
        }

        private void Update()
        {
            anim_state = anim.GetCurrentAnimatorStateInfo(0);

            Wall_Fall(controller.IsWallFalling());

            //Sets the animation for falling
            anim.SetBool("InAir", (!controller.IsGrounded() && !controller.IsWallFalling() && !climbing) && !controller.IsOnPlatform());
        }

        /// <summary>
        /// Verifies if the attack animation is playing
        /// </summary>
        /// <returns></returns>
        public bool Is_Attacking()
        {
            return anim_state.fullPathHash == attack_hash;
        }

        /// <summary>
        /// Activates/Deactivates the climbing animation
        /// </summary>
        /// <param name="climb"></param>
        public void Climb(bool climb)
        {
            climbing = climb;
            anim.SetBool("Climb", climb);
        }

        /// <summary>
        /// Sets the idle climbing animation, when you hang on a ladder
        /// </summary>
        /// <param name="climb_stay"></param>
        public void Climb_Stay(bool climb_stay)
        {
            climbing = climb_stay;
            anim.SetBool("ClimbStay", climb_stay);
        }
        
        /// <summary>
        /// Activates/Deactivates the moving animation
        /// </summary>
        /// <param name="move"></param>
        public void Move(bool move)
        {
            anim.SetBool("Run", move);
        }

        /// <summary>
        /// The jump animation
        /// </summary>
        public void Jump_Anim()
        {
            anim.SetTrigger("Jump");
        }

        /// <summary>
        /// The double jump animaiton
        /// This mechanic is not implemented
        /// Don't try to implement for now
        /// </summary>
        public void Double_Jump_Anim()
        {
            anim.SetTrigger("DoubleJump");
        }

        /// <summary>
        /// The death animation
        /// Still need to implement mechanic
        /// </summary>
        public void Die_Anim()
        {
            anim.SetTrigger("Die");
        }

        /// <summary>
        /// Activates/Deactivates the wall run animation
        /// Uhm, I don't think this exists, but I'm scare to remove it
        /// </summary>
        /// <param name="wall_run"></param>
        public void Wall_Run(bool wall_run)
        {
            anim.SetBool("WallRun", wall_run);
        }

        /// <summary>
        /// Activates/Deactivates the wall fall animation
        /// </summary>
        /// <param name="wall_falling"></param>
        public void Wall_Fall(bool wall_falling)
        {
            anim.SetBool("WallFall", wall_falling);
        }

        /// <summary>
        /// Get hurt animation
        /// </summary>
        public void Get_Hit()
        {
            anim.SetTrigger("Hit");
        }

        /// <summary>
        /// Activates the attack animation
        /// </summary>
        public void Attack()
        {
            anim.SetTrigger("Attack");
        }
    }
}
