

using System;
using UnityEngine;
using System.Collections.Generic;

namespace BackEndRefactored
{
    internal class Initialize : MonoBehaviour
    {
        public static Country alaska = new(Utils.CountryName.Alaska, Utils.ContinentsName.NorthAmerika);

        public static Country northWestTerritory =
            new(Utils.CountryName.NorthwestTerritory, Utils.ContinentsName.NorthAmerika);

        public static Country alberta = new(Utils.CountryName.Alberta, Utils.ContinentsName.NorthAmerika);
        public static Country ontario = new(Utils.CountryName.Ontario, Utils.ContinentsName.NorthAmerika);
        public static Country westernUS = new(Utils.CountryName.WesternUS, Utils.ContinentsName.NorthAmerika);

        public static Country centralAmerika =
            new(Utils.CountryName.CentralAmeriaca, Utils.ContinentsName.NorthAmerika);

        public static Country easternUS = new(Utils.CountryName.EasternUS, Utils.ContinentsName.NorthAmerika);
        public static Country quebec = new(Utils.CountryName.Quebec, Utils.ContinentsName.NorthAmerika);
        public static Country greenland = new(Utils.CountryName.Greenland, Utils.ContinentsName.NorthAmerika);

        public static Country venzuela = new(Utils.CountryName.Venezuela, Utils.ContinentsName.Southamerika);
        public static Country brazil = new(Utils.CountryName.Brazil, Utils.ContinentsName.Southamerika);
        public static Country peru = new(Utils.CountryName.Peru, Utils.ContinentsName.Southamerika);
        public static Country argentinia = new(Utils.CountryName.Argentinia, Utils.ContinentsName.Southamerika);

        public static Country iceland = new(Utils.CountryName.Iceland, Utils.ContinentsName.Europe);
        public static Country greatBritian = new(Utils.CountryName.GreatBritain, Utils.ContinentsName.Europe);
        public static Country scandinavia = new(Utils.CountryName.Scandinavia, Utils.ContinentsName.Europe);
        public static Country northernEurope = new(Utils.CountryName.NorthernEurope, Utils.ContinentsName.Europe);
        public static Country ukraine = new(Utils.CountryName.Ukraine, Utils.ContinentsName.Europe);
        public static Country westernEurope = new(Utils.CountryName.WesternEurope, Utils.ContinentsName.Europe);
        public static Country southernEurope = new(Utils.CountryName.SouthernEurope, Utils.ContinentsName.Europe);

        public static Country northAfrika = new(Utils.CountryName.NorthAfrica, Utils.ContinentsName.Africa);
        public static Country egypt = new(Utils.CountryName.Egypt, Utils.ContinentsName.Africa);
        public static Country eastAfrica = new(Utils.CountryName.EastAfrica, Utils.ContinentsName.Africa);
        public static Country congo = new(Utils.CountryName.Congo, Utils.ContinentsName.Africa);
        public static Country southAfrica = new(Utils.CountryName.SouthAfrifca, Utils.ContinentsName.Africa);
        public static Country madagascar = new(Utils.CountryName.Madagascar, Utils.ContinentsName.Africa);

        public static Country middleEast = new(Utils.CountryName.MiddleEast, Utils.ContinentsName.Asia);
        public static Country afganistan = new(Utils.CountryName.Afganistan, Utils.ContinentsName.Asia);
        public static Country ural = new(Utils.CountryName.Ural, Utils.ContinentsName.Asia);
        public static Country sibiria = new(Utils.CountryName.Sibiria, Utils.ContinentsName.Asia);
        public static Country yakusk = new(Utils.CountryName.Yakusk, Utils.ContinentsName.Asia);
        public static Country kamschka = new(Utils.CountryName.Kamschka, Utils.ContinentsName.Asia);
        public static Country irusk = new(Utils.CountryName.Irkusk, Utils.ContinentsName.Asia);
        public static Country japan = new(Utils.CountryName.Japan, Utils.ContinentsName.Asia);
        public static Country mongolia = new(Utils.CountryName.Mongolia, Utils.ContinentsName.Asia);
        public static Country china = new(Utils.CountryName.China, Utils.ContinentsName.Asia);
        public static Country siam = new(Utils.CountryName.Siam, Utils.ContinentsName.Asia);
        public static Country india = new(Utils.CountryName.India, Utils.ContinentsName.Asia);

        public static Country newGuinea = new(Utils.CountryName.NewGuinea, Utils.ContinentsName.Australia);
        public static Country indonesia = new(Utils.CountryName.Indonesia, Utils.ContinentsName.Australia);

        public static Country westernAustralia =
            new(Utils.CountryName.WesternAustralia, Utils.ContinentsName.Australia);

