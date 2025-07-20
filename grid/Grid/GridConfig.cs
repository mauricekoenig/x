namespace tcg
{
    public class GridConfig
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        private GridConfig()
        {

        }

        public static GridConfig New(int rows, int columns)
        {
            return new GridConfig { Rows = rows, Columns = columns };
        }

    }
}