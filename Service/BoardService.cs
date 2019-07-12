using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Domain;
using Helper;
using Helper.Helpers;


namespace Service
{
    public interface IBoardService
    {

        void RunInitial(string command);
    }
    public class BoardService : IBoardService
    {
        private Board _board;

        private void CreateInitial(int width, int height)
        {
            _board = new Board
            {
               Width = width,
               Height = height
            };
        }
        //public Board CreateInitial(List<Mine> mines, Coordinate exit,
        //    Turtle turtle)
        //{
        //    return new Board
        //    {
        //        Turtle = turtle,
        //        Mines = mines,
        //        ExitPoint = exit
        //    };
        //}



        public void RunInitial(string command)
        {
            var boardSizeCoordinates = command.ToIntArray();
            //check if int
            CreateInitial(boardSizeCoordinates[0], boardSizeCoordinates[1]);
        }
    }
}
