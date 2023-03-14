using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace BackEndRefactored
{
    public class Utils
    {
        public static Random random = new();    
        public enum CountryName
        {
            Alaska, NorthwestTerritory, Greenland, Alberta, Ontario, Quebec, WesternUS, EasternUS, CentralAmeriaca,
            Venezuela, Brazil, Peru, Argentinia,
            Iceland, Scandinavia, Ukraine, GreatBritain, NorthernEurope, WesternEurope, SouthernEurope,
            NorthAfrica, Egypt, EastAfrica, Congo, SouthAfrifca, Madagascar,
            Ural, Afganistan, MiddleEast, Sibiria, China, India, Yakusk, Irkusk, Mongolia, Siam, Japan, Kamschka,
            Indonesia, NewGuinea, WesternAustralia, EasternAustralia
        }

        public enum ContinentsName
        {
            NorthAmerika, Southamerika, Africa, Europe, Asia, Australia
        }
        public enum PlayerNames
        {
            Red, Blue, Purple, Green, Yellow, Brown
        }

        public Dictionary<string, Country> GetCountry = new Dictionary<string, Country>()
        {
            // Africa
            { "NorthAfrica", Initialize.northAfrika },
            { "Congo", Initialize.congo },
            { "EastAfrica", Initialize.eastAfrica },
            { "Egypt", Initialize.egypt },
            { "SouthAfrica", Initialize.southAfrica },
            { "Madagascar", Initialize.madagascar },

            //Europe
            { "Scandinavia", Initialize.scandinavia },
            { "GreatBriatian", Initialize.greatBritian },
            { "SouthernEurope", Initialize.southernEurope },
            { "NothernEurope", Initialize.northernEurope },
            { "WesternEurope", Initialize.westernEurope },
            { "Iceland", Initialize.iceland },
            { "Ukraine", Initialize.ukraine },

            //Australia
            { "EasternAustralia", Initialize.easternAustralia },
            { "WesternAustralia", Initialize.westernAustralia },
            { "Indonesia", Initialize.indonesia },
            { "NewGuinea", Initialize.newGuinea },

            //Asia
            { "Ural", Initialize.ural },
            { "Afghanistan", Initialize.afganistan },
            { "China", Initialize.china },
            { "India", Initialize.india },
            { "Irusk", Initialize.irusk },
            { "Japan", Initialize.japan },
            { "Kamschka", Initialize.kamschka },
            { "MiddleEast", Initialize.middleEast },
            { "Monglolia", Initialize.mongolia },
            { "Siam", Initialize.siam },
            { "Siberia", Initialize.sibiria },
            { "Yakusk", Initialize.yakusk },

            //South Amerika
            { "Peru", Initialize.peru },
            { "Argentinia", Initialize.argentinia },
            { "Brasil", Initialize.brazil },
            { "Venezuela", Initialize.venzuela },

            //North America
            { "Alaska", Initialize.alaska },
            { "NorthWestTerritory", Initialize.northWestTerritory },
            { "Greenland", Initialize.greenland },
            { "Alberta", Initialize.alberta },
            { "Ontario", Initialize.ontario },
            { "Qubec", Initialize.quebec },
            { "WesternUS", Initialize.westernUS },
            { "CentralAmerika", Initialize.centralAmerika },
            { "EasternUs", Initialize.easternUS },

        };

        public Dictionary<string, Player> GetPlayer = new Dictionary<string, Player>()
        {
            { "PlayerRed", Initialize.playerRed },
            { "PlayerBlue", Initialize.playerBlue },
            { "PlayerYellow", Initialize.playerYellow },
            { "PlayerGreen", Initialize.playerGreen },
            { "PlayerPink", Initialize.playerPurple }
        };

        public static string[] gameStates = 
            new[] { "Place All Troops!", "End Attack", "End Stabilization" };

    }
}
