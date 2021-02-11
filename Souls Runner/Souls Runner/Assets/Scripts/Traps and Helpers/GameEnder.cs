using UnityEngine;
using LesserKnown.Manager;

namespace LesserKnown.TrapsAndHelpers
{
    public class GameEnder: MonoBehaviour
    {
        private GameManagement game_manager;

        private void Start()
        {
            game_manager = FindObjectOfType<GameManagement>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                game_manager.Game_Over(true);
        }
    }
}
