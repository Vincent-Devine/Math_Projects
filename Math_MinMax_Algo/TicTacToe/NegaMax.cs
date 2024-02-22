using System.Collections.Generic;
using System.Diagnostics;

namespace TicTacToe
{
    internal class NegaMax
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
            int bestScore = int.MinValue;
            Move bestMove = availableMoves[0];

            foreach (Move move in availableMoves)
            {
                board.MakeMove(move);
                int score = -EvaluateMove(board, depth - 1);
                board.UndoMove(move);

                if(score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            // Stat
            stopwatch.Stop();
            time = stopwatch.ElapsedMilliseconds;

            return bestMove;
        }

        static private int EvaluateMove(Board board, int depth)
        {
            nbIteration++;

            if (board.IsGameOver() || depth <= 0)
                return board.Evaluate();

            int bestScore = int.MinValue;
            List<Move> availableMoves = board.GetAvailableMoves();

            foreach (Move move in availableMoves)
            {
                int score = 0;
                board.MakeMove(move);
                score = -EvaluateMove(board, depth - 1);
                board.UndoMove(move);

                if(score > bestScore)
                    bestScore = score;
            }

            return bestScore;
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
