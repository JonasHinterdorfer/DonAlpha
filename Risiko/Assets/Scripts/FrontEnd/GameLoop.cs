using System;
using System.Collections.Generic;
using BackEndRefactored;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FrontEnd
{
    public class GameLoop: MonoBehaviour
    {
        private List<GameObject> _clickedObjects = new ();
        private GameObject _gameStateButton, _playerTextDisplay, _troopsToSpreadDisplay;
        private Player _playingPlayer = Initialize.players[0];
        private TextMeshProUGUI _gameStateText, _playerText, _troopsToSpreadText;
        private Image _playerTextBackground;
        private int _gameStateIndex;
        private void Start()
        {
            _gameStateButton = GameObject.Find("GameStateButton");
            _gameStateText = GetTextComponent(_gameStateButton);

            _playerTextDisplay = GameObject.Find("PlayerText");
            _playerText = GetTextComponent(_playerTextDisplay);
            _playerTextBackground = GetImageComponent(_playerTextDisplay);
       
            _troopsToSpreadDisplay = GameObject.Find("TroopsAmount");
            _troopsToSpreadText = GetTextComponent(_troopsToSpreadDisplay);
            
            
            _playerTextBackground.color = _playingPlayer.playerColor;
            RefreshButtonText(_playerText, _playingPlayer.playerName);
            RefreshButtonText(_troopsToSpreadText, _playingPlayer.GetBonus().ToString());
            RefreshButtonText(_gameStateText, Utils.gameStates[_gameStateIndex]);
            _gameStateButton.SetActive(false);
        }

        private void Update()
        {
            
            if (_playingPlayer.isHuman > 0)
            {
                KI.BotExecute(_playingPlayer, false);
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
                    
                }
                TextMeshProUGUI textObject = GetTextComponent(_clickedObjects[0]);
                ChangeTextColor(textObject, Color.black);
                    
                textObject = GetTextComponent(_clickedObjects[1]);
                ChangeTextColor(textObject, Color.black);

                _clickedObjects = new List<GameObject>();
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
            int index = Array.IndexOf(Initialize.players, player);
            GetNextIndex(ref index, Initialize.players.Length);
            player = Initialize.players[index];
        }

        private void ChangeToNextPlayerMove()
        {
            GetNextPlayer(ref _playingPlayer);
            _playerTextBackground.color = _playingPlayer.playerColor;
            RefreshButtonText(_playerText, _playingPlayer.playerName);
            RefreshButtonText(_troopsToSpreadText, _playingPlayer.GetBonus().ToString());
        }
    }
}
