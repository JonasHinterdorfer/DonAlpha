using System.Collections;
using System.Collections.Generic;
using BackEndRefactored;
using UnityEngine;

public class testgamelooop : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var player in Initialize.players)
        {
            KI.BotExecute(player, false);
        }
    }
}
