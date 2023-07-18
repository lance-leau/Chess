namespace Chess.Pieces;

public class Queen: Piece
{
    public Queen(Color color, Board board, Cell cell) : base(color, Type.Queen, board, cell)
    {
    }
    
    public override char symbole()
    {
        return this._Color == Color.White ? 'Q' : 'q';
    }
    
    public override float value()
    {
        return this._Color == Color.White ? 9 : -9;
    }

    public override List<Cell> getMoves()
    {
        (int currentRow, int currentCol) = (this._Cell._Row, this._Cell._Column);

        if (this._Board._Board[currentRow, currentCol]._Piece == null)
        {
            throw new ArgumentException("Cell empty, invalid row and/or col", nameof(currentRow));
        }
        
        List<Cell> ret = new List<Cell>();

        // moves left --------------------------------------------------------------------------------------------------
        (int row, int col) = (currentRow, currentCol-1);
        while (col != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            col--;
        }
        // if piece is enemy, add move
        if (col != -1 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);
        
        // moves down --------------------------------------------------------------------------------------------------
        (row, col) = (currentRow+1, currentCol);
        while (row != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            row++;
        }
        // if piece is enemy, add move
        if (row != 8 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);
        
        // moves right -------------------------------------------------------------------------------------------------
        (row, col) = (currentRow, currentCol+1);
        while (col != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            col++;
        }
        // if piece is enemy, add move
        if (col != 8 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);
        
        // moves up ----------------------------------------------------------------------------------------------------
        (row, col) = (currentRow-1, currentCol);
        while (row != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            row--;
        }
        // if piece is enemy, add move
        if (row != -1 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);

        // moves up left -----------------------------------------------------------------------------------------------
        (row, col) = (currentRow-1, currentCol-1);
        while (col != -1 && row != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            col--;
            row--;
        }
        // if piece is enemy, add move
        if (col != -1 && row != -1 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);
        
        // moves down left ---------------------------------------------------------------------------------------------
        (row, col) = (currentRow+1, currentCol-1);
        while (row != 8 && col != -1 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            row++;
            col--;
        }
        // if piece is enemy, add move
        if (row != 8 && col != -1 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);
        
        // moves down right --------------------------------------------------------------------------------------------
        (row, col) = (currentRow+1, currentCol+1);
        while (col != 8 && row != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            col++;
            row++;
        }
        // if piece is enemy, add move
        if (col != 8 && row != 8 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);
        
        // moves up right ----------------------------------------------------------------------------------------------
        (row, col) = (currentRow-1, currentCol+1);
        while (row != -1 && col != 8 && this._Board._Board[row, col]._Piece == null)
        {
            ret.Add(this._Board._Board[row, col]);
            row--;
            col++;
        }
        // if piece is enemy, add move
        if (row != -1 && col != 8 && this._Board._Board[row, col]._Piece._Color != this._Color) ret.Add(this._Board._Board[row, col]);

        return ret;
    }
}