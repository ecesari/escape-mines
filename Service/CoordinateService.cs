﻿using Domain;
using Helper;
using Helper.Helpers;

namespace Service
{
    public interface ICoordinateService
    {
        Coordinate Create(int x, int y);
        void RunExitCreationCommand(string command);
    }
    public class CoordinateService : ICoordinateService
    {
        private readonly IBoardService _boardService;

        public CoordinateService(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public Coordinate Create(int x, int y)
        {
            return new Coordinate
            {
                X = x,
                Y = y
            };
        }

        public void RunExitCreationCommand(string command)
        {
            var exit = command.ToIntArray();
            var exitCoordinate = CreateCoordinate(exit[0], exit[1]);
            _boardService.AddExit(exitCoordinate);
        }

        private static Coordinate CreateCoordinate(int x, int y)
        {
            return new Coordinate
            {
                X = x,
                Y = y
            };
        }
    }
}
