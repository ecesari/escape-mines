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
