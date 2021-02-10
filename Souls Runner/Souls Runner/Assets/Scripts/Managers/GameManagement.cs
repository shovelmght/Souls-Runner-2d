using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Public;
using UnityEngine.SceneManagement;
namespace LesserKnown.Manager
{
    public class GameManagement : MonoBehaviour
    {
        public GameObject health_obj;
        public Transform health_holder;

        private List<GameObject> health_list = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i < PublicVariables.health; i++)
            {
                var _copy = Instantiate(health_obj);
                _copy.transform.SetParent(health_holder, true);
                health_list.Add(_copy);
            }
        }

        public void Die()
        {            
            StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            int _index = health_list.FindLastIndex(x => { return x != null; });

            health_list[_index].GetComponent<Animator>().SetTrigger("Destroy");

            yield return new WaitForSeconds(.2f);
            Destroy(health_list[_index]);
            health_list.RemoveAt(_index);

            PublicVariables.Lose_Health(1);

            if (PublicVariables.health == 0)
                SceneManager.LoadScene(0);
        }

        private void OnLevelWasLoaded(int level)
        {
            PublicVariables.Reset_Level(10);
        }
    }
}
