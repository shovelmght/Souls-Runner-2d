using UnityEngine;
using LesserKnown.Player;

namespace LesserKnown.TrapsAndHelpers
{
    public class CheckPointSetter: MonoBehaviour
    {
        private bool isSet;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isSet)
                return;

            if(collision.tag == "Player")
            {
               /* collision.GetComponent<CharacterController2D>().Checkpoint(transform.position);*/
                isSet = true;
                anim.SetTrigger("Activate");
            }
        }
    }
}
