using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Camera
{  
    public class CameraFollow : MonoBehaviour
    {     
        public Transform target;

        private Vector3 offset;

        private void Start()
        {
            offset = transform.position - target.position;
        }


        void Update()
        {
            if(target == null)
            {
                Debug.LogError("Missing player to follow");
                return;
            }

            transform.position = Vector3.Lerp(transform.position, target.position + offset, .5f);
        }
    }
}
