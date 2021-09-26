using KrestikiNoliki.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Model
{
    class Game
    {
        public Field Field { get; set; }

        public bool VersusBot { get; set; }

        public bool PlayerIsFirst { get; set; }

        public bool PlayerIsCross { get; set; }

        public bool FirstPlayerTurn { get; set; }

        public Bot Bot { get; set; }

        public Game()
        {
            Field = new Field();
            VersusBot = true;
            PlayerIsFirst = true;
            PlayerIsCross = true;
            FirstPlayerTurn = true;
        }

        public Game(Field field, bool versusBot, bool playerIsFirst, bool playerIsCross, bool firstPlayerTurn)
        {
            Field = field;
            VersusBot = versusBot;
            PlayerIsFirst = playerIsFirst;
            PlayerIsCross = playerIsCross;
            FirstPlayerTurn = firstPlayerTurn;
        }

        public Game(Field field, bool versusBot, bool playerIsFirst, bool playerIsCross, bool firstPlayerTurn, Bot bot)
        {
            Field = field;
            VersusBot = versusBot;
            PlayerIsFirst = playerIsFirst;
            PlayerIsCross = playerIsCross;
            FirstPlayerTurn = firstPlayerTurn;
            Bot = bot;
        }
    }
}
