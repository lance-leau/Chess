using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player_1 = new Player(Piece.Color.White);
            Player player_2 = new AI.Random(Piece.Color.Black);

            Game game = new Game(player1:player_1, player2:player_2);

            Player currPlayer = game._Player1;

            Console.Clear();
            game._Board.PrettyPrint();
            
            while (game._State == Game.State.Ongoing)
            {
                (Cell start, Cell end) = currPlayer.getMove(game._Board);
                game._Board.movePiece(start, end);
                if (currPlayer == game._Player1) currPlayer = game._Player2;
                else currPlayer = game._Player1;
                Console.Clear();
                game._Board.PrettyPrint(end._Row, end._Column, false);
            }
        }
    }
}