﻿namespace TetrisWPF
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        //простий конструктор
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
