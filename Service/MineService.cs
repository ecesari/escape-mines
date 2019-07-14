using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain;
using Helper.Helpers;

namespace Service
{
    public interface IMineService
    {
        void Create(string command);
    }
    public class MineService:IMineService
    {
        private readonly IBoardService _boardService;
        private readonly ICoordinateService _coordinateService;

        public MineService(IBoardService boardService, ICoordinateService coordinateService)
        {
            _boardService = boardService;
            _coordinateService = coordinateService;
        }

        public void Create(string command)
        {
            var mineCoordinates = command.ToStringArray(' ');

            var mineList = CreateMines(mineCoordinates);
            _boardService.AddMines(mineList);
        }

        private  List<Mine> CreateMines(string[] mineCoordinates)
        {
            var mineList = new List<Mine>();

            foreach (var mineCoordinate in mineCoordinates)
            {
                var coordinates = mineCoordinate;
                //check if mine is within range
                //check if mine already exists

                var mine = new Mine
                {
                   Position = _coordinateService.Create(coordinates[0], coordinates[1])
                };
                mineList.Add(mine);
            }

            return mineList;
        }
    }
}
