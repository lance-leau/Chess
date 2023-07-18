using Chess.Pieces;

namespace Chess;

public static class Parser
{
    public static void fen(Board board, string fen)
    {
        int currRow = 0;
        int currCol = 0;
        Cell[,] _board = board._Board;
        
        foreach (char letter in fen)
        {
            switch (letter)
            {
                case 'p':
                    _board[currRow, currCol]._Piece = new Pawn(Piece.Color.Black, board, _board[currRow, currCol]);
                    break;
                case 'n':
                    _board[currRow, currCol]._Piece = new Knight(Piece.Color.Black, board, _board[currRow, currCol]);
                    break;
                case 'b':
                    _board[currRow, currCol]._Piece = new Bishop(Piece.Color.Black, board, _board[currRow, currCol]);
                    break;
                case 'r':
                    _board[currRow, currCol]._Piece = new Rook(Piece.Color.Black, board, _board[currRow, currCol]);
                    break;
                case 'q':
                    _board[currRow, currCol]._Piece = new Queen(Piece.Color.Black, board, _board[currRow, currCol]);
                    break;
                case 'k':
                    _board[currRow, currCol]._Piece = new King(Piece.Color.Black, board, _board[currRow, currCol]);
                    break;
                case 'P':
                    _board[currRow, currCol]._Piece = new Pawn(Piece.Color.White, board, _board[currRow, currCol]);
                    break;
                case 'N':
                    _board[currRow, currCol]._Piece = new Knight(Piece.Color.White, board, _board[currRow, currCol]);
                    break;
                case 'B':
                    _board[currRow, currCol]._Piece = new Bishop(Piece.Color.White, board, _board[currRow, currCol]);
                    break;
                case 'R':
                    _board[currRow, currCol]._Piece = new Rook(Piece.Color.White, board, _board[currRow, currCol]);
                    break;
                case 'Q':
                    _board[currRow, currCol]._Piece = new Queen(Piece.Color.White, board, _board[currRow, currCol]);
                    break;
                case 'K':
                    _board[currRow, currCol]._Piece = new King(Piece.Color.White, board, _board[currRow, currCol]);
                    break;
                case '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8':
                    for (int i = 0; i < letter - '0'-1; i++) { currCol++; }
                    break;
                case '/':
                    break;
            }

            if (currCol == 8) (currCol, currRow) = (0, currRow + 1);
            else currCol++;
        }
    }
}