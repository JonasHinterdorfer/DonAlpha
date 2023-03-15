using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BackEndRefactored
{
    public class Player
    {
        public int TroopsLeft { get; set; }
        private readonly Utils.PlayerNames Name;
        public List<Country> OwnedCountries { get; set; } = new();
        public Color playerColor;
        public string playerName;
        public int isHuman;
        public Player(Utils.PlayerNames name, Color playerColor)
        {
            Name = name;
            this.playerColor = playerColor;
        }

        public Utils.PlayerNames GetName() => Name;

        private int GetTroops()
        {
            int Troops = 0;
            foreach (Country country in OwnedCountries)
            {
                Troops += country.Troops;
            }

            return Troops;
        }
        public List<Utils.ContinentsName> GetFullContinents()
        {
            List<Utils.ContinentsName> fullContientes = new();

            if (HasContinent(Initialize.northAmerika))
                fullContientes.Add(Utils.ContinentsName.NorthAmerika);

            if (HasContinent(Initialize.southAmerika))
                fullContientes.Add(Utils.ContinentsName.Southamerika);

            if (HasContinent(Initialize.europe))
                fullContientes.Add(Utils.ContinentsName.Europe);

            if (HasContinent(Initialize.africa))
                fullContientes.Add(Utils.ContinentsName.Africa);

            if (HasContinent(Initialize.asia))
                fullContientes.Add(Utils.ContinentsName.Asia);

            if (HasContinent(Initialize.australia))
                fullContientes.Add(Utils.ContinentsName.Australia);

            return fullContientes;
        }

        public bool HasNoMoreCountries()
        {
            foreach(Country country in Initialize.global)
            {
                if(country.GetPlayer() == this)
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasFullMap()
            => OwnedCountries.Count == Initialize.global.Length;

        public int GetBonus()
        {
            int bonus = GetContinentBonus();

            bonus += OwnedCountries.Count / 3;

            return Math.Max(3,bonus);
        }

        private int GetContinentBonus()
        {
            int continentBonus = 0;
            if (HasContinent(Initialize.northAmerika) || HasContinent(Initialize.europe))
                continentBonus += 5;

            if (HasContinent(Initialize.southAmerika) || HasContinent(Initialize.australia))
                continentBonus += 2;

            if (HasContinent(Initialize.africa))
                continentBonus += 3;

            if (HasContinent(Initialize.asia))
                continentBonus += 7;

            return continentBonus;
        }

        private bool HasContinent(Country[] continent)
        {
            foreach (Country country in continent)
            {
                if (country.GetPlayer() != this)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
