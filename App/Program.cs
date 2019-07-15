using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace EscapeMine
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main()
        {
            RegisterServices();
            const string path = (@"../../../Data/settings.txt");
            var settings = File.ReadAllLines(path);

            //Get Services
            var boardService = _serviceProvider.GetService<IBoardService>();
            var turtleService = _serviceProvider.GetService<ITurtleService>();

            //Enter Commands
            try
            {
                boardService.Create(settings[0]);
                boardService.CreateMines(settings[1]);
                boardService.CreateExit(settings[2]);
                turtleService.CreateTurtle(settings[3]);
                turtleService.Move(settings[4]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            
            //Get Result
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
