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
            int amountOfAttackingTroops = Troops - 1; ; // Here to change 
            int amoutOfDefenceTroops = countryToAttack.Troops;
            
            Debug.Log(amountOfAttackingTroops + " at st");
            Debug.Log(amoutOfDefenceTroops + " de st");

            if (!IsNeighbor(countryToAttack) || amountOfAttackingTroops < 1 || HasSamePlayer(countryToAttack))
            {
               // Debug.Log("Are not Neighbors");
                return;
            }

            Debug.Log("Are Neighbors");
            
            int[] attackerCubes = GetCubes(Math.Min(3, amountOfAttackingTroops));
            int[] defenceCubes = GetCubes(Math.Min(2, countryToAttack.Troops));
            
            Debug.Log($"Times Cubes Thrown: {Math.Min(defenceCubes.Length, attackerCubes.Length)}");
            //Debug.Log($"Attackercubes: {attackerCubes[0]}");
            //Debug.Log($"Defendercubes: {defenceCubes[0]}");
            
            
            for (int i = 0; i < Math.Min(defenceCubes.Length, attackerCubes.Length); i++)
            {

                if (amoutOfDefenceTroops == 0 || amountOfAttackingTroops < 1)
                {
                    Debug.Log("break");
                    break;
                }

                
                Debug.Log($"Attacking {i + 1} time");
                Debug.Log(attackerCubes[i] + " at");
                Debug.Log(defenceCubes[i] + " de");
                
                if (attackerCubes[i] > defenceCubes[i])
                    amoutOfDefenceTroops --;
                else
                {
                    amountOfAttackingTroops--;
                }
                
                Debug.Log($"Attacking: {amountOfAttackingTroops}");
                Debug.Log($"Defence: {amoutOfDefenceTroops}");
            }



            if (amoutOfDefenceTroops == 0)
            {
                amoutOfDefenceTroops = amountOfAttackingTroops;
                amountOfAttackingTroops = 0;
                countryToAttack.ChangePlayer(GetPlayer());
            }

                countryToAttack.Troops = amoutOfDefenceTroops;
                Troops =+ amountOfAttackingTroops + 1;
        }

        private bool HasSamePlayer(Country countryToAttack)
        {
            if (countryToAttack.GetPlayer() == GetPlayer())
                return true;
            
            Debug.Log("Dont have the same Player");
            return false;
        }

        public void TransferTroops(Country countryToGetTroops, int amount) 
        {
            if (Troops <= amount + 1)
                return;

            Troops -= amount;
            countryToGetTroops.Troops += amount;
        }

        private bool IsNeighbor(Country countryToCheck)
        {
            if(NeighborCountrys == null) return false;

            foreach(Country country in this.NeighborCountrys)
            {
                if(country ==countryToCheck) return true;   
            }
                
            Debug.Log("are not neighbors");
            
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
