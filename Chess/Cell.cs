namespace Chess;

public class Cell
{
    private int _row;
    private int _column;
    private Piece? _piece;

    public Cell(int row, int column)
    {
        this._row = row;
        this._column = column;
        this._piece = null;
    }

    public Piece? _Piece
    {
        get => this._piece;
        set => this._piece = value;
    }

    public int _Row
    {
        get => this._row;
        set => this._row = value;
    }
    
    public int _Column
    {
        get => this._column;
        set => this._column = value;
    }

    public int Symbole
    {
        get
        {
            if (this._piece != null) return this._piece.symbole();
            return ' ';
        }
    }
}