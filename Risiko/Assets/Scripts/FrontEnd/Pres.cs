using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BackEndRefactored
{
    public class Pres : MonoBehaviour
    {
        public void OneHuman()
        {
            Initialize.players[0].isHuman = 0;
            Initialize.players[0].playerName = LoadPlayer.allNames[0];
            SetPlayer(1, 1);
        }

        public void OnlyHuman()
        {
            SetPlayer(0, 0);
        }
        private void SetPlayer(int humanMode, int startIndex)
        {
            for (int i = startIndex; i < Initialize.players.Length; i++)
            {
                Initialize.players[i].isHuman = humanMode;
                Initialize.players[i].playerName = LoadPlayer.allNames[i];
            }
        }
        public void SwitchScene()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}