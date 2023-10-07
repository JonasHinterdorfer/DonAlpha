using BackEndRefactored;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BackendEndRefactored
{
    public class WinData : MonoBehaviour
    {
        public GameObject first;
        public GameObject second;
        public GameObject third;
        public GameObject fourth;
        public GameObject fifth;
        public GameObject can;
        private GameObject[] all;
        private void Start()
        {
            AddToArray();
            for (int i = 0; i < LoadPlayer.playerSum; i++)
            {
                all[i].SetActive(true);
            }
        }
        public void ShowGameObject()
        {
            can.gameObject.SetActive(true);
        }

        public void HideGameObject()
        {
            can.gameObject.SetActive(false);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void Stop()
        {
            Application.Quit();
        }

        private void AddToArray()
        {
            List<GameObject> list = new List<GameObject>()
        { first
        , second
        , third
        , fourth
        , fifth
        };

            all = list.ToArray();
        }
    }
}