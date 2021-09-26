using KrestikiNoliki.Model;
using KrestikiNoliki.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Controller
{
    static class Menu
    {
        private static Field field;
        private static Game game;
        public static void Start()
        {
            Output.MessageOutput("Выберите пункт меню" +
                "\n 1) Начать новую игру" +
                "\n 2) Выход");

            if (Input.StartMenuInput() == 1)
            {
                SizeChoose();
            }
        }

        public static void NewGame(int size)
        {
            Output.MessageOutput("Выберите вариант игры" +
                "\n 1) Против бота" +
                "\n 2) Против другого игрока" +
                "\n 3) Назад");

            switch (Input.NewGameInput())
            {
                case 1:
                    FigureChoose(size);
                    break;
                case 2:
                    PVPGameStart(size);
                    break;
                default:
                    Start();
                    break;
            }
        }

        public static void SizeChoose()
        {
            Output.MessageOutput("Введите размер поля (нечётное и не менее трёх)");
            NewGame(Input.SizeChooseInput());
        }

        public static void PVPGameStart(int size)
        {
            field = new Field(size);
            game = new Game(field, false, true, true, true);
            PvPGame.NextTurn(game);
        }

        public static void FigureChoose(int size)
        {
            Output.MessageOutput("Выберите фигуру" +
                "\n 1) Крестик" +
                "\n 2) Нолик" +
                "\n 3) Рандом");
            FirstPlayerChoose(size, Input.FigureChooseInput());
        }

        public static void FirstPlayerChoose(int size, bool playerIsCross)
        {
            Output.MessageOutput("Выберите, кто ходит первым" +
                "\n 1) Игрок" +
                "\n 2) Бот" +
                "\n 3) Рандом");
            PVEGameStart(size, playerIsCross, Input.WhoIsFirstChooseInput());
        }
        
        public static void PVEGameStart(int size, bool playerIsCross, bool playerIsFirst)
        {
            field = new Field(size);
            int enemyValue;

            if (playerIsCross)
            {
                enemyValue = 1;
            }
            else
            {
                enemyValue = 2;
            }

            game = new Game(field, true, playerIsFirst, playerIsCross, true, new Bot(enemyValue));
            PvEGame.NextTurn(game, -1, -1);
        }
    }
}