        public static Country easternAustralia =
            new(Utils.CountryName.EasternAustralia, Utils.ContinentsName.Australia);


        public static Country[] northAmerika =
            { alaska, northWestTerritory, alberta, ontario, westernUS, centralAmerika, easternUS, quebec, greenland };

        public static Country[] southAmerika = { venzuela, brazil, peru, argentinia };

        public static Country[] europe =
            { iceland, greatBritian, scandinavia, northernEurope, ukraine, westernEurope, southernEurope };

        public static Country[] africa = { northAfrika, egypt, congo, eastAfrica, southAfrica, madagascar };

        public static Country[] asia =
            { middleEast, afganistan, ural, sibiria, yakusk, kamschka, irusk, japan, mongolia, china, siam, india };

        public static Country[] australia = { newGuinea, indonesia, westernAustralia, easternAustralia };

        public static Country[] global =
        {
            alaska, northWestTerritory, alberta, ontario, westernUS, centralAmerika, easternUS, quebec, greenland,
            venzuela, brazil, peru, argentinia,
            iceland, greatBritian, scandinavia, northernEurope, ukraine, westernEurope, southernEurope,
            northAfrika, egypt, congo, eastAfrica, southAfrica, madagascar,
            middleEast, afganistan, ural, sibiria, yakusk, kamschka, irusk, japan, mongolia, china, siam, india,
            newGuinea, indonesia, westernAustralia, easternAustralia
        };

        public static Player playerRed = new(Utils.PlayerNames.Red, Color.red);
        public static Player playerYellow = new(Utils.PlayerNames.Yellow, Color.gray);
        public static Player playerGreen = new(Utils.PlayerNames.Green, Color.green);
        public static Player playerPurple = new(Utils.PlayerNames.Purple, Color.magenta);
        public static Player playerBlue = new(Utils.PlayerNames.Blue, Color.blue);

        public static Player[] players = {playerRed, playerYellow, playerGreen, playerPurple, playerBlue };
       
        /// <summary>
        /// The constructor here all Coordiantes are register for every country there is a own methode
        /// </summary>
        public void Start()
        {
            RegisterAfricaNeighborCountrys();
            RegisterAsiaNeighborCountrys();
            RegisterAustraliaNeighborCountrys();
            RegisterEuropeNeighborCountrys();
            RegisterNorthAmerikaNeighborCountrys();
            RegisterSouthAmerikaNeighborCountrys();

            DistributeCountriesAndTroops(players);
            foreach (var player in players)
            {
                Debug.Log($"{player.GetName()}:\n\tTroops:{player.GetTroops()}");
                Debug.Log($"Countries: {player.OwnedCountries.Count} ");
            }
        }

        private static void RegisterEuropeNeighborCountrys()
        {
            iceland.SetNeighborCountrys(new Country[] { greenland, greatBritian, scandinavia });
            greatBritian.SetNeighborCountrys(new Country[] { iceland, scandinavia, northernEurope, westernEurope });
            scandinavia.SetNeighborCountrys(new Country[] { ukraine, northernEurope, greatBritian, iceland});
            northernEurope.SetNeighborCountrys(new Country[] { greatBritian, scandinavia, ukraine, southernEurope, westernEurope });
            ukraine.SetNeighborCountrys(new Country[] { scandinavia, ural, afganistan, middleEast, southernEurope, northernEurope });
            westernEurope.SetNeighborCountrys(new Country[] { greatBritian, northernEurope, southernEurope, northAfrika });
            southernEurope.SetNeighborCountrys(new Country[] { northAfrika, westernEurope, northernEurope, ukraine, middleEast, egypt });
        }

        private static void RegisterAsiaNeighborCountrys()
        {
            middleEast.SetNeighborCountrys(new Country[] { ukraine, afganistan, india, egypt, eastAfrica, southAfrica, southernEurope });
            afganistan.SetNeighborCountrys(new Country[] { ukraine, ural, china, india, middleEast });
            ural.SetNeighborCountrys(new Country[] { ukraine, sibiria, china, afganistan });
            sibiria.SetNeighborCountrys(new Country[] { ural, yakusk, irusk, mongolia, china });
            yakusk.SetNeighborCountrys(new Country[] { sibiria, irusk, kamschka });
            kamschka.SetNeighborCountrys(new Country[] { alaska, japan, mongolia, irusk, yakusk });
            irusk.SetNeighborCountrys(new Country[] { sibiria, yakusk, kamschka, mongolia });
            japan.SetNeighborCountrys(new Country[] { kamschka, mongolia });
            mongolia.SetNeighborCountrys(new Country[] { kamschka, irusk, sibiria, china, japan });
            china.SetNeighborCountrys(new Country[] { sibiria, mongolia, siam, india, afganistan, ural });
            siam.SetNeighborCountrys(new Country[] { indonesia, india, china });
            india.SetNeighborCountrys(new Country[] { afganistan, china, siam, middleEast });
        }

