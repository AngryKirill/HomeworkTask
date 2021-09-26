using KrestikiNoliki.Model;
using KrestikiNoliki.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Controller
{
    static class PvEGame
    {
        public static void NextTurn(Game game, int x, int y)
        {
            Console.Clear();
            Output.FieldOutput(game.Field);

            if ((game.FirstPlayerTurn && game.PlayerIsFirst) || (!game.FirstPlayerTurn && !game.PlayerIsFirst))
            {
                do
                {
                    Output.MessageOutput("Введите координату Х");
                    x = Input.XInput(game.Field.Cells.GetLength(0));

                    Output.MessageOutput("Введите координату Y");
                    y = Input.XInput(game.Field.Cells.GetLength(0));
                }
                while (game.Field.Cells[x, y] == 1 || game.Field.Cells[x, y] == 2);

                if (game.PlayerIsCross)
                {
                    game.Field.Cells[x, y] = 1;
                }
                else
                {
                    game.Field.Cells[x, y] = 2;
                }
            }
            else
            {
                (int, int) point = game.Bot.Turn(x, y, game.Field.Cells);
                if (game.PlayerIsCross)
                {
                    game.Field.Cells[point.Item1, point.Item2] = 2;
                }
                else
                {
                    game.Field.Cells[point.Item1, point.Item2] = 1;
                }
                x = point.Item1;
                y = point.Item2;
            }

            if (!Check.EndGameCheck(game.Field, out int crossTripleCount, out int circleTripleCount))
            {
                game.FirstPlayerTurn = !game.FirstPlayerTurn;
                NextTurn(game, x, y);
            }
            else
            {
                Console.Clear();

                Output.FieldOutput(game.Field);

                Output.WinnerOutput(crossTripleCount, circleTripleCount);

                Menu.Start();
            }
        }
    }
}
