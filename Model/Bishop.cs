using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Bishop : Piece
    {
        public Bishop(ColorPiece color, Position position) : base(color, position)
        {
            Name = "Bishop";

            if (color == ColorPiece.Black)
            {
                ImagePath = "D:\\HocC#\\Chess_Project\\Resources\\Black_B_5.png";
            }
            else
            {
                ImagePath = "D:\\HocC#\\Chess_Project\\Resources\\White_B_5.png";
            } 
                
        }

        public override List<Position> GetValidMoves(Board board)
        {
            List<Position> moves = new List<Position>();

            // 4 hướng chéo
            int[,] directions =
            {
                { -1, -1 }, // trên trái
                { -1, 1 }, // trên phải
                { 1, -1 }, // dưới trái
                { 1, 1 } // dưới phải
            };

            for( int i = 0; i < directions.GetLength(0); i++)
            {
                int x = Position.X;
                int y = Position.Y;

                while(true)
                {
                    x = x + directions[i, 0];
                    y = y + directions[i, 1];

                    if(board.IsInsideBoard(x, y))
                    {
                        break;
                    }

                    Piece targetPiece = board.GetPiece(x, y);
                    // Ô trống
                    if(targetPiece == null)
                    {
                        moves.Add(new Position(x, y));
                    }
                    else
                    {
                        // Quân đối phương
                        if(targetPiece.Color != Color)
                        {
                            moves.Add(new Position(x, y));
                        }

                        // gặp quân thì dừng
                        break;
                    }
                }
            }

            return moves;
        }
    }
}
