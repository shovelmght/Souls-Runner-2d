using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    public class PlatformBehaviour: MonoBehaviour
    {
        private PlatformEffector2D effector;

        private void Start()
        {
            effector = GetComponent<PlatformEffector2D>();
        }
        private void Update()
        {
            /*
            if (Input.GetKey(KeyCode.S))
                effector.rotationalOffset = 180f;
            else
                effector.rotationalOffset = 0f;
            */
        }

    }
}
