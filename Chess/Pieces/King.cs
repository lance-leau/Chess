namespace Chess.Pieces;

public class King: Piece
{
    public King(Color color, Board board, Cell cell) : base(color, Type.King, board, cell)
    {
    }
    
    public override char symbole()
    {
        return this._Color == Color.White ? 'K' : 'k';
    }
    
    public override float value()
    {
        return this._Color == Color.White ? Int32.MaxValue : Int32.MinValue;
    }

    public override List<Cell> getMoves()
    {
        (int currentRow, int currentCol) = (this._Cell._Row, this._Cell._Column);

        if (this._Board._Board[currentRow, currentCol]._Piece == null)
        {
            throw new ArgumentException("Cell empty, invalid row and/or col", nameof(currentRow));
        }
        
        List<Cell> ret = new List<Cell>();
        
        // add castles
        if (this._Color == Color.Black && this._Board._Game._Player2._CastleKingSide)
        {
            if (_Board._Board[0, 5]._Piece == null && _Board._Board[0, 6]._Piece == null) ret.Add(this._Board._Board[0, 6]);
        }
        
        if (this._Color == Color.Black && this._Board._Game._Player2._CastleKingSide)
        {
            if (_Board._Board[0, 1]._Piece == null && _Board._Board[0, 2]._Piece == null && _Board._Board[0, 3]._Piece == null) ret.Add(this._Board._Board[0, 2]);
        }
        if (this._Color == Color.White && this._Board._Game._Player1._CastleKingSide)
        {
            if (_Board._Board[7, 5]._Piece == null && _Board._Board[7, 6]._Piece == null) ret.Add(this._Board._Board[7, 6]);
        }
        
        if (this._Color == Color.White && this._Board._Game._Player1._CastleKingSide)
        {
            if (_Board._Board[7, 1]._Piece == null && _Board._Board[7, 2]._Piece == null && _Board._Board[7, 3]._Piece == null) ret.Add(this._Board._Board[7, 2]);
        }

        // moves left --------------------------------------------------------------------------------------------------
        (int row, int col) = (currentRow, currentCol-1);
        if (col != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        // moves down --------------------------------------------------------------------------------------------------
        (row, col) = (currentRow+1, currentCol);
        if (row != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }

        // moves right -------------------------------------------------------------------------------------------------
        (row, col) = (currentRow, currentCol+1);
        if (col != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        // moves up ----------------------------------------------------------------------------------------------------
        (row, col) = (currentRow-1, currentCol);
        if (row != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }

        // moves up left -----------------------------------------------------------------------------------------------
        (row, col) = (currentRow-1, currentCol-1);
        if (col != -1 && row != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        // moves down left ---------------------------------------------------------------------------------------------
        (row, col) = (currentRow+1, currentCol-1);
        if (row != 8 && col != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        // moves down right --------------------------------------------------------------------------------------------
        (row, col) = (currentRow+1, currentCol+1);
        if (col != 8 && row != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }
        
        // moves up right ----------------------------------------------------------------------------------------------
        (row, col) = (currentRow-1, currentCol+1);
        if (row != -1 && col != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
        }

        return ret;
    }
}