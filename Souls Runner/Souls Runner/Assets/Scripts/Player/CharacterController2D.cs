using LesserKnown.Public;
using UnityEngine;
using LesserKnown.TrapsAndHelpers;
using System.Collections;
using LesserKnown.Network;

namespace LesserKnown.Player
{
    public class CharacterController2D : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Collider2D p_collider;
        private SpriteRenderer player_sprite;
        private AnimManager anim_manager;
        [Tooltip("Used to detect the terrain")]
        public LayerMask ground;
        public LayerMask platform;

        [Space(10)]
        [Header("Modifiers")]
        [Range(1f,2f)]
        public float fall_modifier;

        [Space(10)]
       // private PlayerNetwork player_network;
        [HideInInspector]
        public bool wall_jump;
        private float touching_wall;
        private bool can_climb_ladder;
        [HideInInspector]
        public bool is_climbing_ladder;
        [HideInInspector]
        public bool is_invicible;
        private bool local_left;

        [Space(10)]
        [Header("Boundaries Ground")]
        public Vector2 boundary_size;
        public Vector2 boundary_placement;

        [Space(10)]
        [Header("Boundaries Right")]
        public Vector2 boundary_size_r;
        public Vector2 boundary_placement_r;

        [Space(10)]
        [Header("Boundaries Left")]
        public Vector2 boundary_size_l;
        public Vector2 boundary_placement_l;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            player_sprite = GetComponent<SpriteRenderer>();
            anim_manager = GetComponent<AnimManager>();
           // player_network = GetComponent<PlayerNetwork>();
            p_collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            //player_sprite.flipX = player_network.left;
            player_sprite.flipX = local_left;

            if (IsWallFalling())
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / fall_modifier);

            if (is_climbing_ladder)
            {
                p_collider.isTrigger = true;
                rb.gravityScale = 0f;
            }
            else
            {
                p_collider.isTrigger = false;
                rb.gravityScale = 6.5f;
            }

            if (IsWallFalling() && IsTouchingLeft())
                Flip(false);
            else if (IsWallFalling() && IsTouchingRight())
                Flip(true);

            anim_manager.Move(rb.velocity.magnitude > 0 && (IsGrounded() || IsOnPlatform()));

        }

        /// <summary>
        /// Move player
        /// </summary>
        /// <param name="movement_speed">The player movement speed</param>
        public void Move(float movement_speed)
        {
            if (wall_jump)
                return;

            anim_manager.Climb(false);
            anim_manager.Climb_Stay(false);

            if(movement_speed != 0)
            is_climbing_ladder = false;

            if(!IsGrounded() && !IsOnPlatform())
            {
                if (IsTouchingRight())
                {
                    if (movement_speed > 0)
                        return;
                }

                if (IsTouchingLeft())
                {
                    if (movement_speed < 0)
                        return;
                }
            }


            rb.velocity = new Vector2(movement_speed, rb.velocity.y);

            if (movement_speed > 0)
                Flip(false);
            else if (movement_speed < 0)
                Flip(true);
        }

        public void Get_Hit(int amount)
        {
            is_invicible = true;
            anim_manager.Get_Hit();            
            bool death = PublicVariables.Lose_Health(amount);

            if(death)
            {
                anim_manager.Die_Anim();
                //Disable Network Player
            }

            StartCoroutine(Reset_InvicibilityIE());
        }

        private IEnumerator Reset_InvicibilityIE()
        {
            yield return new WaitForSeconds(1.5f);
            is_invicible = false;
        }

        public void Climb(float movement_speed)
        {
            if (movement_speed != 0)
                is_climbing_ladder = true;


            if (!can_climb_ladder)
            {
                is_climbing_ladder = false;
                return;
            }

            if (!is_climbing_ladder)
                return;

            if(movement_speed == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                anim_manager.Climb_Stay(true);
                return;
            }

            anim_manager.Climb_Stay(false);
            anim_manager.Climb(true);
            rb.velocity = new Vector2(rb.velocity.x, movement_speed);
        }

        private void Flip(bool left)
        {
            /* Set left on network
            player_network.left = left;            */

            local_left = left;
        }

        /// <summary>
        /// Does the player jump
        /// </summary>
        /// <param name="jump_force">The jump force</param>
        /// <param name="move_speed">The jump force used to jump when next to a wall</param>
        public void Jump(float jump_force)
        {
           
                if (IsTouchingLeft())
                    touching_wall = 1;
                else if (IsTouchingRight())
                    touching_wall = -1;

                if ((IsTouchingLeft() || IsTouchingRight()) && !IsGrounded())
                    wall_jump = true;

            if (wall_jump)
            {
                Invoke("Wall_Jump", 0.08f);
                return;
            }

            if (!IsGrounded() && !IsOnPlatform())
                return;

           
                anim_manager.Jump_Anim();
                rb.velocity = new Vector2(rb.velocity.x, jump_force);          

            anim_manager.Climb(false);
            anim_manager.Climb_Stay(false);
            is_climbing_ladder = false;
            
        }

        public void Jump_Wall(Vector2 jump_force)
        {
            if (jump_force.x == 0)
                return;

            rb.velocity = new Vector2(jump_force.x * touching_wall, jump_force.y);
        }

        private void Wall_Jump()
        {
            wall_jump = false;
        }

        #region Public bools
        /// <summary>
        /// Verifies if the player is touching any object with the Layer Terrain bottom
        /// </summary>
        public bool IsGrounded()
        {
            
               return Physics2D.OverlapBox(new Vector2(transform.position.x + boundary_placement.x, transform.position.y - boundary_placement.y), new Vector2(boundary_size.x, boundary_size.y), 0f, ground);
            
        }

        public bool IsOnPlatform()
        {
            
                return Physics2D.OverlapBox(new Vector2(transform.position.x + boundary_placement.x, transform.position.y - boundary_placement.y), new Vector2(boundary_size.x, boundary_size.y), 0f, platform);
            
        }

        /// <summary>
        /// Verifies if the player is touching any object with Layer Terrain on left
        /// </summary>
        public bool IsTouchingLeft()
        {
            
                return Physics2D.OverlapBox(new Vector2(transform.position.x - boundary_placement_l.x, transform.position.y - boundary_placement_l.y), new Vector2(boundary_size_l.x, boundary_size_l.y), 0f, ground);
            
        }

        /// <summary>
        /// Verifies if the player is touching any object with Layer Terrain on right
        /// </summary>
        public bool IsTouchingRight()
        {
               return Physics2D.OverlapBox(new Vector2(transform.position.x + boundary_placement_r.x, transform.position.y - boundary_placement_r.y), new Vector2(boundary_size_r.x, boundary_size_r.y), 0f, ground);
            
        }

        /// <summary>
        /// Verifies if the player is falling next to a wall
        /// </summary>
        public bool IsWallFalling()
        {
             return (IsTouchingLeft() || IsTouchingRight()) && !IsGrounded(); 
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ladder")
                can_climb_ladder = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Ladder")
                can_climb_ladder = false;
        }

        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector2(transform.position.x + boundary_placement.x, transform.position.y - boundary_placement.y), new Vector2(boundary_size.x, boundary_size.y));

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector2(transform.position.x - boundary_placement_l.x, transform.position.y - boundary_placement_l.y), new Vector2(boundary_size_l.x, boundary_size_l.y));

            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(new Vector2(transform.position.x + boundary_placement_r.x, transform.position.y - boundary_placement_r.y), new Vector2(boundary_size_r.x, boundary_size_r.y));

        }
        
    }
}