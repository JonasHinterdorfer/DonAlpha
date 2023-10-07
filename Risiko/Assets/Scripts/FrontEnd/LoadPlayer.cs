using BackendEndRefactored;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace BackEndRefactored
{
    public class LoadPlayer : MonoBehaviour
    {
        public Button button;
        public static int playerSum = 0;
        public static SaveValues red;
        public static SaveValues green;
        public static SaveValues blue;
        public static SaveValues grey;
        public static SaveValues purpel;
        public static SaveValues[] allPlayersArrayInStruct = {red, grey, green, purpel, blue};
        public List<Player> allPlayers = new();
        public static string[] allNames = { "red", "grey", "green", "purpel", "blue" };
        private void Update()
        {
            if (playerSum >= 3)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        public void StartGame()
        {
            AddPlayer();
            Initialize.players = allPlayers.ToArray();
            Debug.Log(allPlayers.Count);
            SwitchScene();
        }
        private void AddPlayer()
        {
            int index = 0;

            foreach(SaveValues player in allPlayersArrayInStruct)
            {
                if(player.state)
                {
                    Debug.Log(player.state);
                    Initialize.players[index].playerName = player.playerName;
                    Initialize.players[index].isHuman = player.isHuman;
                    allPlayers.Add(Initialize.players[index]);
                }
                index++;
            }
        }

        private void SwitchScene()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
            foreach (Player player in Initialize.players)
            {
                Debug.Log(player.playerName);
            }
        }
        public struct SaveValues
        {
            public string playerName;
            public bool state;
            public int isHuman;
            public int colorNumber;

            public SaveValues(string playerName, int number)
            {
                this.playerName = playerName;
                state = false;
                isHuman = 1;
                colorNumber = number;
            }
        }
    }
}