using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Service
{
    public interface IMineService
    {
        Mine Create(Coordinate coordinate);
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
    }
}
