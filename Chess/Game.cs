using System.Runtime.CompilerServices;

namespace Chess;

public class Game
{
    public enum State
    {
        Ongoing,
        Checkmate,
        Stalemate,
        Draw,
        Repetirion,
        FiftyMove,
        InsufficientMaterial,
        TimeOut,
        Resignation
    }
    
    private Board _board;
    private Player _player1;
    private Player _player2;
    private Player _currPlayer;
    private State _state;

    public Game(Board? board = null, Player? player1 = null, Player? player2 = null)
    {
        this._board = board ?? new Board(this);
        this._player1 = player1 ?? new Player(Piece.Color.White);
        this._player2 = player2 ?? new Player(Piece.Color.Black);
        
        this._player1.initPiece(this._board);
        this._player2.initPiece(this._board);

        this._currPlayer = this._player1;
        this._state = State.Ongoing;
    }

    public Board _Board
    {
        get => this._board;
        set
        {
            this._board = value;
            this._player1._Pieces = new List<Piece>();
            this._player2._Pieces = new List<Piece>();
            this._player1.initPiece(this._board);
            this._player2.initPiece(this._board);

        }
            
    }
    
    public Player _Player1
    {
        get => this._player1;
        set => this._player1 = value;
    }
    
    public Player _Player2
    {
        get => this._player2;
        set => this._player2 = value;
    }
    
    public State _State
    {
        get => this._state;
        set => this._state = value;
    }

    public string generateFen()
    {
        string ret = "";
        for (int i = 0; i < 8; i++)
        {
            int nbrEmpty = 0;
            for (int j = 0; j < 8; j++)
            {
                if (this._board._Board[i, j]._Piece != null)
                {
                    if (nbrEmpty != 0)
                    {
                        ret += Convert.ToChar(nbrEmpty.ToString());
                        nbrEmpty = 0;
                    }
                    ret += Convert.ToChar(this._board._Board[i, j].Symbole);
                }
                else
                {
                    nbrEmpty++;
                }
            }
            if (nbrEmpty != 0)
            {
                ret += Convert.ToChar(nbrEmpty.ToString());
            }
            if (i != 7)
            {
                ret += '/';
            }
        }

        return ret;
    }
}