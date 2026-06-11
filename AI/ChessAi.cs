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
        private Board board;

        public ChessAI(Board board)
        {
            this.board = board;
        }

        public Move GetBestMove()
        {
            List<Move> allMoves = GetAllMoves(ColorPiece.Black); // AI cho quân đen

            Move bestMove = null;
            int bestScore = int.MinValue;

            foreach (Move move in allMoves)
            {
                Board copy = board.Clone();
                copy.MovePiece(move.From, move.To);

                // tránh nước đi tự sát
                if (copy.IsKingCheck(ColorPiece.Black))
                {
                    continue;
                }

                int score = EvaluateBoard(copy);

                if(score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                } 
            }

            return bestMove;
        }

        public List<Move> GetAllMoves(ColorPiece colorPiece) // lấy tất cả đường đi
        {
            List<Move> moves = new List<Move>();

            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    Piece p = board.GetPiece(x, y); // lấy vị trí hiện tại của quân cờ

                    if(p == null || p.Color != colorPiece) continue; // nếu ô trông hoặc không cùng phe thì bỏ qua

                    var valid = p.GetValidMoves(board);

                    foreach (var move in valid) // duyệt từng nước đi
                    {
                        moves.Add(new Move // lưu vào danh sách moves
                        {
                            From = new Position(x, y),
                            To = move
                        });
                    }
                }    
            }
            return moves;
        }

        private int EvaluateBoard(Board board) // tính điểm
        {
            int score = 0;

            for (int x = 0;x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    Piece piece = board.GetPiece(x, y);
                    if(piece == null) continue;

                    int value = GetPieceValue(piece);

                    if(piece.Color == ColorPiece.Black)
                    {
                        score += value;
                    }
                    else
                    {
                        score -= value;
                    }    
                } 
            }

            return score;
        }

        private int GetPieceValue(Piece piece)
        {
            if (piece is Pawn) return 1; // tốt
            if (piece is Knight) return 3; // ngựa
            if (piece is Bishop) return 3; // tượng
            if (piece is Rook) return 5; // xe
            if (piece is Queen) return 9; // hậu
            if (piece is King) return 1000; // vua

            return 0;
        }
    }
}
