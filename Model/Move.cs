using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public class Move
    {
        public Position From {  get; set; }
        public Position To { get; set; }

        public Move(Position from, Position to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
