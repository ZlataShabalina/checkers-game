using System;

namespace CheckersGame
{
    public class Board
    {
        private const int BoardSize = 8;
        private Piece[,] board;

        public Board()
        {
            board = new Piece[BoardSize, BoardSize];
        }

        public void InitializeBoard(Player player1, Player player2)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        if (row < 3)
                            board[row, col] = new Pawn(player2.Icon);
                        else if (row > 4)
                            board[row, col] = new Pawn(player1.Icon);
                        else
                            board[row, col] = null;
                    }
                    else
                    {
                        board[row, col] = null;
                    }
                }
            }
        }

        public void PrintBoard()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                Console.Write($"{row} ");
                for (int col = 0; col < BoardSize; col++)
                {
                    Console.Write(board[row, col]?.ToString() ?? "  ");
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            for (int col = 0; col < BoardSize; col++)
            {
                Console.Write($"{col} ");
            }
            Console.WriteLine();
        }

        public bool GameOver(out string winner)
        {
            bool player1HasPieces = false;
            bool player2HasPieces = false;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (board[row, col] != null)
                    {
                        if (board[row, col].Icon == 'x')
                            player1HasPieces = true;
                        else if (board[row, col].Icon == 'o')
                            player2HasPieces = true;
                    }
                }
            }

            if (!player1HasPieces)
            {
                winner = "Player 2 Wins!";
                return true;
            }

            if (!player2HasPieces)
            {
                winner = "Player 1 Wins!";
                return true;
            }

            winner = null;
            return false;
        }

        public bool MakeMove(string input, Player currentPlayer)
        {
            var parts = input.Split(" to ");
            if (parts.Length != 2) return false;

            if (!ParsePosiion(parts[0], out int fromRow, out int fromCol)) return false;
            if (!ParsePosiion(parts[1], out int toRow, out int toCol)) return false;

            Piece piece = board[fromRow, fromCol];
            if (piece == null || piece.Icon != currentPlayer.Icon) return false;

            if (IsValidMove(fromRow, fromCol, toRow, toCol))
            {
                int rowDiff = Math.Abs(toRow - fromRow);

                
                if (rowDiff == 2)
                {
                    int midRow = (fromRow + toRow) / 2;
                    int midCol = (fromCol + toCol) / 2;
                    board[midRow, midCol] = null; 
                }

                board[toRow, toCol] = piece;  
                board[fromRow, fromCol] = null; 

                piece.CheckPromotion(toRow, toCol);  

                return true;
            }

            return false;
        }

        private bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            if (fromRow == toRow && fromCol == toCol)
                return false;

            if ((toRow + toCol) % 2 == 0)
                return false;

            int rowDiff = Math.Abs(toRow - fromRow);
            int colDiff = Math.Abs(toCol - fromCol);

            if (rowDiff == 1 && colDiff == 1)
            {
                return board[toRow, toCol] == null;
            }

            if (rowDiff == 2 && colDiff == 2)
            {
                int midRow = (fromRow + toRow) / 2;
                int midCol = (fromCol + toCol) / 2;

                if (board[midRow, midCol] != null && board[midRow, midCol].Icon != board[fromRow, fromCol].Icon)
                {
                    return board[toRow, toCol] == null;
                }
            }

            return false;
        }


        private bool ParsePosiion(string pos, out int row, out int col)
        {
            var parts = pos.Split(',');
            if (parts.Length != 2 || !int.TryParse(parts[0], out row) || !int.TryParse(parts[1], out col))
            {
                row = col = -1;
                return false;
            }
            return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
        }
    }
}
