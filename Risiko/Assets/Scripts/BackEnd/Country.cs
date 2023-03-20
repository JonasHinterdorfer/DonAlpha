using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BackEndRefactored
{
    public class Country
    {
        private readonly Utils.CountryName Name;
        public int Troops;
        private Country[] NeighborCountrys;
        private readonly Utils.ContinentsName Contient;
        private Player PlayerOfCountry;

        public Country(Utils.CountryName name, Utils.ContinentsName contient)
        {
            Name = name;
            Contient = contient;
        }

        public Utils.CountryName GetName() => Name;
        public Utils.ContinentsName GetContient() => Contient;
        public void SetNeighborCountrys(Country[] countries) => NeighborCountrys = countries;
        public Country[] GetNeighborCountrys() => NeighborCountrys;
        public Player GetPlayer() => PlayerOfCountry;
        public Player SetPlayer(Player playerToSet) => PlayerOfCountry = playerToSet;

        public void ChangePlayer(Player playerToChange)
        {
            if(PlayerOfCountry != null)
            {
                PlayerOfCountry.OwnedCountries.Remove(this);
                playerToChange.OwnedCountries.Add(this);
                SetPlayer(playerToChange);
            }
        }

        public void Attack(Country countryToAttack)
        {
            int amountOfAttackingTroops = Troops - 1; 
            int amoutOfDefenceTroops = countryToAttack.Troops;


            if (!IsNeighbor(countryToAttack) || amountOfAttackingTroops < 1 || HasSamePlayer(countryToAttack))
            {
                return;
            }
            
            int[] attackerCubes = GetCubes(Math.Min(3, amountOfAttackingTroops));
            int[] defenceCubes = GetCubes(Math.Min(2, countryToAttack.Troops));
            
            for (int i = 0; i < Math.Min(defenceCubes.Length, attackerCubes.Length); i++)
            {

                if (amoutOfDefenceTroops == 0 || amountOfAttackingTroops < 1)
                {
                    break;
                }

                if (attackerCubes[i] > defenceCubes[i])
                {
                    Utils.gameLog.Add($"T-{Array.IndexOf(Initialize.global, countryToAttack)}-1-m");
                    amoutOfDefenceTroops--;
                }
                else
                {
                    amountOfAttackingTroops--;
                }
            }

            if (amoutOfDefenceTroops == 0)
            {
                Utils.gameLog.Add($"X-{Array.IndexOf(Initialize.global, this)}-{Array.IndexOf(Initialize.global, countryToAttack)}");
                amoutOfDefenceTroops = amountOfAttackingTroops;
                amountOfAttackingTroops = 0;
                countryToAttack.ChangePlayer(GetPlayer());
            }

            countryToAttack.Troops = amoutOfDefenceTroops;
            Troops =+ amountOfAttackingTroops + 1;
        }

        public bool HasSamePlayer(Country countryToAttack)
        {
            if (countryToAttack.GetPlayer() == GetPlayer())
                return true;
           
            return false;
        }

        public void TransferTroops(Country countryToGetTroops, int amount)
        {
            if (countryToGetTroops.HasSamePlayer(this) && countryToGetTroops.IsNeighbor(this))
            {
                if (Troops <= amount)
                    return;

                Utils.gameLog.Add($"T-{Array.IndexOf(Initialize.global, this)}-{amount}-m");
                Troops -= amount;
                Utils.gameLog.Add($"T-{Array.IndexOf(Initialize.global, countryToGetTroops)}-{amount}-p");
                countryToGetTroops.Troops += amount;
            }
        
        }

        public void TransferTroopsWithoutQuestion(Country countryToGetTroops, int amount)
        {
            Utils.gameLog.Add($"T-{Array.IndexOf(Initialize.global, this)}-{amount}-p");
            Troops += amount;
            Utils.gameLog.Add($"T-{Array.IndexOf(Initialize.global, countryToGetTroops)}-{amount}-m");
            countryToGetTroops.Troops -= amount;
        }

        public bool IsNeighbor(Country countryToCheck)
        {
            if(NeighborCountrys == null) return false;

            foreach(Country country in this.NeighborCountrys)
            {
                if(country ==countryToCheck) return true;   
            }

            return false;
        }

        private static int[] GetCubes(int amountOfCubes)
        {
            int[] cubesToReturn = new int[amountOfCubes];

            for (int i = 0; i < cubesToReturn.Length; i++)
            {
                cubesToReturn[i] = Utils.random.Next(1, 7);
            }

            Array.Sort(cubesToReturn);
            Array.Reverse(cubesToReturn);
            return cubesToReturn;
        }

    }
}
