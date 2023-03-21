using BackEndRefactored;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BackendEndRefactored
{
    public class Simulate
    {
        public static int SimulateCounter = 0;
        public static void SimulateOneMove(string move)
        {
            if (move.Split('-')[0] == "T")
            {
                SimulateTroopModify(move);
                SimulateCounter++;
            }
            else if(move.Split('-')[0] == "X")
            {
                SimulateChangePlayerOfCountry(move);
                SimulateCounter++;
            }
            else if(move.Split('-')[0] == "I")
            {
                SimulateInitialize(move);
                SimulateCounter++;
            }
        }

        private static void SimulateTroopModify(string move)
        {
            Country country = Initialize.global[int.Parse(move.Split('-')[1])];
            int TroopsToAddOrSubstract = int.Parse(move.Split("-")[2]);
            if (move.Split('-')[3] == "p")
            {
                country.Troops += TroopsToAddOrSubstract;
            }
            else
            {
                country.Troops -= TroopsToAddOrSubstract;
            }
        }

        private static void SimulateChangePlayerOfCountry(string move)
        {
            Country winningCountry = Initialize.global[int.Parse(move.Split('-')[1])];
            Country losingCountry = Initialize.global[int.Parse(move.Split("-")[2])];
            losingCountry.Troops = winningCountry.Troops - 1;
            winningCountry.Troops = 1;
        }

        private static void SimulateInitialize(string move)
        {
            string[] splitted = move.Split("-");
            Player player = GetPlayerByChar(splitted[1]);

            for(int i = 2; i < splitted.Length; i++)
            {
                Country country = Initialize.global[int.Parse(splitted[i].Split(':')[0])];
                int troopsOfCountry = int.Parse(splitted[i].Split(':')[1]);
                player.OwnedCountries.Add(country);
                country.SetPlayer(player);
                country.Troops = troopsOfCountry;
            }
        }

        private static Player GetPlayerByChar(string ch)
        {
            Player player = null;
            switch(ch)
            {
                case "R":
                    player = Initialize.playerRed;
                    break;

                case "GY":
                    player = Initialize.playerYellow;
                    break;

                case "B":
                    player = Initialize.playerBlue;
                    break;

                case "GN":
                    player = Initialize.playerGreen;
                    break;

                default:
                    player = Initialize.playerPurple;
                    break;
            }

            return player;
        }
    }
}
