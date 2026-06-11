using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Pawn : Piece
    {
        public Pawn(ColorPiece color, Position position) : base(color, position)
        {
            Name = "Pawn";

            if (color == ColorPiece.Black) 
            {
                PieceImage = Image.FromFile("D:\\HocC#\\Chess_Project\\Resources\\Black_P_5.png");
            }
            else
            {
                PieceImage = Image.FromFile("D:\\HocC#\\Chess_Project\\Resources\\White_P_5.png");
            }
        }

        public override List<Position> GetValidMoves(Board board)
        {
            List<Position> moves = new List<Position>();

            // hướng duy chuyển
            int direction = Color == ColorPiece.White ? -1 : 1;

            // đi 1 ô
            Position oneStep = new Position(Position.X + direction, Position.Y);
            if (board.IsInsideBoard(oneStep.X, oneStep.Y) && board.IsEmpty(oneStep))
            {
                moves.Add(oneStep);

                // duy chuyển lần đầu 2 ô
                Position twoStep = new Position(Position.X + direction * 2, Position.Y);
                if (!HasMoved && board.IsInsideBoard(twoStep.X, twoStep.Y) && board.IsEmpty(twoStep))
                {
                    moves.Add(twoStep);
                }
            }

            // ăn chéo trái
            Position leftCapture = new Position(Position.X + direction, Position.Y - 1);
            if (board.IsInsideBoard(leftCapture.X, leftCapture.Y) && board.HasEnemyPiece(leftCapture, Color))
            {
                moves.Add(leftCapture);
            }

            // ăn chéo phải
            Position rightCapture = new Position(Position.X + direction, Position.Y + 1);
            if (board.IsInsideBoard(rightCapture.X, rightCapture.Y) &&  board.HasEnemyPiece(rightCapture, Color))
            {
                moves.Add(rightCapture);
            }

            return moves;
        }

        public Piece Promote() // phong hậu
        {
            return new Queen(Color, Position);
        }
    }
}
