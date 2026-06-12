using Chess_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.AI
{
    public class BoardEvaluator
    {
        public int Evaluate(Board board)
        {
            int score = 0;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board.GetPiece(x, y);

                    if (piece == null)
                        continue;

                    if (piece.Color == ColorPiece.Black)
                        score += piece.Value;
                    else
                        score -= piece.Value;
                }
            }

            return score;
        }
    }
}
