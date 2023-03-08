using System.Collections;
using System.Collections.Generic;
using BackEndRefactored;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public List<GameObject> GameObjects = new List<GameObject>();
    [SerializeField] private bool _canAttack = false;
    private GameObject GameStateButton;
    private UnityEngine.UI.Image playerTextBackgroundColor;
    private TextMeshProUGUI gameStateText, playerText, _troopsToDistriubteText;
    private Player playingPlayer = Initialize.players[0];

    // Start is called before the first frame update
    void Start()
    {
        
        GameStateButton = GameObject.Find("GameStateButton");
        gameStateText = GameStateButton.transform.GetChild(0).GameObject().GetComponent<TextMeshProUGUI>();
        playerTextBackgroundColor = GameObject.Find("PlayerText").GetComponent<Image>();
        playerText = GetTextMeshProComponentFromButton("PlayerText");
        _troopsToDistriubteText = GetTextMeshProComponentFromButton("TroopsAmount");

        
        SetPlayerDisplay(playingPlayer);
        SetTroopsText(playingPlayer);
        gameStateText.text = "End Place Troups";

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStateText.text == "End Place Troups" 
            && GameObjects.Count == 1
            )
        {
            if (GameObjects[0].GetComponent<CountryObject>().country.GetPlayer() == playingPlayer)
            {
                SubtractTroopsText(GameObjects[0].GetComponent<CountryObject>().country);
            }   
            ChangeTextColor(GameObjects[0], Color.black);
            GameObjects = new List<GameObject>();
        }
        
        else if (GameObjects.Count == 2)
        {

            if (GameObjects[0].GetComponent<CountryObject>().country.GetPlayer() == playingPlayer)
            {
                Debug.ClearDeveloperConsole();
                //Debug.Log($"{GameObjects[0].name} attacks {GameObjects[1].name}");
                Country attackingCountry = GameObjects[0].GetComponent<CountryObject>().country;
                Country defendingCountry = GameObjects[1].GetComponent<CountryObject>().country;
            
                if(gameStateText.text == "End Attack")
                    attackingCountry.Attack(GameObjects[1].GetComponent<CountryObject>().country);
            
                else if (gameStateText.text == "End Stabilization" &&
                         attackingCountry.HasSamePlayer(defendingCountry) &&
                         attackingCountry.IsNeighbor(defendingCountry))
                {
                    attackingCountry.TransferTroops(defendingCountry, 1);
                }
            }
            
            ChangeTextColor(GameObjects[0], Color.black);
            ChangeTextColor(GameObjects[1], Color.black);
            GameObjects = new List<GameObject>();
        }
        
    }
    
    private TextMeshProUGUI GetTextMeshProComponentFromButton(string buttonName) => 
        GameObject.Find(buttonName).transform.GetChild(0).GameObject().GetComponent<TextMeshProUGUI>();

    private void SetTroopsText(Player player)
    {
        int amount = player.GetBonus();
        Debug.Log($"Toops: {amount}");
        _troopsToDistriubteText.text = amount.ToString();
    }

    private void SubtractTroopsText(Country country)
    {
        int amount = int.Parse(_troopsToDistriubteText.text);
        if (amount == 1)
        {
            gameStateText.text = "End Attack";
            amount--;
            country.Troops++;
        }
        else if (amount > 1)
        {
            amount--;
            country.Troops++;
        }
        else
        {
            gameStateText.text = "End Attack";
        }

        _troopsToDistriubteText.text = amount.ToString();
    }
    
    private void SetPlayerDisplay(Player player)
    {
        playerText.text = player.playerName;
        playerTextBackgroundColor.color = player.playerColor;
    }
    
    
    public void ButtonHasBeenClicked(GameObject gameOb)
    {
        GameObjects.Add(gameOb);
        ChangeTextColor(gameOb, Color.white);
    }

    private void ChangeTextColor(GameObject gm, Color color)
    {
        TextMeshProUGUI gameStateText = gm.transform.GetChild(0).GameObject().GetComponent<TextMeshProUGUI>();
        gameStateText.color = color;
    }

    public void GameStateButtonHasBeenClicked()
    {
        
        switch (gameStateText.text)
        {
            case "End Attack":
                gameStateText.text = "End Stabilization";
                break;
            case "End Stabilization":
                gameStateText.text = "End Place Troups";
                
                GetNextPlayer(ref playingPlayer);
                SetPlayerDisplay(playingPlayer);
                SetTroopsText(Initialize.players[0]);
                break;
            
            case "End Place Troups":
                
                gameStateText.text = "End Attack";
                break;
            
            default:
                gameStateText.text = "End Attack";
                break;
            
        }
    }
    void GetNextPlayer(ref Player player)
    {
        int index = 0;
        for (int i = 0; i < Initialize.players.Length; i++)
        {
            if (Initialize.players[i] == player)
            {
                index = i;
                i = Initialize.players.Length;
            }
        }

        if (index == Initialize.players.Length - 1)
        {
            player = Initialize.players[0];
        }
        else
        {
            player = Initialize.players[index + 1]; 
        }
    }
    
}


