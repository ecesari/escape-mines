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

            var settings = System.IO.File.ReadAllLines(@"C:\Users\ece.sarioglu\source\repos\escape-mines\Infrastructure\settings.txt");

            var boardService = _serviceProvider.GetService<IBoardService>();
            var mineService = _serviceProvider.GetService<IMineService>();
            var turtleService = _serviceProvider.GetService<ITurtleService>();
            var coordinateService = _serviceProvider.GetService<ICoordinateService>();

            boardService.RunInitial(settings[0]);
            mineService.RunInitial(settings[1]);
            coordinateService.RunExitCreationCommand(settings[2]);
            turtleService.RunInitial(settings[3]);
            turtleService.Move(settings[4]);

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
