namespace Chess.Pieces;

public class Pawn : Piece
{
    private bool _canBeOnPassant;
    public Pawn(Color color, Board board, Cell cell) : base(color, Type.Pawn, board, cell)
    {
        this._canBeOnPassant = false;
    }

    public bool _CanBeOnPassant
    {
        get => this._canBeOnPassant;
        set => this._canBeOnPassant = value;
    }

    public override char symbole()
    {
        return this._Color == Color.White ? 'P' : 'p';
    }
    
    public override float value()
    {
        return this._Color == Color.White ? 1 : -1;
    }

    public override List<Cell> getMoves()
    {
        (int currentRow, int currentCol) = (this._Cell._Row, this._Cell._Column);

        if (this._Board._Board[currentRow, currentCol]._Piece == null)
        {
            throw new ArgumentException("Cell empty, invalid row and/or col", nameof(currentRow));
        }
        
        List<Cell> ret = new List<Cell>();

        int offset = 1;
        if (this._Color == Color.White) { offset = -1; }
        
        // and curr row isn't 1
        if (currentRow != 0)
        {
            // and there is no piece in front
            if (-1 < currentRow+offset && currentRow+offset < 8 && this._Board._Board[(currentRow+offset), currentCol]._Piece == null)
            {
                // add possible move
                ret.Add(this._Board._Board[(currentRow+offset), currentCol]);

                // if currentRow == 6/1 and there is no piece 2 cells in front
                if ((this._Color == Color.Black && currentRow == 1) || (this._Color == Color.White && currentRow == 6))
                {
                    if (this._Board._Board[(currentRow + (2 * offset)), currentCol]._Piece == null)
                    {
                        // pawn can move 2 squares
                        ret.Add(this._Board._Board[(currentRow + (2 * offset)), currentCol]);
                    }
                }
            }

            // if there is a piece diagonal left
            if (-1 < currentRow+offset && currentRow+offset < 8 && currentCol != 0 && this._Board._Board[(currentRow+offset), (currentCol - 1)]._Piece != null)
            {
                // if it is black
                if (this._Board._Board[(currentRow+offset), (currentCol - 1)]._Piece._Color != this._Color)
                {
                    // add possible move
                    ret.Add(this._Board._Board[(currentRow+offset), (currentCol - 1)]);
                }
            }

            // if there is a piece diagonal right
            if (-1 < currentRow+offset && currentRow+offset < 8 && currentCol != 7 && this._Board._Board[(currentRow+offset), (currentCol + 1)]._Piece != null)
            {
                // if it is black
                if (this._Board._Board[(currentRow+offset), (currentCol + 1)]._Piece._Color != this._Color)
                {
                    // add possible move
                    ret.Add(this._Board._Board[(currentRow+offset), (currentCol + 1)]);
                }
            }
        }
        
        // check if pawn can be taken on passant
        if (currentCol != 0)
        {
            if (this._Board._Board[currentRow, currentCol - 1]._Piece != null)
            {
                if (this._Board._Board[currentRow, currentCol - 1]._Piece is Pawn pawn && pawn._CanBeOnPassant)
                {
                    if (this._Board._Board[currentRow+offset, currentCol - 1]._Piece == null) ret.Add(this._Board._Board[currentRow+offset, currentCol - 1]);
                }
            }
        }
        if (currentCol != 7)
        {
            if (this._Board._Board[currentRow, currentCol + 1]._Piece != null)
            {
                if (this._Board._Board[currentRow, currentCol + 1]._Piece is Pawn pawn && pawn._CanBeOnPassant)
                {
                    if (this._Board._Board[currentRow+offset, currentCol + 1]._Piece == null) ret.Add(this._Board._Board[currentRow+offset, currentCol + 1]);
                }
            }
        }
        
        return ret;
    }
}