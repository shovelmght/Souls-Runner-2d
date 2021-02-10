using UnityEngine;
using LesserKnown.Manager;
namespace LesserKnown.TrapsAndHelpers
{
    public class PassageActivator: MonoBehaviour
    {
        private DelegateControler delegate_controller;
        private bool isActive;
        public DeathPlatform[] death_platforms;


        private void Start()
        {
            delegate_controller = FindObjectOfType<DelegateControler>();

            foreach (var platform in death_platforms)
            {
                delegate_controller.reset_on_death += platform.ResetPlatform;
            }

            delegate_controller.reset_on_death += Reset_Object;
        }

        public void Reset_Object()
        {
            isActive = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                isActive = true;
                foreach (var platform in death_platforms)
                {
                    platform.Activate();
                }
            }
        }
    }
}
