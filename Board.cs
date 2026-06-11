using Chess_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

            piece.Position = to;
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
    }
}
