using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_Project
{
    public partial class frmMain : Form
    {
        #region Properties
        ChessBoardManager chessBoard;
        private Board board;
        #endregion
        public frmMain()
        {
            InitializeComponent();

            chessBoard = new ChessBoardManager(pnlBoard);
            chessBoard.DrawChessBoard();

            board = new Board();

            board.SetupPieces();
            chessBoard.DrawPieces(board);
        }
    }
}
