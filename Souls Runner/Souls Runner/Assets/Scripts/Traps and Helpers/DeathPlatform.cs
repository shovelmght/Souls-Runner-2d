using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    public class DeathPlatform : MonoBehaviour
    {
        public GameObject[] platforms;
        public float trigger_delay = .7f;
        public float return_delay = .9f;
        public bool is_manual;
        private Coroutine trigger_co;
        private Coroutine return_co;

        private void Start()
        {
            if (is_manual)
                platforms[0].GetComponent<BoxCollider2D>().isTrigger = false;
        }
        public void Activate()
        {
            platforms[0].SetActive(false);
            platforms[1].SetActive(true);
        }

        public void ResetPlatform()
        {
            platforms[0].SetActive(true);
            platforms[1].SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                trigger_co = StartCoroutine(TriggerIE());
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                StartCoroutine(ReturnIE());
            }
        }

        private IEnumerator TriggerIE()
        {
            while(return_co != null)
            {

                yield return null;
            }

            yield return new WaitForSeconds(trigger_delay);
            platforms[0].SetActive(false);
            platforms[1].SetActive(true);
        }

        private IEnumerator ReturnIE()
        {
            yield return new WaitForSeconds(return_delay);
            platforms[1].SetActive(false);
            platforms[0].SetActive(true);
            platforms[1].transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
