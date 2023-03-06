using System.Collections;
using System.Collections.Generic;
using BackEndRefactored;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<GameObject> GameObjects = new List<GameObject>();
    [SerializeField] private bool _canAttack = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObjects.Count == 2)
        {
            Debug.ClearDeveloperConsole();
            //Debug.Log($"{GameObjects[0].name} attacks {GameObjects[1].name}");
            Country attackingCountry = GameObjects[0].GetComponent<CountryObject>().country;
            
           // if(attackingCountry.GetPlayer() == Gameloop.playerOnMove && Gameloop.gameState == Utils.GameStates.Attack)
             //   attackingCountry.Attack(GameObjects[1].GetComponent<CountryObject>().country);
            GameObjects = new List<GameObject>();
        }
    }

    public void ButtonHasBeenClicked(GameObject gameOb)
    {
        GameObjects.Add(gameOb);
    }
}
