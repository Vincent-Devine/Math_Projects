using System.Collections.Generic;
using System.Diagnostics;

namespace TicTacToe
{
    public class MiniMax
    {
        static private int nbIteration;
        static private long time;

        static public Move ComputeMove(Board board, int depth)
        {
            // Stat
            nbIteration = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<Move> availableMoves = board.GetAvailableMoves();
            List<int> scores = new List<int>();
            int indexBestScore = -1;

            foreach (Move move in availableMoves)
            {
                board.MakeMove(move);
                int score = EvaluateMove(board, depth - 1, true);
                scores.Add(score);
                board.UndoMove(move);

                if(indexBestScore == -1 || (score > scores[indexBestScore]))
                    indexBestScore = scores.Count - 1;
            }

            // Stat
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;

            return availableMoves[indexBestScore];
        }

        static private int EvaluateMove(Board board, int depth, bool isMin)
        {
            nbIteration++;

            if (board.IsGameOver() || depth <= 0)
                return board.Evaluate(Player.Circle);

            List<int> scores = new List<int>();
            List<Move> availableMoves = board.GetAvailableMoves();

            foreach (Move move in availableMoves)
            {
                board.MakeMove(move);
                scores.Add(EvaluateMove(board, depth - 1, !isMin));
                board.UndoMove(move);
            }

            if (isMin)
                return Utils.Min(scores);
            else
                return Utils.Max(scores);
        }

        static public int GetIteration()
        {
            return nbIteration;
        }

        static public long GetTime()
        {
            return time;
        }
    }
}
