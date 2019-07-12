using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Helper;
using Infrastructure.Enums;
using Infrastructure.Helper;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace EscapeMine
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            var boardService = _serviceProvider.GetService<IBoardService>();
            var mineService = _serviceProvider.GetService<IMineService>();
            var turtleService = _serviceProvider.GetService<ITurtleService>();
            var coordinateService = _serviceProvider.GetService<ICoordinateService>();

            var settings = System.IO.File.ReadAllLines(@"C:\Users\ece.sarioglu\source\repos\escape-mines\Infrastructure\settings.txt");

            var boardSize = settings[0];
            var boardSizeCoordinates = boardSize.ToIntArray();
            var boardDimension = coordinateService.Create(boardSizeCoordinates[0], boardSizeCoordinates[1]);


            var mines = settings[1];
            var mineCoordinates = mines.ToIntArray();
            var mineList = new List<Mine>();
            

            for (var i = 0; i < mineCoordinates.Length; i++)
            {
                if (i % 2 == 0)
                {
                    var coordinate = coordinateService.Create(mineCoordinates[i], mineCoordinates[i + 1]);
                    var mine = mineService.Create(coordinate);
                    mineList.Add(mine);
                }
            }


            var exit = settings[2];
            var exitPoint = coordinateService.Create(exit[0], exit[1]);

            var turtleStartingPosition = settings[3];
            var turtleStartingCoordinate = coordinateService.Create(turtleStartingPosition[0], turtleStartingPosition[1]);
            var turtleOrientation = EnumHelper<Orientation>.GetValueFromName(turtleStartingPosition[2].ToString());
            var turtle = turtleService.Create(turtleStartingCoordinate,turtleOrientation);


            var board = boardService.Create(boardDimension,mineList,exitPoint,turtle);


            var moves = settings[4];

            foreach (var move in moves)
            {
                var movement = EnumHelper<Movement>.GetValueFromName(move.ToString());
                turtleService.Move(movement,turtle);
            }


        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IBoardService, BoardService>();
            collection.AddScoped<ITurtleService, TurtleService>();
            collection.AddScoped<IMineService, MineService>();
            collection.AddScoped<ICoordinateService, CoordinateService>();
            _serviceProvider = collection.BuildServiceProvider();
        }
    }
}
