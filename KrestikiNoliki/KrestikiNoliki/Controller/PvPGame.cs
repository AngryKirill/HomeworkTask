using KrestikiNoliki.Model;
using KrestikiNoliki.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Controller
{
    static class PvPGame
    {
        public static void NextTurn(Game game)
        {
            Console.Clear();
            int x;
            int y;
            Output.FieldOutput(game.Field);
            do
            {
                Output.MessageOutput("Введите координату Х");
                x = Input.XInput(game.Field.Cells.GetLength(0));

                Output.MessageOutput("Введите координату Y");
                y = Input.XInput(game.Field.Cells.GetLength(0));
            } 
            while (game.Field.Cells[x, y] == 1 || game.Field.Cells[x, y] == 2);

            if (game.FirstPlayerTurn)
            {
                game.Field.Cells[x, y] = 1;
            }
            else
            {
                game.Field.Cells[x, y] = 2;
            }

            if (!Check.EndGameCheck(game.Field, out int crossTripleCount, out int circleTripleCount))
            {
                game.FirstPlayerTurn = !game.FirstPlayerTurn;
                NextTurn(game);
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
