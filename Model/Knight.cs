using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Knight : Piece
    {
        public override int Value => 320;
        public override Piece Clone()
        {
            Knight knight = new Knight( Color, new Position(Position.X, Position.Y));

            knight.HasMoved = HasMoved;

            return knight;
        }
        public Knight(ColorPiece color, Position position) : base(color, position)
        {
            Name = "Knight";

            if(color == ColorPiece.Black)
            {
                PieceImage = PieceImages.BlackKnight;
            }
            else
            {
                PieceImage = PieceImages.WhiteKnight;
            }
        }

        public override List<Position> GetValidMoves(Board board)
        {
            List<Position> moves = new List<Position>();

            

            int[,] knightMoves =
            {
                {-2, -1},
                {-2,  1},

                {-1, -2},
                {-1,  2},

                { 1, -2},
                { 1,  2},

                { 2, -1},
                { 2,  1}
            };

            for(int i = 0; i < knightMoves.GetLength(0); i++)
            {
                int x = Position.X;
                int y = Position.Y;
                int newX = x + knightMoves[i, 0];
                int newY = y + knightMoves[i, 1];

                if (!board.IsInsideBoard(newX, newY)) continue;

                Piece targetPiece = board.GetPiece(newX, newY);

                // Ô trống
                if(targetPiece == null)
                {
                    moves.Add(new Position(newX, newY));
                }
                else if(targetPiece.Color != Color) // quân đối phương
                {
                    moves.Add(new Position(newX, newY));
                }
            }

            return moves;
        }
    }
}
