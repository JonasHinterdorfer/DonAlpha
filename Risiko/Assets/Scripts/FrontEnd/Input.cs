using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection.Emit;
using TMPro;
using System.Linq;

namespace BackEndRefactored
{
    public class Input : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI text;
        public int colorNumber;

        private void Update()
        {
            if (LoadPlayer.allPlayersArrayInStruct[colorNumber].state)
            {
                button.interactable = TextIsValid(LoadPlayer.allPlayersArrayInStruct[colorNumber].playerName);
            }
        }
        public void Loadstate(bool states)
        {
            LoadPlayer.allPlayersArrayInStruct[colorNumber].state = states;
            if(states)
            {
                LoadPlayer.playerSum++;
            }
            else
            {
                if (LoadPlayer.playerSum > 0)
                {
                    LoadPlayer.playerSum--;
                }
            }
            Debug.Log(LoadPlayer.allPlayersArrayInStruct[colorNumber].state);
        }
        public void LoadName(string name)
        {
            LoadPlayer.allPlayersArrayInStruct[colorNumber].playerName = name;
            LoadPlayer.allNames[colorNumber] = name.ToLower();
            Debug.Log(name);
        }
        public void LoadMode(int number)
        {
            LoadPlayer.allPlayersArrayInStruct[colorNumber].isHuman = number;
            Debug.Log($"{LoadPlayer.allPlayersArrayInStruct[colorNumber].isHuman} , {number}");
        }
        private bool TextIsValid(string name)
        {
            bool isValid = true;
            if (LoadPlayer.allPlayersArrayInStruct[colorNumber].state)
            {
                if (string.IsNullOrEmpty(name))
                {
                    text.text = "No Name inside";
                    text.color = Color.red;
                    isValid = false;
                }
                /*
                else if(LoadPlayer.allNames.Contains(name))
                {
                    text.text = "Name exist";
                    text.color = Color.red;
                    isValid = false;
                }
                */
                else
                {
                    isValid = true;
                    text.text = "Enter Name";
                    text.color = Color.gray;
                }
            }
            else
            {
                isValid = true;
                text.text = "Enter Name";
                text.color = Color.gray;
            }
            return isValid;
        }
    }
}