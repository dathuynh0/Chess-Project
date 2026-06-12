using Chess_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.AI
{
    public class ChessAI
    {
        private Random random = new Random();
        private BoardEvaluator boardEvaluator = new BoardEvaluator();

        public Move GetRandomMove(Board board)
        {
            List<Move> moves = GetAllMoves(board, ColorPiece.Black);

            if (moves.Count == 0)
                return null;

            return moves[random.Next(moves.Count)];
        }

        private List<Move> GetAllMoves(Board board, ColorPiece color)
        {
            List<Move> moves = new List<Move>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board.GetPiece(x, y);

                    if (piece == null)
                        continue;

                    if (piece.Color != color)
                        continue;

                    foreach (Position pos in piece.GetValidMoves(board))
                    {
                        moves.Add(new Move(piece.Position, pos));
                    }
                }
            }

            return moves;
        }

        public int AlphaBeta(Board board, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0)
            {
                return boardEvaluator.Evaluate(board);
            }

            ColorPiece color = maximizingPlayer ? ColorPiece.Black : ColorPiece.White;

            List<Move> moves = GetAllMoves(board, color);

            if (moves.Count == 0)
            {
                return boardEvaluator.Evaluate(board);
            }
            moves = moves.OrderByDescending(m =>
            {
                Piece target = board.GetPiece(m.To.X, m.To.Y);

                return target?.Value ?? 0;
            }).ToList();

            // Max Đen
            if (maximizingPlayer)
            {
                int bestScore = int.MinValue;

                foreach (Move move in moves)
                {
                    Board clone = board.Clone();

                    clone.MovePiece(move.From, move.To);

                    int score = AlphaBeta(clone, depth - 1, alpha, beta, false);

                    bestScore = Math.Max(bestScore, score);

                    alpha = Math.Max(alpha, bestScore);

                    // Cắt nhánh
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return bestScore;
            }
            // MIN (Người chơi Trắng)
            else
            {
                int bestScore = int.MaxValue;

                foreach (Move move in moves)
                {
                    Board clone = board.Clone();

                    clone.MovePiece(move.From, move.To);

                    int score = AlphaBeta(clone, depth - 1, alpha, beta, true);

                    bestScore = Math.Min(bestScore, score);

                    beta = Math.Min(beta, bestScore);

                    // Cắt nhánh
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return bestScore;
            }
        }


        public Move GetBestMove(Board board, int depth)
        {
            /*
             Depth = 1  : rất yếu
             Depth = 2  : dễ
             Depth = 3  : khá mạnh
             Depth = 4  : bắt đầu chậm
             */
            Move bestMove = null;
            int bestScore = int.MinValue;

            List<Move> moves = GetAllMoves(board, ColorPiece.Black);

            moves = moves.OrderByDescending(m =>
            {
                Piece target = board.GetPiece(m.To.X, m.To.Y);

                return target?.Value ?? 0;
            }).ToList();

            foreach (Move move in moves)
            {
                Board clone = board.Clone();
                clone.MovePiece(move.From, move.To);

                int score = AlphaBeta(clone, depth - 1, int.MinValue, int.MaxValue, false);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }
    }
}
