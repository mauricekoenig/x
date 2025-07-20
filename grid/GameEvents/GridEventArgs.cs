using System;

namespace tcg
{
    public class GridEventArgs : EventArgs
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private GridEventArgs(int rows, int columns)
        {
            Rows = rows; Columns = columns;
        }

        public static GridEventArgs New (int rows, int columns)
        {
            return new GridEventArgs(rows, columns);
        }
    }
}