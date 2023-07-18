namespace Chess;

public class Player
{
    private Piece.Color _color;
    private List<Piece> _pieces;
    private bool _castleKingSide;
    private bool _castleQueenSide;

    public Player(Piece.Color color)
    {
        this._color = color;
        this._pieces = new List<Piece>();
        this._castleKingSide = true;
        this._castleQueenSide = true;
    }

    public Piece.Color _Color
    {
        get => _color;
        set => this._color = value;
    }

    public List<Piece> _Pieces
    {
        get => this._pieces;
        set => this._pieces = value;
    }
    
    public bool _CastleKingSide
    {
        get => this._castleKingSide;
        set => this._castleKingSide = value;
    }
    
    public bool _CastleQueenSide
    {
        get => this._castleQueenSide;
        set => this._castleQueenSide = value;
    }
    
    public void initPiece(Board board)
    {
        foreach (Cell cell in board._Board)
        {
            Piece? currPiece = cell._Piece;
            if (currPiece != null && currPiece._Color == this._color)
            {
                addPiece(currPiece);
            }
        }
    }

    public void addPiece(Piece piece)
    {
        this._pieces.Add(piece);
    }

    public bool removePiece(Piece piece)
    {
        foreach (Piece p in this._pieces)
        {
            if (p == piece)
            {
                this._pieces.Remove(p);
                return true;
            }
        }

        return false;
    }

    public virtual (Cell, Cell) getMove(Board board)
    {
        Console.WriteLine("What piece do you want to move? <letter><number>");
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Console.Clear();
            return getMove(board);
        }

        int col;
        for (col = 0; col < 8; col++)
        {
            if ("ABCDEFGH"[col] == input[0]) break;
        }
        
        int row;
        for (row = 0; row < 8; row++)
        {
            if ("87654321"[row] == input[1]) break;
        }
        
        if (row > 7 || col > 7) return getMove(board);

        Console.Clear();
        board.PrettyPrint(row, col);
        Console.WriteLine("Where do you want to move? <letter><number>");
        input = Console.ReadLine();

        int col2;
        for (col2 = 0; col2 < 8; col2++)
        {
            if ("ABCDEFGH"[col2] == input[0]) break;
        }
        
        int row2;
        for (row2 = 0; row2 < 8; row2++)
        {
            if ("87654321"[row2] == input[1]) break;
        }

        if (row2 > 7 || col2 > 7) return getMove(board);
        return (board._Board[row, col], board._Board[row2, col2]);
    }

    public bool isInCheck()
    {
        //get the cell of the king
        Cell? kingCell = this._pieces.FirstOrDefault(piece => piece._Type == Piece.Type.King)?._Cell;
        
        // get enemy pieces:
        Player enemy;
        if (this._color == Piece.Color.White) enemy = this._pieces[0]._Board._Game._Player2;
        else enemy = this._pieces[0]._Board._Game._Player1;

        foreach (Piece piece in enemy._pieces)
        {
            foreach (Cell cell in piece.getMoves())
            {
                if (cell == kingCell) return true;
            }
        }

        return false;
    }
}