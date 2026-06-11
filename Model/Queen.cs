using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Queen : Piece
    {
        public Queen(ColorPiece color, Position position) : base(color, position)
        {
            ImagePath = "";
            Name = "Queen";
        }

        public override List<Position> GetValidMoves(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
