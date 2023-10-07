using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BackendEndRefactored;
using BackEndRefactored;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FrontEnd
{
    public class GameLoop: MonoBehaviour
    {
        private List<GameObject> _clickedObjects = new ();
        private GameObject _gameStateButton, _playerTextDisplay, _troopsToSpreadDisplay;
        private Player _playingPlayer;
        private TextMeshProUGUI _gameStateText, _playerText, _troopsToSpreadText;
        private Image _playerTextBackground;
        private int _gameStateIndex;

        private List<Player> _allPlayers = new List<Player>();
        private void Start()
        {
            foreach (var player in Initialize.players)
            {
                _allPlayers.Add(player);
            }
            
            _gameStateButton = GameObject.Find("GameStateButton");
            _gameStateText = GetTextComponent(_gameStateButton);

            _playerTextDisplay = GameObject.Find("PlayerText");
            _playerText = GetTextComponent(_playerTextDisplay);
            _playerTextBackground = GetImageComponent(_playerTextDisplay);
       
            _troopsToSpreadDisplay = GameObject.Find("TroopsAmount");
            _troopsToSpreadText = GetTextComponent(_troopsToSpreadDisplay);

            _playingPlayer = _allPlayers[0];
            _playerTextBackground.color = _playingPlayer.playerColor;
            RefreshButtonText(_playerText, _playingPlayer.playerName);
            RefreshButtonText(_troopsToSpreadText, _playingPlayer.GetBonus().ToString());
            RefreshButtonText(_gameStateText, Utils.gameStates[_gameStateIndex]);
            _gameStateButton.SetActive(false);
        }

        private void Update()
        {
            CheckWin();
            if (_playingPlayer.isHuman > 0)
            {
                KI.BotExecute(_playingPlayer, _playingPlayer.isHuman);
                _gameStateIndex = 0;
                RefreshButtonText(_gameStateText, Utils.gameStates[_gameStateIndex]);
                ChangeToNextPlayerMove();
            }
            if (Utils.gameStates[_gameStateIndex] == "Place All Troops!" && _clickedObjects.Count == 1)
            {
                Country country = _clickedObjects[0].GetComponent<CountryObject>().country;
                if (country.GetPlayer() == _playingPlayer)
                {
                    SubtractTroopsToPlaceText(country);
                }
                
                TextMeshProUGUI textObject = GetTextComponent(_clickedObjects[0]);
                ChangeTextColor(textObject, Color.black);
                
                _clickedObjects = new List<GameObject>();

                

            }
            else if (_clickedObjects.Count == 2)
            {
                Country attackingCountry = _clickedObjects[0].GetComponent<CountryObject>().country;
                Country defendingCountry = _clickedObjects[1].GetComponent<CountryObject>().country;

                if (attackingCountry.GetPlayer() == _playingPlayer)
                {
                    if (Utils.gameStates[_gameStateIndex] == "End Attack")
                    {
                        attackingCountry.Attack(defendingCountry);
                    }
                    else if (_gameStateText.text == "End Stabilization"
                             && attackingCountry.HasSamePlayer(defendingCountry)
                             && attackingCountry.IsNeighbor(defendingCountry))
                    {
                        attackingCountry.TransferTroops(defendingCountry, 1);
                    }

                    CheckWin();
                    
                }
                TextMeshProUGUI textObject = GetTextComponent(_clickedObjects[0]);
                ChangeTextColor(textObject, Color.black);
                    
                textObject = GetTextComponent(_clickedObjects[1]);
                ChangeTextColor(textObject, Color.black);

                _clickedObjects = new List<GameObject>();
            }

            
        }

        private void CheckPlayerLost()
        {
            
            foreach (Player player in _allPlayers)
            {
                if (player.OwnedCountries.Count == 0)
                {
                    _allPlayers.Remove(player);
                    Utils.playerGoneList.Add(player);
                }
                    
            }


        }


        private void SubtractTroopsToPlaceText(Country country)
        {
            int amount = Convert.ToInt32(_troopsToSpreadText.text);
            if (amount == 1)
            {
                amount--;
                country.Troops++;
                _gameStateButton.SetActive(true);
                OnGameStateButtonClick();
            }
            else
            {
                amount--;
                country.Troops++;
            }

            _troopsToSpreadText.text = amount.ToString();
        }

        private void CheckWin()
        {
            foreach (var player in _allPlayers)
            {
                if (player.HasFullMap())
                {
                    string logString = string.Empty;
                    foreach (string line in Utils.gameLog)
                    {
                        logString += line;
                        logString += "|";
                    }

                    StreamWriter writer = new("Logs\\gameLog.txt", true);
                    writer.Write(logString);
                    writer.Close();
                    foreach (string line in Utils.gameLog)
                    {
                        Simulate.SimulateOneMove(line);   
                    }
                    Debug.Log(Simulate.SimulateCounter);
                    SwitchScene();
                }
            }
        }
        
        private void SwitchScene()
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
        
        private TextMeshProUGUI GetTextComponent(GameObject gm) 
            => gm.transform.GetChild(0).GameObject().GetComponent<TextMeshProUGUI>();

        private Image GetImageComponent(GameObject gm)
            => gm.GetComponent<Image>();

        private void ChangeTextColor(TextMeshProUGUI textObject, Color color)
            => textObject.color = color;
    
        public void OnCountryButtonClick(GameObject gm)
        {
            _clickedObjects.Add(gm);
            TextMeshProUGUI textObject = GetTextComponent(gm);
            ChangeTextColor(textObject, Color.white);
        }

        public void OnGameStateButtonClick()
        {
            GetNextIndex(ref _gameStateIndex, Utils.gameStates.Length);
            RefreshButtonText(_gameStateText, Utils.gameStates[_gameStateIndex]);

            if (Utils.gameStates[_gameStateIndex] == "Place All Troops!")
            {
                ChangeToNextPlayerMove();
                _gameStateButton.SetActive(false);
            }

            
        }

        private void GetNextIndex(ref int index, int lenght)
        {
            index++;
            
            if (index + 1  > lenght )
                index = 0;
        }

        private void RefreshButtonText(TextMeshProUGUI textObject, string text)
            => textObject.text = text;

        private void GetNextPlayer(ref Player player)
        {
            int index = _allPlayers.IndexOf(player);
            GetNextIndex(ref index, _allPlayers.Count);
            player = _allPlayers[index];
        }

        private void ChangeToNextPlayerMove()
        {
            GetNextPlayer(ref _playingPlayer);
            _playerTextBackground.color = _playingPlayer.playerColor;
            RefreshButtonText(_playerText, _playingPlayer.playerName);
            RefreshButtonText(_troopsToSpreadText, _playingPlayer.GetBonus().ToString());
            CheckPlayerLost();
            CheckWin();
        }

        
    }
}
