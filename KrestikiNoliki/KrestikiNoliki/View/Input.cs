using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.View
{
    static class Input
    {
        public static string InputWithMessage(string message)
        {
            Output.MessageOutput(message);
            return Console.ReadLine();
        }
        public static string InputString()
        {
            return Console.ReadLine();
        }
        public static int StartMenuInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                default:
                    Output.MessageOutput("Неправильный ввод");
                    StartMenuInput();
                    return 0;
            }
        }

        public static int SizeChooseInput()
        {
            try
            {
                int size = Convert.ToInt32(Input.InputString());
                if (size < 3 || size % 2 != 1)
                {
                    Output.MessageOutput("Неправильный ввод");
                    SizeChooseInput();
                    return 0;
                }
                else
                {
                    return size;
                }
            }
            catch
            {
                Output.MessageOutput("Неправильный ввод");
                SizeChooseInput();
                return 0;
            }
        }
        public static int NewGameInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "3":
                    return 3;
                default:
                    Output.MessageOutput("Неправильный ввод");
                    NewGameInput();
                    return 0;
            }
        }

        public static int XInput(int length)
        {
            try
            {
                int x = Convert.ToInt32(Console.ReadLine());
                if (x < 1 || x > length)
                {
                    Output.MessageOutput("Неправильный ввод");
                    XInput(length);
                    return 0;
                }
                else
                {
                    return x - 1;
                }
            }
            catch
            {
                Output.MessageOutput("Неправильный ввод");
                XInput(length);
                return 0;
            }
        }
        public static int YInput(int length)
        {
            try
            {
                int y = Convert.ToInt32(Console.ReadLine());
                if (y < 1 || y > length)
                {
                    Output.MessageOutput("Неправильный ввод");
                    YInput(length);
                    return 0;
                }
                else
                {
                    return y - 1;
                }
            }
            catch
            {
                Output.MessageOutput("Неправильный ввод");
                YInput(length);
                return 0;
            }
        }

        public static bool FigureChooseInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    return true;
                case "2":
                    return false;
                default:
                    Random random = new Random();
                    bool playerIsCross = Convert.ToBoolean(random.Next(0, 2));
                    if (playerIsCross)
                    {
                        Output.MessageOutput("Игрок крестик");
                    }
                    else
                    {
                        Output.MessageOutput("Игрок нолик");
                    }
                    return playerIsCross;
            }
        }

        public static bool WhoIsFirstChooseInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    return true;
                case "2":
                    return false;
                default:
                    Random random = new Random();
                    bool playerIsFirst = Convert.ToBoolean(random.Next(0, 2));
                    if (playerIsFirst)
                    {
                        Output.MessageOutput("Игрок крестик");
                    }
                    else
                    {
                        Output.MessageOutput("Игрок нолик");
                    }
                    return playerIsFirst;
            }
        }
    }
}
