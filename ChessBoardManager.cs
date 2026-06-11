using Chess_Project.AI;
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
        private Label lblTurn;
        private Board board;
        private Piece selectedPiece;
        private List<Position> validMoves;
        private ColorPiece currentTurn; // lượt của ai
        public event Action TurnChanged;

        // tạo AI
        private ChessAI ai;

        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        public Button[,] Matrix { get; set; }

        #endregion

        #region Initialize

        public ChessBoardManager(Panel chessBoard, Board board, Label lblTurn)
        {
            this.chessBoard = chessBoard;
            this.board = board;
            this.lblTurn = lblTurn;

            Matrix = new Button[8, 8];
            currentTurn = ColorPiece.White;

            // khởi tạo ai
            ai = new ChessAI(board);
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

            Position position = (Position)btn.Tag;

            HandleClick(position);
        }

        private void HandleClick(Position position)
        {
            Piece piece = board.GetPiece(position.X, position.Y);

            // chưa chọn quân
            if(selectedPiece == null)
            {
                if (piece == null) return;
                if(piece == null && piece.Color == currentTurn)
                {
                    return;
                }

                if(piece.Color != currentTurn)
                {
                    return;
                }
                selectedPiece = piece;
                validMoves = piece.GetValidMoves(board);

                HighlightMoves(validMoves);

                return;
            }

            MoveSelectedPiece(position);
        }

        private void HighlightMoves(List<Position> moves)
        {
            foreach (Position move in moves)
            {
                Matrix[move.X, move.Y].BackColor = Color.DarkBlue;
            }
        }

        private void ClearHighlight()
        {
            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        Matrix[x, y].BackColor
                            = Color.White;
                    }
                    else
                    {
                        Matrix[x, y].BackColor
                            = Color.DarkGreen;
                    }
                } 
                    
            }
        }

        private bool IsValidMove(Position position)
        {
            return validMoves.Any(p => p.X == position.X && p.Y == position.Y);
        }

        private void MoveSelectedPiece(Position target) // bắt đầu đi
        {
            if (!IsValidMove(target))
            {
                ClearHighlight();
                selectedPiece = null;
                return;
            }

            Piece targetPiece = board.GetPiece(target.X, target.Y);
            if (targetPiece != null && targetPiece.Color == selectedPiece.Color)
            {
                ClearHighlight();
                selectedPiece = null;
                return;
            }

            board.MovePiece(selectedPiece.Position, target);
            ColorPiece opponent = currentTurn == ColorPiece.White
            ? ColorPiece.Black
            : ColorPiece.White;
            if (board.IsKingCheck(opponent)) // kiểm tra vua bị chiếu
            {
                King king = board.FindKing(opponent);
                Matrix[king.Position.X, king.Position.Y].BackColor = Color.Blue;
                MessageBox.Show($"Vua {GetOpponentName(opponent)} đang bị chiếu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if(targetPiece is King dieKing)
            {
                string winner = dieKing.Color == ColorPiece.Black ? "Trắng" : "Đen";
                MessageBox.Show($"Vua {GetOpponentName(dieKing.Color)} đã chết./n Người chơi {winner} thắng",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                EndGame();
                return;
            }

            if (selectedPiece is Pawn pawn) // kiểm tra có phải là tốt
            {
                if (board.IsPromotion(pawn)) // gọi hàm IsPromotion() để kiểm tra màu trắng hay đen đã đi tới cuối chưa
                {
                    board.PromatePawn(pawn);
                }
            }
            SwitchTargetTurn();
            ClearHighlight();

            selectedPiece = null;
            DrawPieces(board);

            if (currentTurn == ColorPiece.Black) // AI chơi quân đen
            {
                MakeAIMove();
            }
        }

        public void SwitchTargetTurn()
        {
            if (currentTurn == ColorPiece.White)
            {
                currentTurn = ColorPiece.Black;
            }
            else
            {
                currentTurn = ColorPiece.White;
            }
            lblTurn.Text = GetCurrentTurn();

           TurnChanged?.Invoke();
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

                    btn.BackgroundImage = piece.PieceImage;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        // hàm hiển thị lượt của ai
        public string GetCurrentTurn()
        {
            return currentTurn == ColorPiece.White ? "Lượt quân trắng" : "Lượt quân đen";
        }

        private string GetOpponentName(ColorPiece color) // hàm chuyển tên thành trắng, đen thay vì black, white
        {
            return color == ColorPiece.White ? "trắng" : "đen";
        }

        private void EndGame()
        {
            foreach(Button btn in Matrix) // khóa bàn cờ
            {
                btn.Enabled = false;
            }

            selectedPiece = null;
        }

        private void MakeAIMove()
        {
            ai = new ChessAI(board);

            Move aiMove = ai.GetBestMove();

            if(aiMove != null)
            {
                board.MovePiece(aiMove.From, aiMove.To);

                DrawPieces(board);
                SwitchTargetTurn();
            }
        }
        #endregion
    }
}
