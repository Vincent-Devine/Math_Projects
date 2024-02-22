using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    internal class AlphaBeta
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
            int alpha = int.MinValue;
            int beta = int.MaxValue;
            int bestScore = int.MinValue;
            Move bestMove = new Move();

            foreach(Move move in availableMoves)
            {

                board.MakeMove(move);
                int score = EvaluateMove(board, depth - 1, alpha, beta, true);
                board.UndoMove(move);

                if (score > bestScore)
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

        static private int EvaluateMove(Board board, int depth, int alpha, int beta, bool isMin)
        {
            nbIteration++;
            if (board.IsGameOver() || depth <= 0)
                return board.Evaluate(Player.Circle);

            List<Move> availableMoves = board.GetAvailableMoves();

            if(isMin)
            {
                foreach(Move move in availableMoves)
                {
                    board.MakeMove(move);
                    int score = EvaluateMove(board, depth - 1, alpha, beta, !isMin);
                    board.UndoMove(move);

                    beta = Math.Min(beta, score);
                    if (beta <= alpha)
                        break;
                }
                return beta;
            }
            else
            {
                foreach(Move move in availableMoves)
                {
                    board.MakeMove(move);
                    int score = EvaluateMove(board, depth - 1, alpha, beta, !isMin);
                    board.UndoMove(move);

                    alpha = Math.Max(alpha, score);
                    if(alpha >= beta)
                        break;
                }
                return alpha;
            }
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
