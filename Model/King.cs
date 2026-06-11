using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class King : Piece
    {
        public King(ColorPiece color, Position position) : base(color, position)
        {
            Name = "King";

            if (color == ColorPiece.Black)
            {
                PieceImage = Image.FromFile("D:\\HocC#\\Chess_Project\\Resources\\Black_K_5.png");
            }
            else
            {
                PieceImage = Image.FromFile("D:\\HocC#\\Chess_Project\\Resources\\White_K_5.png");
            }    
        }

        public override List<Position> GetValidMoves(Board board)
        {
            List<Position> moves = new List<Position>();

            int[,] directions =
            {
                {-1,-1},
                {-1, 0},
                {-1, 1},

                { 0,-1},
                { 0, 1},

                { 1,-1},
                { 1, 0},
                { 1, 1}
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = Position.X + directions[i, 0];
                int newY = Position.Y + directions[i, 1];

                if (!board.IsInsideBoard(newX, newY))
                    continue;

                Piece targetPiece = board.GetPiece(newX, newY);

                if (targetPiece == null)
                {
                    moves.Add(new Position(newX, newY));
                }
                else if (targetPiece.Color != Color)
                {
                    moves.Add(new Position(newX, newY));
                }
            }

            return moves;
        }

        public bool CanCastle(Board board)
        {
            // nhập thành ...
            return false;
        }
    }
}
