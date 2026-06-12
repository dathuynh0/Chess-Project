using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_Project.Model
{
    public abstract class Piece
    {
        public ColorPiece Color { get; set; } // quân trắng 0 hoặc đen 1    
        public Position Position { get; set; }
        public Image PieceImage { get; protected set; }
        public string Name { get; set; }
        public bool HasMoved { get; set; }
        
        public virtual int Value { get { return 0; } }
        public abstract Piece Clone();

        public Piece (ColorPiece color, Position position)
        {
            Color = color;
            Position = position;
            HasMoved = false;
        }

        public abstract List<Position> GetValidMoves(Board board); // tất cả các ô mà quân cờ có thể đi.
        public virtual void Move(Position newPosition)
        {
            this.Position = newPosition;
            HasMoved = true;
        }
    }
}
