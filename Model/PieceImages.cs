using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Project.Model
{
    public static class PieceImages
    {
        public static readonly Image WhitePawn =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\White_P_5.png");

        public static readonly Image BlackPawn =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\Black_P_5.png");

        public static readonly Image WhiteRook =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\White_R_5.png");

        public static readonly Image BlackRook =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\Black_R_5.png");

        public static readonly Image WhiteKnight =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\White_N_5.png");

        public static readonly Image BlackKnight =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\Black_N_5.png");

        public static readonly Image WhiteBishop =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\White_B_5.png");

        public static readonly Image BlackBishop =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\Black_B_5.png");

        public static readonly Image WhiteQueen =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\White_Q_5.png");

        public static readonly Image BlackQueen =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\Black_Q_5.png");

        public static readonly Image WhiteKing =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\White_K_5.png");

        public static readonly Image BlackKing =
            Image.FromFile(@"D:\HocC#\Chess_Project\Resources\Black_K_5.png");
    }
}
