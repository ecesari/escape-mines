using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Service
{
    public interface ICoordinateService
    {
        Coordinate Create(int x,int y);
    }
    public class CoordinateService:ICoordinateService
    {
        public Coordinate Create(int x, int y)
        {
            return new Coordinate
            {
                X = x,
                Y = y
            };
        }
    }
}
