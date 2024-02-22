using System.Collections.Generic;

namespace TicTacToe
{
    public class Utils
    {
        static public int Min(List<int> values)
        {
            int min = int.MaxValue;
            foreach (int value in values)
            {
                if (min > value)
                    min = value;
            }
            return min;
        }

        static public int Max(List<int> values)
        {
            int max = int.MinValue;
            foreach (int value in values)
            {
                if (max < value)
                    max = value;
            }
            return max;
        }
    }
}
