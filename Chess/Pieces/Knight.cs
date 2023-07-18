namespace Chess.Pieces;

public class Knight: Piece
{
    public Knight(Color color, Board board, Cell cell) : base(color, Type.Knight, board, cell)
    {
    }
    
    public override char symbole()
    {
        return this._Color == Color.White ? 'N' : 'n';
    }
    
    public override float value()
    {
        return this._Color == Color.White ? 1.7f : -1.7f;
    }

    public override List<Cell> getMoves()
    {
        (int currentRow, int currentCol) = (this._Cell._Row, this._Cell._Column);

        if (this._Board._Board[currentRow, currentCol]._Piece == null)
        {
            throw new ArgumentException("Cell empty, invalid row and/or col", nameof(currentRow));
        }
        
        List<Cell> ret = new List<Cell>();
        
        (int row, int col) = (currentRow-1, currentCol-2);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        (row, col) = (currentRow+1, currentCol-2);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        (row, col) = (currentRow-1, currentCol+2);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        (row, col) = (currentRow+1, currentCol+2);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        (row, col) = (currentRow+2, currentCol-1);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        (row, col) = (currentRow+2, currentCol+1);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        (row, col) = (currentRow-2, currentCol-1);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        (row, col) = (currentRow-2, currentCol+1);
        if (-1 < row && row < 8 && -1 < col && col < 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }

        return ret;
    }
}