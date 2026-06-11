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

        private Timer turnTimer;
        private int timeLeft;

        #endregion
        public frmMain()
        {
            InitializeComponent();
            board = new Board();

            chessBoard = new ChessBoardManager(pnlBoard, board, lblTurn);
            chessBoard.DrawChessBoard();
            lblTurn.Text = chessBoard.GetCurrentTurn();
            chessBoard.TurnChanged += ChessBoard_TurnChanged;
            board.SetupPieces();
            chessBoard.DrawPieces(board);
            turnTimer = new Timer();
            turnTimer.Interval = 1000; // 1000 mls
            turnTimer.Tick += TurnTimer_Tick;

            StartTurn();
        }

        private void ChessBoard_TurnChanged()
        {
            StartTurn();
        }

        private void StartTurn()
        {
            turnTimer.Stop();

            timeLeft = 30;

            lblTime.Text = $"{timeLeft}s";

            turnTimer.Start();
        }

        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--; // trừ 1s
            lblTime.Text = $"{timeLeft}s"; // cập nhật label
            if (timeLeft <= 0)
            {
                turnTimer.Stop();
                chessBoard.SwitchTargetTurn();
                StartTurn();
            }
        }
    }
}
