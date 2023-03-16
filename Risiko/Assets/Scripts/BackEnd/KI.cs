
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BackEndRefactored
{
    public class KI : MonoBehaviour
    {

        private static List<int[]> PlaceTroops(Player color)
        {
            string information = string.Empty;

            Player botName = color;

            int troopCount = botName.GetBonus();
            
            for (int i = 0; i < 42; i++)
            {
                int informationInt = 0;
                Country country = Initialize.global[i];
                int troops = country.Troops;
                Player playerOfCountry = country.GetPlayer();
                Country[] neighbours = country.GetNeighborCountrys();
                if (botName == playerOfCountry && neighbours != null)
                {
                    foreach (Country neighbourCountry in neighbours)
                    {
                        Player neighbourPlayer = neighbourCountry.GetPlayer();
                        int neighbourTroops = neighbourCountry.Troops;
                        if (neighbourPlayer != botName)
                        {
                            informationInt += neighbourTroops;
                        }
                    }
                }
                informationInt -= troops;
                information += informationInt;
                information += ",";
            }

            information = information.Trim(',');

            List<int[]> placing = GetPlaceTroopsByInfo(information, troopCount, color);

            return placing;
        }

        private static List<int[]> GetPlaceTroopsByInfo(string information, int troopCount, Player color)
        {
            int max = 0;
            int maxCountry = 0;
            int secondMax = 0;
            int secondMaxCountry = 0;
            int thirdMax = 0;
            int thirdMaxCountry = 0;
            int count = 0;

            foreach (string info in information.Split(','))
            {
                int infoInt;

                if(info != "")
                {
                    infoInt = int.Parse(info);
                }
                else
                {
                    infoInt = -int.MaxValue;
                }

                if (infoInt > max)
                {
                    thirdMax = secondMax;
                    secondMax = max;
                    max = infoInt;
                    maxCountry = count;
                }
                else if (infoInt > secondMax)
                {
                    thirdMax = secondMax;
                    secondMax = infoInt;
                    secondMaxCountry = count;
                }
                else if (infoInt > thirdMax)
                {
                    thirdMax = infoInt;
                    thirdMaxCountry = count;
                }
                count++;
            }

            if (max <= 0)
            {
                max = 100;
                secondMax = 50;
                thirdMax = 25;
            }
            else if (secondMax <= 0)
            {
                max = 100;
                secondMax = 50;
                thirdMax = 25;
            }
            else if (thirdMax <= 0)
            {
                thirdMax = 0;
            }

            List<int[]> placing = GetPlacing(troopCount, maxCountry, secondMaxCountry, thirdMaxCountry, max, secondMax, thirdMax, color);

            return placing;
        }

        private static List<int[]> GetPlacing(int troopCount, int maxCountry, int secondMaxCountry, int thirdMaxCountry, int max, int secondMax, int thirdMax, Player color)
        {
            List<int[]> placing = new();
            int sum = max + secondMax + thirdMax;

            if (max == 100)
            {
                placing = DebugMove(color, troopCount);
            }
            else
            {
                placing.Add(new int[] { maxCountry, 0 });
                placing.Add(new int[] { secondMaxCountry, 0 });
                placing.Add(new int[] { thirdMaxCountry, 0 });

                for (int i = 0; i < troopCount; i++)
                {
                    System.Random rnd = new();
                    int random = rnd.Next(0, sum);
                    if (random < max)
                    {
                        placing.ElementAt(0)[1] += 1;
                    }
                    else if (random < secondMax + max)
                    {
                        placing.ElementAt(1)[1] += 1;
                    }
                    else
                    {
                        placing.ElementAt(2)[1] += 1;
                    }
                }
            }
            return placing;
        }

        private static List<int[]> DebugMove(Player color, int troopCount)
        {
            Player botPlayer = color;

            foreach(Country country in Initialize.global)
            {
                if(country.GetPlayer() != botPlayer)
                {
                    foreach(Country neighbour in country.GetNeighborCountrys())
                    {
                        if(neighbour.GetPlayer() == botPlayer)
                        {
                            List<int[]> placing = new();
                            int countryInt = Array.IndexOf(Initialize.global,neighbour);
                            int[] placeArray = { countryInt,troopCount };
                            placing.Add(placeArray);
                            return placing;
                        }
                    }
                }
            }
            List<int[]> debugPlace = new();
            int[] placeDebugArray = { 0,troopCount };
            debugPlace.Add(placeDebugArray);
            return debugPlace;
        }
        
        private static Country[] BotAttack(Player color)
        {
            Player botName = color;

            Country[] attackCountryAttackedCountry = GetArrayByLoop(botName);
            int maxChance = GetMaxChanceByLoop(botName);

            if (maxChance > 1)
            {
                return attackCountryAttackedCountry;
            }
            else
            {
                return new Country[] { Initialize.afganistan, Initialize.afganistan };
            }
        }

        public static Player GetPlayerByColorInt(int color)
        {
            Player botName = color switch
            {
                0 => Initialize.playerRed,
                1 => Initialize.playerYellow,
                2 => Initialize.playerGreen,
                3 => Initialize.playerPurple,
                _ => Initialize.playerBlue,
            };
            return botName;
        }

        private static Country[] GetArrayByLoop(Player botName)
        {
            int maxChance = 0;
            Country[] countries = new Country[2];
            for (int i = 0; i < 42; i++)
            {
                Country country = Initialize.global[i];
                Player playerNameOfCountry = country.GetPlayer();
                int troops = country.Troops;
                if (botName == playerNameOfCountry)
                {
                    Country[] neighbours = country.GetNeighborCountrys();
                    if (neighbours != null)
                    {
                        for (int i1 = 0; i1 < neighbours.Length; i1++)
                        {
                            Country neighbourCountry = neighbours[i1];
                            Player neighbourPlayer = neighbourCountry.GetPlayer();
                            int neighbourTroops = neighbourCountry.Troops;
                            if (neighbourPlayer != botName)
                            {
                                if (troops - neighbourTroops > maxChance)
                                {
                                    maxChance = troops - neighbourTroops;
                                    countries[0] = country;
                                    countries[1] = neighbourCountry;
                                }
                            }
                        }
                    }
                }
            }
            return countries;
        }

        private static int GetMaxChanceByLoop(Player botName)
        {
            int maxChance = 0;
            for (int i = 0; i < 42; i++)
            {
                Country country = Initialize.global[i];
                Player playerNameOfCountry = country.GetPlayer();
                int troops = country.Troops;
                if (botName == playerNameOfCountry)
                {
                    Country[] neighbours = country.GetNeighborCountrys();
                    if (neighbours != null)
                    {
                        for (int i1 = 0; i1 < neighbours.Length; i1++)
                        {
                            Country neighbourCountry = neighbours[i1];
                            Player neighbourPlayer = neighbourCountry.GetPlayer();
                            int neighbourTroops = neighbourCountry.Troops;
                            if (neighbourPlayer != botName)
                            {
                                if (troops - neighbourTroops > maxChance)
                                {
                                    maxChance = troops - neighbourTroops;
                                }
                            }
                        }
                    }
                }
            }
            return maxChance;
        }

        public static void BotExecute(Player color, bool version2)
        {
            List<int[]> placeDictionary = PlaceTroops(color);

            foreach(int[] countryIntArray in placeDictionary)
            {
                int countryInt = countryIntArray[0];
                Country country = Initialize.global[countryInt];
                country.Troops += countryIntArray[1];
            }

            Country[] attackCountryAttackedCountry = BotAttack(color);
            
            while(attackCountryAttackedCountry[0] != attackCountryAttackedCountry[1])
            {
                attackCountryAttackedCountry = BotAttack(color);

                if(attackCountryAttackedCountry[0] != attackCountryAttackedCountry[1])
                {
                    attackCountryAttackedCountry = KI.BotAttack(color);
                    attackCountryAttackedCountry[0].Attack(attackCountryAttackedCountry[1]);
                    if(version2)
                    {
                        AfterAttackTransferExecute(attackCountryAttackedCountry[0], attackCountryAttackedCountry[1]);
                    }
                }
            }

            if(version2)
            {
                int[] transferArray = BotTransfer(color);
                Initialize.global[transferArray[1]].TransferTroops(Initialize.global[transferArray[0]], transferArray[2]);
            }
        }

        public static int[] BotTransfer(Player botColor)
        {
            Player botPlayer = botColor;
            int[] transfer = new int[3];
            int maxLevel = 0;
            Country maxComeFromCountry = Initialize.global[0];
            Country maxDestinationCountry = Initialize.global[0];

            foreach (Country country in Initialize.global)
            {
                Player playerOfCountry = country.GetPlayer();
                Country maxNeighbour = Initialize.global[0];
                int troopsOfCountry = country.Troops;
                int chillLevel = troopsOfCountry;
                int desperateLevel = 0;
                if (playerOfCountry == botPlayer)
                {
                    foreach(Country neighbour in country.GetNeighborCountrys())
                    {
                        Player neighbourPlayer = neighbour.GetPlayer();
                        int troopsOfNeighbour = neighbour.Troops;
                        if (neighbourPlayer != botPlayer)
                        {
                            chillLevel -= troopsOfNeighbour;
                        }
                        else
                        {
                            int neighbourDesperateLevel = GetDesperateLevel(neighbour);
                            desperateLevel = Math.Min( neighbourDesperateLevel, desperateLevel);
                            if(neighbourDesperateLevel == desperateLevel)
                            {
                                maxNeighbour = neighbour;
                            }
                        }
                    }

                    maxLevel = Math.Max(maxLevel, -1 * (chillLevel + desperateLevel));

                    if (maxLevel == -1 * (chillLevel + desperateLevel))
                    {
                        maxComeFromCountry = country;
                        maxDestinationCountry = maxNeighbour;
                    }
                }
            }
            transfer[0] = GetCountryInt(maxComeFromCountry);
            transfer[1] = GetCountryInt(maxDestinationCountry);
            double floor = maxLevel / 2;
            transfer[2] = (int) Math.Floor(floor);

            return transfer;
        }

        private static int GetCountryInt(Country country)
        {
            int counter = 0;
            foreach(Country checkCountry in Initialize.global)
            {
                if(checkCountry == country)
                {
                    return counter;
                }
                counter++;
            }
            return counter;
        }

        private static int GetDesperateLevel(Country country)
        {
            int desperateLevel = country.Troops;

            foreach (Country neighbour in country.GetNeighborCountrys())
            {
                if(neighbour.GetPlayer() != country.GetPlayer())
                {
                    desperateLevel -= neighbour.Troops;
                }
            }

            return desperateLevel;
        }

        private static void AfterAttackTransferExecute(Country countryOne, Country countryTwo)
        {
            int[] afterAttackArray = AfterAttackTransfer(countryOne, countryTwo);
            int distanceOne = countryOne.Troops - afterAttackArray[0];
            int distanceTwo = countryTwo.Troops - afterAttackArray[1];

            Debug.Log(distanceOne);

            if(distanceOne > 0)
            {
                countryTwo.TransferTroops(countryOne, distanceOne);
            }
            else
            {
                countryOne.TransferTroops(countryTwo , distanceTwo);
            }
        }

        public static int[] AfterAttackTransfer(Country countryOne, Country countryTwo)
        {
            int[] afterAttackTransferArray = new int[2];
            if(countryOne.GetPlayer() == countryTwo.GetPlayer())
            {
                int countryOneTroops = countryOne.Troops;
                int countryTwoTroops = countryTwo.Troops;
                int countryOneDesperateLevel = -1 * GetDesperateLevel(countryOne);
                int countryTwoDesperateLevel = -1 * GetDesperateLevel(countryTwo);
                int sum = countryOneDesperateLevel + countryTwoDesperateLevel;
                int troopSum = countryOneTroops + countryTwoTroops;
                double countryOneTroopsDouble = troopSum * (countryOneDesperateLevel / sum);
                afterAttackTransferArray[0] = (int) Math.Floor(countryOneTroopsDouble);
                int countryTwoTroopsInt = troopSum - afterAttackTransferArray[0];
                afterAttackTransferArray[1] = countryTwoTroopsInt;
            }
            return afterAttackTransferArray;
        }
    }
}