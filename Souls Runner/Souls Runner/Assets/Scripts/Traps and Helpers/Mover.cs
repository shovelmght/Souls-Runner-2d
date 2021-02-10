using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace LesserKnown.TrapsAndHelpers 
{
    public class Mover : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 movement_vector;
        private bool move_bool;

        public void Move()
        {
            gameObject.SetActive(true);
            StartCoroutine(MoveIE());
        }
        
        public void Reset()
        {
            move_bool = false;
            transform.localPosition = new Vector3(0,0,0);
            gameObject.SetActive(false);
        }

        private IEnumerator MoveIE()
        {
            move_bool = true;
            while (move_bool)
            {
                transform.position += movement_vector * Time.deltaTime;
                yield return null;
            }
        }
    }
}
