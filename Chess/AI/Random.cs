namespace Chess.AI;

public class Random : Player
{
    public Random(Piece.Color color) : base(color)
    {
    }

    public override (Cell, Cell) getMove(Board board)
    {
        List<(Cell, Cell)> movesAllowed = new List<(Cell, Cell)>();
        int listSize = 0;
        foreach (Piece piece in this._Pieces)
        {
            foreach (Cell cell in piece.getMoves())
            {
                movesAllowed.Add((piece._Cell, cell));
                listSize++;
            }
        }
        
        System.Random random = new System.Random();
        // System.Threading.Thread.Sleep(1000);
        return movesAllowed[random.Next(listSize)];
    }
}