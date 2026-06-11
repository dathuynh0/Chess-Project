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
        private Piece[,] squares;

        public Piece[,] Squares { get; set; }

        public Board() 
        {
            squares = new Piece[8, 8];
        }

        public bool IsInsideBoard(int x, int y) // kiểm tra vị trí hợp lệ
        {
            return (x >= 0 && x < 8 && y >= 0 && y < 8);
        }

        public Piece GetPiece(int x, int y) // lấy vị trí hiện tại của quân cờ
        {
            if(!IsInsideBoard(x, y)) return null;

            return squares[x, y];
        }

        public void SetPiece(int x, int y, Piece piece) // set vị trí mới
        {
            if(!IsInsideBoard(x, y))
            {
                return;
            }
            squares[x, y] = piece;
        }

        public void RemovePiece(int x, int y) // xóa quân cờ
        {
            if (!IsInsideBoard(x, y))
            {
                return;
            }
            squares[x, y] = null;
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

        public void InitializeBoard()
        {
            squares = new Piece[8, 8];
        }
    }
}
