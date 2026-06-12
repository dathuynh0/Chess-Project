using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Rook : Piece
    {
        public override int Value => 500;
        public override Piece Clone()
        {
            Rook rook = new Rook( Color, new Position(Position.X, Position.Y));

            rook.HasMoved = HasMoved;

            return rook;
        }
        public Rook(ColorPiece color, Position position) : base(color, position)
        {
            Name = "Rook";

            if(color == ColorPiece.Black)
            {
                PieceImage = PieceImages.BlackRook;
            }
            else
            {
                PieceImage = PieceImages.WhiteRook;
            }
        }

        public override List<Position> GetValidMoves(Board board)
        {
            List<Position> moves = new List<Position>();

            int[,] directions =
            {
                { -1, 0 }, // đi lên 
                { 1, 0 }, // đi xuống
                { 0, -1 }, // đi qua trái
                { 0, 1 } // đi qua phải
            };

            for(int i = 0; i < directions.GetLength(0); i++)
            {
                int x = Position.X;
                int y = Position.Y;

                while(true)
                {
                    x = x + directions[i, 0];
                    y = y + directions[i, 1];

                    // ngoài bàn cờ
                    if (!board.IsInsideBoard(x, y)) break;

                    Piece targetPiece = board.GetPiece(x, y);

                    // ô trống
                    if(targetPiece == null)
                    {
                        moves.Add(new Position(x, y));
                    }
                    else
                    {
                        // quân đối phương
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
