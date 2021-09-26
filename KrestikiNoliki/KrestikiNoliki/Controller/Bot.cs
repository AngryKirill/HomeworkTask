using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Controller
{
    class Bot
    {
        public int EnemyValue { get; set; }

        public Bot(int enemyValue)
        {
            EnemyValue = enemyValue;
        }

        public (int, int) Turn(int x, int y, int[,] cells)
        {
            int length = cells.GetLength(0);
            int outX;
            int outY;
            Random random = new Random();

            if (x != -1 && y != -1)
            {
                if (x - 1 >= 0 && y - 1 >= 0 &&
                    cells[x - 1, y - 1] == EnemyValue)
                {
                    if (x + 1 < length && y + 1 < length &&
                        cells[x + 1, y + 1] == 0)
                    {
                        return (x + 1, y + 1);
                    }
                    else if (x - 2 >= 0 && y - 2 >= 0 &&
                            cells[x - 2, y - 2] == 0)
                    {
                        return (x - 2, y - 2);
                    }
                }

                if (y - 1 >= 0 && cells[x, y - 1] == EnemyValue)
                {
                    if (y + 1 < length && cells[x, y + 1] == 0)
                    {
                        return (x, y + 1);
                    }
                    else if (y - 2 >= 0 && cells[x, y - 2] == 0)
                    {
                        return (x, y - 2);
                    }
                }

                if (x + 1 < length && y - 1 >= 0 &&
                    cells[x + 1, y - 1] == EnemyValue)
                {
                    if (x - 1 >= 0 && y + 1 < length &&
                        cells[x - 1, y + 1] == 0)
                    {
                        return (x - 1, y + 1);
                    }
                    else if (x + 2 < length && y - 2 >= 0 &&
                        cells[x + 2, y - 2] == 0)
                    {
                        return (x + 2, y - 2);
                    }
                }

                if (x - 1 >= 0 && cells[x - 1, y] == EnemyValue)
                {
                    if (x + 1 < length && cells[x + 1, y] == 0)
                    {
                        return (x + 1, y);
                    }
                    else if (x - 2 >= 0 && cells[x - 2, y] == 0)
                    {
                        return (x - 2, y);
                    }
                }

                if (x + 1 < length && cells[x + 1, y] == EnemyValue)
                {
                    if (x - 1 >= 0 && cells[x - 1, y] == 0)
                    {
                        return (x - 1, y);
                    }
                    else if (x + 2 < length && cells[x + 2, y] == 0)
                    {
                        return (x + 2, y);
                    }
                }

                if (x - 1 >= 0 && y + 1 < length &&
                    cells[x - 1, y + 1] == EnemyValue)
                {
                    if (x + 1 < length && y - 1 >= 0 &&
                        cells[x + 1, y - 1] == 0)
                    {
                        return (x + 1, y - 1);
                    }
                    else if (x - 2 >= 0 && y + 2 < length &&
                        cells[x - 2, y + 2] == 0)
                    {
                        return (x - 2, y + 2);
                    }
                }

                if (y + 1 < length && cells[x, y + 1] == EnemyValue)
                {
                    if (y - 1 >= 0 && cells[x, y - 1] == 0)
                    {
                        return (x, y - 1);
                    }
                    else if (y + 2 < length && cells[x, y + 2] == 0)
                    {
                        return (x, y + 2);
                    }
                }

                if (x + 1 < length && y + 1 < length &&
                    cells[x + 1, y + 1] == EnemyValue)
                {
                    if (x - 1 >= 0 && y - 1 >= 0 && cells[x - 1, y - 1] == 0)
                    {
                        return (x - 1, y - 1);
                    }
                    else if (x + 2 < length && y + 2 < length &&
                        cells[x + 2, y + 2] == EnemyValue)
                    {
                        return (x + 2, y + 2);
                    }
                }

                if ((x - 1 >= 0 && y - 1 >= 0 && cells[x - 1, y - 1] == 0) ||
                    (y - 1 >= 0 && cells[x, y - 1] == 0) ||
                    (x + 1 < length && y - 1 >= 0 && cells[x + 1, y - 1] == 0) ||
                    (x - 1 >= 0 && cells[x - 1, y] == 0) ||
                    (x + 1 < length && cells[x + 1, y] == 0) ||
                    (x - 1 >= 0 && y + 1 < length && cells[x - 1, y + 1] == 0) ||
                    (y + 1 < length && cells[x, y + 1] == 0) ||
                    (x + 1 < length && y + 1 < length && cells[x + 1, y + 1] == 0))
                {
                    do
                    {
                        outX = random.Next(0, length);
                        outY = random.Next(0, length);
                    }
                    while (cells[outX, outY] != 0);

                    return (outX, outY);
                }
            }

            do
            {
                outX = random.Next(0, length);
                outY = random.Next(0, length);
            }
            while (cells[outX, outY] != 0);

            return (outX, outY);
        }
    }
}
