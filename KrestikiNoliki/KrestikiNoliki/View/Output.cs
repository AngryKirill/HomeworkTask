using KrestikiNoliki.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.View
{
    static class Output
    {
        public static void FieldOutput(Field field)
        {
            Console.Write("1");

            for (int index = 2; index <= field.Cells.GetLength(0); index++)
            {
                Console.Write("{0,2}", index);
            }

            Console.WriteLine();

            for(int index1=0;index1< field.Cells.GetLength(0); index1++)
            {
                for(int index2=0;index2< field.Cells.GetLength(0); index2++)
                {
                    switch (field.Cells[index2, index1])
                    {
                        case 1:
                            Console.Write("x ");
                            break;
                        case 2:
                            Console.Write("o ");
                            break;
                        default:
                            Console.Write("  ");
                            break;
                    }
                }
                Console.WriteLine(index1);
            }
            for (int index = 0; index < field.Cells.GetLength(0); index++)
            {
                Console.Write("- ");
            }

            Console.WriteLine();
        }

        public static void MessageOutput(string message)
        {
            Console.WriteLine(message);
        }

        public static void WinnerOutput(int crossCount, int circleCount)
        {
            if (crossCount > circleCount)
            {
                MessageOutput("Победили крестики!!!");
            }
            else if(crossCount < circleCount)
            {
                MessageOutput("Победили нолики!!!");
            }
            else
            {
                MessageOutput("Ничья!!!");
            }

            MessageOutput("Количество троек крестиков: " + crossCount +
                "\nКоличество троек ноликов: " + circleCount);
        }
    }
}