        private static void RegisterNorthAmerikaNeighborCountrys()
        {
            alaska.SetNeighborCountrys(new Country[] { kamschka, northWestTerritory, alberta });
            northWestTerritory.SetNeighborCountrys(new Country[] { alaska, alberta, greenland, ontario });
            alberta.SetNeighborCountrys(new Country[] { alaska, northWestTerritory, ontario, westernUS });
            ontario.SetNeighborCountrys(new Country[] { northWestTerritory, greenland, quebec, easternUS, westernUS, alberta });
            westernUS.SetNeighborCountrys(new Country[] { alberta, ontario, easternUS, centralAmerika });
            centralAmerika.SetNeighborCountrys(new Country[] { westernUS, easternUS, venzuela });
            easternUS.SetNeighborCountrys(new Country[] { centralAmerika, westernUS, ontario, quebec });
            quebec.SetNeighborCountrys(new Country[] { ontario, greenland, easternUS });
            greenland.SetNeighborCountrys(new Country[] { northWestTerritory, ontario, quebec, iceland });
        }
        private static void RegisterSouthAmerikaNeighborCountrys()
        {
            venzuela.SetNeighborCountrys(new Country[] { centralAmerika, peru, brazil });
            brazil.SetNeighborCountrys(new Country[] { venzuela, peru, argentinia, northAfrika });
            peru.SetNeighborCountrys(new Country[] { venzuela, brazil, argentinia });
            argentinia.SetNeighborCountrys(new Country[] { brazil, peru });
        }
        private static void RegisterAfricaNeighborCountrys()
        {
            northAfrika.SetNeighborCountrys(new Country[] { brazil, westernEurope, southernEurope, egypt, eastAfrica, congo });
            egypt.SetNeighborCountrys(new Country[] { southernEurope, middleEast, eastAfrica, northAfrika });
            eastAfrica.SetNeighborCountrys(new Country[] { egypt, middleEast, madagascar, southAfrica, congo, northAfrika });
            congo.SetNeighborCountrys(new Country[] { northAfrika, eastAfrica, southAfrica });
            southAfrica.SetNeighborCountrys(new Country[] { congo, eastAfrica, madagascar });
            madagascar.SetNeighborCountrys(new Country[] { southAfrica, eastAfrica });

        }
        private static void RegisterAustraliaNeighborCountrys()
        {
            newGuinea.SetNeighborCountrys(new Country[] { indonesia, easternAustralia, westernAustralia });
            indonesia.SetNeighborCountrys(new Country[] { siam, newGuinea, westernAustralia });
            westernAustralia.SetNeighborCountrys(new Country[] { easternAustralia, indonesia });
            easternAustralia.SetNeighborCountrys(new Country[] { newGuinea, westernAustralia });
        }

        private static void DistributeCountriesAndTroops(Player[] players)
        {
            DistributePlayerToCountries(players);
            DistributeTroopsToCountries(players);

        }

        private static void DistributePlayerToCountries(Player[] players)
        {
            Debug.Log("Initialize: " + players.Length);
            List<Country> countriesLeft = new();

            foreach (Country country in Initialize.global)
            {
                countriesLeft.Add(country);
            }

            // Here are the Players Registert to the Countries

            while (countriesLeft.Count != 0)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    Debug.Log(players[i].playerName);
                    if (countriesLeft.Count > 0)
                    {
                        int countryToRemove = Utils.random.Next(0, countriesLeft.Count);
                        players[i].OwnedCountries.Add(countriesLeft[countryToRemove]);
                        countriesLeft[countryToRemove].SetPlayer(players[i]);
                        countriesLeft.Remove(countriesLeft[countryToRemove]);
                    }
                }
            }
        }
 
        private static void DistributeTroopsToCountries(Player[] players)
        {
            foreach (Player player in players)
            {
                player.TroopsLeft = GetTroops(players.Length);
                //to Beginn every Country gets one Troup
                foreach (Country country in player.OwnedCountries)
                {
                    country.Troops = 1;
                    player.TroopsLeft--;
                }

                // and the rest is randomly placed
                while (player.TroopsLeft > 0)
                {
                    int countryIndex = Utils.random.Next(0, player.OwnedCountries.Count);
                    player.OwnedCountries[countryIndex].Troops++;
                    player.TroopsLeft--;
                }
            }
        }
        
        private static int GetTroops(int amountOfPlayers)
        {
            switch (amountOfPlayers)
            {
                case 3:
                    return 35;
                case 4:
                    return 30;
                case 5:
                    return 25;
                default:
                    break;
            }
            return 0;
        }
    }
}
