using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Bishop : Piece
    {
        public override int Value => 330;
        public override Piece Clone()
        {
            Bishop bishop = new Bishop( Color, new Position(Position.X, Position.Y));

            bishop.HasMoved = HasMoved;

            return bishop;
        }
        public Bishop(ColorPiece color, Position position) : base(color, position)
        {
            Name = "Bishop";

            if (color == ColorPiece.Black)
            {
                PieceImage = PieceImages.BlackBishop;
            }
            else
            {
                PieceImage = PieceImages.WhiteBishop;
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

                    if(!board.IsInsideBoard(x, y))
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
