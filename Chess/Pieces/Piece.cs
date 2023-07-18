using Chess.Pieces;

namespace Chess;

public class Piece
{
    public enum Color
    {
        White,
        Black
    }
    public enum Type
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    private Color _color;
    private Type _type;
    private Cell _cell;
    private Board _board;

    public Piece(Color color, Type type, Board board, Cell cell)
    {
        this._color = color;
        this._type = type;
        this._cell = cell;
        this._board = board;
    }

    public Color _Color
    {
        get => this._color;
        set => this._color = value;
    }

    public Type _Type
    {
        get => this._type;
        set => this._type = value;
    }
    
    public Cell _Cell
    {
        get => this._cell;
        set => this._cell = value;
    }
    
    public Board _Board
    {
        get => this._board;
        set => this._board = value;
    }

    public virtual char symbole() { return '·'; }
    public virtual float value() { return 0; }

    public virtual List<Cell> getMoves()
    {
        return new List<Cell>();
    }
}