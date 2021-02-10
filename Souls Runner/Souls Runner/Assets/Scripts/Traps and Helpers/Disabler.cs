using UnityEngine;
using LesserKnown.Player;
namespace LesserKnown.TrapsAndHelpers
{
    public class Disabler: MonoBehaviour
    {
        public bool player_only;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Mover" && !player_only)
            {
                collision.GetComponent<Mover>().Reset();
            }else if (collision.tag == "Player")
            {
                collision.GetComponent<CharacterController2D>().Die();
            }
        }
    }
}
