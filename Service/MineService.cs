using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain;
using Helper.Helpers;

namespace Service
{
    public interface IMineService
    {

        Mine CreateMine(int[] mineCoordinates);
    }
    public class MineService : IMineService
    {
        private readonly ICoordinateService _coordinateService;

        public MineService(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }

        //public void Create(string command)
        //{
        //    var mineCoordinates = command.ToStringArray(' ');

        //    var mineList = CreateMines(mineCoordinates);
        //    if (!_boardService.BoardExists())
        //    {
        //        throw new ArgumentNullException("There is no board!");
        //    }


        //    _boardService.AddMines(mineList);
        //}

        //public List<Mine> CreateMines(string[] mineCoordinates)
        //{
        //    var mineList = new List<Mine>();

        //    foreach (var mineCoordinate in mineCoordinates)
        //    {
        //        var coordinates = mineCoordinate.ToIntArray(',');
        //        var x = coordinates[0];
        //        var y = coordinates[1];

        //        #region Validations
        //        var mineExistsInLocation = _boardService.MineExistsInLocation(x, y);
        //        if (mineExistsInLocation)
        //            throw new Exception($"There is already a mine deployed at location[{x}],[{y}]. You cannot put another mine there!");
        //        var exitExistsInLocation = _boardService.ExitExistsInLocation(x, y);
        //        if (exitExistsInLocation)
        //            throw new Exception($"There is an exit at location[{x}],[{y}]. You cannot put a mine there!");
        //        var turtleExistsInLocation = _boardService.TurtleExistsInLocation(x, y);
        //        if (turtleExistsInLocation)
        //            throw new Exception($"There is a turtle at location[{x}],[{y}]. You cannot put a mine there!");

        //        var mineIsInRange = _boardService.ValidPosition(x, y);
        //        if (!mineIsInRange)
        //            throw new Exception($"The position of the mine is not in the range of the board!");
        //        #endregion




        //        var mine = new Mine
        //        {
        //            Position = _coordinateService.Create(x, y)
        //        };
        //        mineList.Add(mine);
        //    }

        //    return mineList;
        //}

        public Mine CreateMine(int[] mineCoordinates)
        {
            var x = mineCoordinates[0];
            var y = mineCoordinates[1];

            var mine = new Mine
            {
                Position = _coordinateService.Create(x, y)
            };
            return mine;
        }
    }
}
