using UnityEngine;
using LesserKnown.Player;
namespace LesserKnown.TrapsAndHelpers
{
    public class Disabler: MonoBehaviour
    {        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<CharacterController2D>().Die();
            }
        }
    }
}
