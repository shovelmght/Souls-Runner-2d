using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Manager;

namespace LesserKnown.Player
{
    public class CharacterController2D : MonoBehaviour
    {
        private Rigidbody2D rb;
        private AnimManager anim;
        private GameManagement manager;
        private DelegateControler delegate_controller;
        public Transform[] rays;
        public ContactFilter2D filter;
        public bool invicibility_test;

        private Vector2 respawn_point;
        [HideInInspector]
        public bool is_wall_running;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<AnimManager>();
            manager = FindObjectOfType<GameManagement>();
            delegate_controller = FindObjectOfType<DelegateControler>();

            respawn_point = transform.position;
        }

        private void Update()
        {
            anim.Move(rb.velocity.magnitude > 0);
        }

        public void Move(float movement_force)
        {
            rb.velocity = new Vector2(movement_force, rb.velocity.y);
           
        }

        public void Wall_Run(float movement_force)
        {
            if (!CanWallRun())
                return;

            is_wall_running = true;
            rb.velocity = new Vector2(rb.velocity.x, movement_force);
        }

        public void Jump(Vector2 jump_force)
        {
            if (!IsGrounded() && !CanWallRun())
                return;

            rb.velocity = jump_force;
        }

        public void Checkpoint(Vector2 _pos)
        {
            respawn_point = _pos;
        }

        public void Die()
        {
            if (invicibility_test)
                return;

            delegate_controller.reset_on_death();
            manager.Die();
            anim.Die_Anim();
            StartCoroutine(DieIE());            
        }

        private IEnumerator DieIE()
        {
            yield return new WaitForSeconds(.1f);
            transform.position = respawn_point;
        }

        public bool IsGrounded()
        {

            foreach (var ray in rays)
            {
                List<RaycastHit2D> hits = new List<RaycastHit2D>();

                Physics2D.Raycast(transform.position, -ray.up, filter, hits, 1.25f);

                if (hits.Count > 0)
                    return true;
            }

            return false;
        }

        public bool CanWallRun()
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

                Physics2D.Raycast(transform.position, transform.right, filter, hits, .85f);

            if (hits.Count > 0)
                return true;

            return false;
        }

        public void Flip(bool left)
        {
            if (left)
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else
                transform.rotation = Quaternion.Euler(Vector3.zero);
        }
	}
}