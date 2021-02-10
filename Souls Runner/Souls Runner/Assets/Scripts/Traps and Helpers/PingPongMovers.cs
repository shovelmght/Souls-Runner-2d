using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    public class PingPongMovers: MonoBehaviour
    {
        public float speed = 5f;
        public Vector3 left_angle;
        public Vector3 right_angle;

        private void Update()
        {
            var t = Mathf.PingPong(Time.time * speed, 1f);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(left_angle), Quaternion.Euler(right_angle), t);
        }
    }
}
