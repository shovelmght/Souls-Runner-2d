using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;

namespace LesserKnown.Camera
{  
    public class CameraFollow : MonoBehaviour
    {     
        public Transform target;
        [Range(1f,10f)]
        public float camera_smoothing;
        [Range(5f, 10f)]
        public float falling_camera_smoothing;

        private Vector3 offset;

        public bool testing;
        private CharacterController2D character;

        private void Start()
        {
            if (testing)
                Set_Camera(target, null);
        }


        public void Set_Camera(Transform target, CharacterController2D character)
        {
            this.target = target;
            this.character = character;
            offset = transform.position - target.position;
        }

        public void Set_Camera_Local(Transform target)
        {
            this.target = target;
            offset = new Vector3(0,0,-10);
        }


        void Update()
        {
            if(target == null)
            {
                Debug.LogError("Missing player to follow");
                return;
            }

            float _smoothing = camera_smoothing * Time.deltaTime;

            if(character != null)
            {
                if (!character.IsGrounded())
                    _smoothing = falling_camera_smoothing * Time.deltaTime;
            }

            transform.position = Vector3.Lerp(transform.position, target.position + offset, _smoothing);
        }
    }
}
