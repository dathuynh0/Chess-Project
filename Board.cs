using Chess_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project
{
    public class Board
    {
        public Piece[,] Squares { get; set; }

        public Board() 
        {
            Squares = new Piece[8, 8];
        }

        public bool IsInsideBoard(int x, int y) // kiểm tra vị trí hợp lệ
        {
            return (x >= 0 && x < 8 && y >= 0 && y < 8);
        }

        public Piece GetPiece(int x, int y) // lấy vị trí hiện tại của quân cờ
        {
            if(!IsInsideBoard(x, y)) return null;

            return Squares[x, y];
        }

        public void SetPiece(int x, int y, Piece piece) // set vị trí mới
        {
            if(!IsInsideBoard(x, y))
            {
                return;
            }
            Squares[x, y] = piece;
        }

        public void RemovePiece(int x, int y) // xóa quân cờ
        {
            if (!IsInsideBoard(x, y))
            {
                return;
            }
            Squares[x, y] = null;
        }

        public bool MovePiece(Position from, Position to)
        {
            Piece piece = GetPiece(from.X, from.Y);
            if(piece == null) 
                return false;

            SetPiece(to.X, to.Y, piece);
            RemovePiece(from.X, from.Y);

            piece.Position = new Position(to.X, to.Y);
            piece.HasMoved = true;
            return true;
        }

        public bool IsEmpty(Position position) // kiểm tra ô trống
        {
            return GetPiece(position.X, position.Y) == null;
        }

        public bool HasEnemyPiece (Position position, ColorPiece currentColor) // kiểm tra đối phương
        {
            Piece piece = GetPiece(position.X, position.Y);
            if(piece == null) return false;

            return piece.Color != currentColor;
        }

        public bool HasAllyPiece(Position position, ColorPiece currentColor) // kiểm tra quân cùng màu
        {
            Piece piece = GetPiece(position.X, position.Y);
            if (piece == null) return false;

            return piece.Color == currentColor;
        }
        // phong hậu
        public bool IsPromotion(Piece piece)
        {
            if (!(piece is Piece)) return false;

            // tốt màu trắng ở dưới nên tới hàng 0 là cuối cùng nên phong hậu
            if (piece.Color == ColorPiece.White && piece.Position.X == 0)
            {
                return true;
            }

            // tốt đen ngược lại
            if (piece.Color == ColorPiece.Black && piece.Position.X == 7)
            {
                return true;
            }

            return false;
        }

        public void PromatePawn(Pawn pawn) // phong hậu cho tốt
        {
            Piece queen = pawn.Promote();

            SetPiece(pawn.Position.X, pawn.Position.Y, queen);
        }

        public void SetupPieces()
        {
            // Xe
            Squares[0, 0] = new Rook(ColorPiece.Black, new Position(0, 0));
            Squares[0, 7] = new Rook(ColorPiece.Black, new Position(0, 7));

            Squares[7, 0] = new Rook(ColorPiece.White, new Position(7, 0));
            Squares[7, 7] = new Rook(ColorPiece.White, new Position(7, 7));

            // Mã
            Squares[0, 1] = new Knight(ColorPiece.Black, new Position(0, 1));
            Squares[0, 6] = new Knight(ColorPiece.Black, new Position(0, 6));

            Squares[7, 1] = new Knight(ColorPiece.White, new Position(7, 1));
            Squares[7, 6] = new Knight(ColorPiece.White, new Position(7, 6));

            // Tượng
            Squares[0, 2] = new Bishop(ColorPiece.Black, new Position(0, 2));
            Squares[0, 5] = new Bishop(ColorPiece.Black, new Position(0, 5));

            Squares[7, 2] = new Bishop(ColorPiece.White, new Position(7, 2));
            Squares[7, 5] = new Bishop(ColorPiece.White, new Position(7, 5));

            // Hậu
            Squares[0, 3] = new Queen(ColorPiece.Black, new Position(0, 3));
            Squares[7, 3] = new Queen(ColorPiece.White, new Position(7, 3));

            // Vua
            Squares[0, 4] = new King(ColorPiece.Black, new Position(0, 4));
            Squares[7, 4] = new King(ColorPiece.White, new Position(7, 4));

            // Tốt
            for (int i = 0; i < 8; i++)
            {
                Squares[1, i] = new Pawn(ColorPiece.Black, new Position(1, i));
                Squares[6, i] = new Pawn(ColorPiece.White, new Position(6, i));
            }
        }

        // kiểm tra vua bị chiếu chưa
        public King FindKing(ColorPiece colorPiece)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = Squares[i, j];

                    if (piece is King king && king.Color == colorPiece)
                    {
                        return king;
                    }
                }
            }

            return null;
        }

        public bool IsKingCheck(ColorPiece kingColor)
        {
            King king = FindKing(kingColor);

            if (king == null) return false;

            Position kingPosition = king.Position;

            // duyệt tất cả quân cờ
            for (int x = 0; x < 8; x++) 
            {
                for (int  y = 0; y < 8; y++)
                {
                    Piece piece = Squares[x, y];
                    if (piece == null) continue;
                    
                    // Chỉ xét quân khác màu
                    if(piece.Color == kingColor)
                    {
                        continue;
                    }

                    List<Position> moves = piece.GetValidMoves(this);

                    if(moves.Any(m => m.X == kingPosition.X && m.Y == kingPosition.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // hàm phục vụ AI
        public Board Clone()
        {
            Board clone = new Board();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = GetPiece(x, y);

                    if (piece == null)
                        continue;

                    clone.SetPiece(x, y, piece.Clone());
                }
            }

            return clone;
        }

        public bool IsCheckMate(ColorPiece color) // checkmate
        {
            return IsKingCheck(color) &&
                   !HasAnyLegalMove(color);
        }

        public bool HasAnyLegalMove(ColorPiece color) // kiểm tra còn đường đi không
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = GetPiece(x, y);

                    if (piece == null || piece.Color != color)
                        continue;

                    List<Position> moves = piece.GetValidMoves(this);

                    foreach (Position move in moves)
                    {
                        Board clone = Clone();

                        clone.MovePiece(
                            new Position(piece.Position.X, piece.Position.Y),
                            new Position(move.X, move.Y));

                        // Sau khi đi thử, vua mình không bị chiếu
                        if (!clone.IsKingCheck(color))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
