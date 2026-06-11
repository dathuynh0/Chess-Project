using Chess_Project.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_Project
{
    public class ChessBoardManager
    {
        #region Properties

        private Panel chessBoard;

        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        public Button[,] Matrix { get; set; }

        #endregion

        #region Initialize

        public ChessBoardManager(Panel chessBoard)
        {
            this.chessBoard = chessBoard;
            Matrix = new Button[8, 8];
        }

        #endregion

        #region Method

        public void DrawChessBoard()
        {
            chessBoard.Controls.Clear();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(
                            col * Cons.CHESS_WIDTH,
                            row * Cons.CHESS_HEIGHT),
                        Tag = new Position(row, col),
                        FlatStyle = FlatStyle.Flat
                    };

                    if ((row + col) % 2 == 0)
                    {
                        btn.BackColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.DarkGreen;
                    }

                    btn.Click += Btn_Click;

                    Matrix[row, col] = btn;

                    chessBoard.Controls.Add(btn);
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            Position pos = (Position)btn.Tag;

            MessageBox.Show(
                $"Row: {pos.X}\nCol: {pos.Y}");
        }

        public void DrawPieces(Board board)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button btn = Matrix[row, col];

                    Piece piece = board.GetPiece(row, col);

                    btn.BackgroundImage = null;

                    if (piece == null)
                        continue;

                    if (System.IO.File.Exists(piece.ImagePath))
                    {
                        btn.BackgroundImage = Image.FromFile(piece.ImagePath);
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }

        #endregion
    }
}
