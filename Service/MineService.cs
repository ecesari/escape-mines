using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Helper;
using Helper.Helpers;

namespace Service
{
    public interface IMineService
    {
        Mine Create(Coordinate coordinate);
        void RunInitial(string command);
    }
    public class MineService:IMineService
    {
        public Mine Create(Coordinate coordinate)
        {
            return new Mine
            {
                Position = coordinate
            };
        }

        public void RunInitial(string command)
        {
            //check if int
            //chekc if array valid

            //var s = "ab,cd;ef,gh;ij,kl";
            var mineCoordinates = command.Split(' ').ToArray();
            //var mineCoordinates = command.ToIntArray();
            var mineList = new List<Mine>();

            foreach (var mineCoordinate in mineCoordinates)
            {
                var coordinates = mineCoordinate.ToIntArray();
                //check if mine is within range
                //check if mine already exists
                var mine = new Mine
                {
                    Position = new Coordinate
                    {
                        X = coordinates[0],
                        Y = coordinates[1]
                    }
                };
                mineList.Add(mine);
            }
        }
    }
}
