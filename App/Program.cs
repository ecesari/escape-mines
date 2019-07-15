using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

using Microsoft.Extensions.DependencyInjection;
using Service;

namespace EscapeMine
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();

            //change to dynamic path
            var settings = System.IO.File.ReadAllLines(@"C:\Users\Ece\Documents\Visual Studio 2017\Projects\EscapeMines\Infrastructure\settings.txt");


            //Get Services
            #region GetServices
            var boardService = _serviceProvider.GetService<IBoardService>();
            var mineService = _serviceProvider.GetService<IMineService>();
            var turtleService = _serviceProvider.GetService<ITurtleService>();
            var coordinateService = _serviceProvider.GetService<ICoordinateService>();
            #endregion

            #region Enter Commands
            boardService.Create(settings[0]);
            boardService.CreateMines(settings[1]);
            boardService.CreateExit(settings[2]);
            boardService.CreateTurtle(settings[3]);
            turtleService.Move(settings[4]); 
            #endregion

            var result = turtleService.GetStatus();
            Console.WriteLine(result);
            Console.ReadLine();
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
