using System.Data;
using System.Runtime.CompilerServices;
using Chess.Pieces;

namespace Chess;

public class Board
{
    public enum State // TODO
    {
        ongoing,
        done
    }
    
    private string _fen;
    private Cell[,] _board;
    private Game _game;

    public Board(Game game, string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR")
    {
        this._game = game;
        this._fen = fen;
        this._board = new Cell[8,8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                this._board[i,j] = new Cell(i, j);
            }
        }

        Parser.fen(this, this._fen);
    }

    public Game _Game
    {
        get => this._game;
        set => this._game = value;
    }
    
    public Cell[,] _Board
    {
        get => this._board;
        set => this._board = value;
    }

    public void PrettyPrint(int row = -1, int col = -1, bool showMoves = true)
    {
        List<Cell> movesAllowed = new List<Cell>();
        if (showMoves & (row != -1 && this._board[row, col]._Piece != null))
        {
            movesAllowed = this._board[row, col]._Piece.getMoves();
        }

        Console.WriteLine("  ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗");
    
        for (int i = 0; i < 8; i++)
        {
            Console.Write($"{"87654321"[i]} ║");
    
            for (int j = 0; j < 8; j++)
            {
                Console.ForegroundColor = ConsoleColor.White;
    
                if (this._board[i, j]._Piece != null)
                {
                    Console.ForegroundColor = this._board[i, j]._Piece._Color == Piece.Color.Black ? ConsoleColor.DarkGray: ConsoleColor.Gray;
                    
                    // show if pawn can be taken on passant
                    // if (this._board[i, j]._Piece is Pawn pawn) if (pawn._CanBeOnPassant) Console.ForegroundColor = ConsoleColor.Red;
                }

                if ((i, j) == (row, col)) Console.BackgroundColor = ConsoleColor.Red;
                if (showMoves)
                {
                    foreach (var cell in movesAllowed)
                    {
                        if ((cell._Row, cell._Column) == (i, j)) Console.BackgroundColor = ConsoleColor.Green;
                    }
                }
                Console.Write($" {(char)_board[i, j].Symbole} ");
                Console.ResetColor();
                Console.Write("║");
            }
    
            if (i != 7) Console.WriteLine("\n  ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣");
            else Console.Write("\n");
        }
    
        Console.WriteLine("  ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝");
        Console.WriteLine("    A   B   C   D   E   F   G   H  ");
        Console.WriteLine("----------------------------------");
        // Console.WriteLine(this._game.generateFen());
    }


    public void movePiece(Cell sourceCell, Cell destCell)
    {
        // if the king is eaten
        if (destCell._Piece != null && destCell._Piece._Type == Piece.Type.King)
        {
            this._Game._State = Game.State.Checkmate;
            Console.WriteLine($"{destCell._Piece._Color} lost there king and lost the game.");
        }

        if (sourceCell._Piece is Pawn pawn)
        {
            // if pawn is moving two cells
            if (Math.Abs(sourceCell._Row - destCell._Row) == 2)
            {
                pawn._CanBeOnPassant = true;
            }
            else
            {
                pawn._CanBeOnPassant = false;
            }
            // if pawn reaches last/first row, promote him (queen by default)
            if (destCell._Row == 0 || destCell._Row == 7)
            {
                // remove the pawn from the player's pieces
                sourceCell._Piece._Board._game.getPlayerByColor(sourceCell._Piece._Color).removePiece(sourceCell._Piece);
                
                Queen queen = new Queen(sourceCell._Piece._Color, sourceCell._Piece._Board, sourceCell);
                // add the queen to the the player's pieces
                sourceCell._Piece._Board._game.getPlayerByColor(sourceCell._Piece._Color).addPiece(queen);
                sourceCell._Piece = queen;
            }
        }

        // if you move your king you can no longer castle
        if (sourceCell._Piece._Type == Piece.Type.King)
        {
            // check if move is castle
            switch ((sourceCell._Row, sourceCell._Column), (destCell._Row, destCell._Column))
            {
                case ((0, 4), (0, 6)):
                    movePiece(this._board[0, 7], this._board[0, 5]);
                    break;
                case ((0, 4), (0, 2)):
                    movePiece(this._board[0, 0], this._board[0, 3]);
                    break;
                case ((7, 4), (7, 6)):
                    movePiece(this._board[7, 7], this._board[7, 5]);
                    break;
                case ((7, 4), (7, 2)):
                    movePiece(this._board[7, 0], this._board[7, 3]);
                    break;
                default:
                    break;
            }
            
            
            if (this._game._Player1._Color == sourceCell._Piece._Color)
            {
                this._game._Player1._CastleKingSide = false;
                this._game._Player1._CastleQueenSide = false;
            }
            else
            {
                this._game._Player2._CastleKingSide = false;
                this._game._Player2._CastleQueenSide = false;
            }
        }

        // if you move your rook you can no longer castle
        if (sourceCell._Piece._Type == Piece.Type.Rook)
        {
            if (sourceCell._Row == 0 && sourceCell._Column == 0 && sourceCell._Piece._Color == this._game._Player1._Color) this._game._Player1._CastleQueenSide = false;
            if (sourceCell._Row == 0 && sourceCell._Column == 7 && sourceCell._Piece._Color == this._game._Player1._Color) this._game._Player1._CastleKingSide = false;
            if (sourceCell._Row == 7 && sourceCell._Column == 0 && sourceCell._Piece._Color == this._game._Player2._Color) this._game._Player2._CastleQueenSide = false;
            if (sourceCell._Row == 7 && sourceCell._Column == 7 && sourceCell._Piece._Color == this._game._Player2._Color) this._game._Player2._CastleKingSide = false;
        }

        Piece pieceToMove = sourceCell._Piece;

        if (destCell._Piece != null)
        {
            // if there is a piece in the destCell, it is a capture
            Player player = this._game._Player1;
            if (this._game._Player2._Color == destCell._Piece._Color) player = this._game._Player2;
            player.removePiece(destCell._Piece);
        }

        // Update the piece's cell property after removing it from the source cell
        sourceCell._Piece = null;
        pieceToMove._Cell = destCell;
        destCell._Piece = pieceToMove;
    }
}