using KrestikiNoliki.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Controller
{
    static class Check
    {
        public static bool EndGameCheck(Field field, out int outCrossTripleCount, out int outCircleTripleCount)
        {
            PointsTripleCheck(field.Cells, out int crossTripleCount, out int circleTripleCount);

            outCrossTripleCount = crossTripleCount;
            outCircleTripleCount = circleTripleCount;

            if (crossTripleCount > circleTripleCount)
            {
                if (circleTripleCount + PossibleTriplesCheck(field.Cells, 2) < crossTripleCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (crossTripleCount < circleTripleCount)
            {
                if (crossTripleCount + PossibleTriplesCheck(field.Cells, 1) < circleTripleCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static int PossibleTriplesCheck(int[,] cells, int value)
        {
            List<Triple> possibleTriples = new List<Triple>();
            int outputValue = 0;

            for (int index1 = 0; index1 < cells.GetLength(0); index1++)
            {
                for (int index2 = 0; index2 < cells.GetLength(0); index2++)
                {
                    if ((cells[index2, index1] == 0 || cells[index2, index1] == value) &&
                        TriplePointCheck((index2, index1), cells, value, possibleTriples))
                    {
                        outputValue++;
                    }
                }
            }

            return outputValue;
        }

        public static bool TriplePointCheck((int, int) point, int[,] cells, int value, List<Triple> triples)
        {
            if (TripleCheckInDirection(point, 1, 0, cells, value, triples))
            {
                return true;
            }
            else if (TripleCheckInDirection(point, 0, 1, cells, value, triples))
            {
                return true;
            }
            else if (TripleCheckInDirection(point, 1, 1, cells, value, triples))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TripleCheckInDirection((int, int) point, int dirX, int dirY, int[,] cells, int value, List<Triple> triples)
        {
            int length = cells.GetLength(0) - 1;
            int x = point.Item1;
            int y = point.Item2;

            if (x + dirX >= 0 && x + dirX <= length &&
                y + dirY >= 0 && y + dirY <= length)
            {
                if (cells[x + dirX, y + dirY] == 0 || cells[x + dirX, y + dirY] == value)
                {
                    Triple triple1 = new Triple((x, y), (x + dirX, y + dirY), (x - dirX, y - dirY), (dirX, dirY), value);
                    Triple triple2 = new Triple((x, y), (x + dirX, y + dirY), (x + 2 * dirX, y + 2 * dirY), (dirX, dirY), value);

                    if (x - dirX >= 0 && x - dirX <= length &&
                        y - dirY >= 0 && y - dirY <= length &&
                        (cells[x - dirX, y - dirY] == 0 || cells[x - dirX, y - dirY] == value) &&
                        triple1.Possible(triples))
                    {
                        triples.Add(triple1);
                        return true;
                    }
                    else if (x + 2 * dirX >= 0 && x + 2 * dirX <= length &&
                             y + 2 * dirY >= 0 && y + 2 * dirY <= length &&
                            (cells[x + 2 * dirX, y + 2 * dirY] == 0 || cells[x + 2 * dirX, y + 2 * dirY] == value) &&
                            triple2.Possible(triples))
                    {
                        triples.Add(triple2);
                        return true;
                    }
                }
            }

            else if(x - dirX >= 0 && x - dirX <= length &&
                    y - dirY >= 0 && y - dirY <= length &&
                    (cells[x - dirX, y - dirY] == 0 || cells[x - dirX, y - dirY] == value) &&
                    x - 2 * dirX >= 0 && x - 2 * dirX <= length &&
                    y - 2 * dirY >= 0 && y - 2 * dirY <= length &&
                    (cells[x - 2 * dirX, y - 2 * dirY] == value || cells[x - 2 * dirX, y - 2 * dirY] == 0))
            {
                Triple triple = new Triple((x, y), (x - dirX, y - dirY), (x - 2 * dirX, y - 2 * dirY), (dirX, dirY), value);
                if (triple.Possible(triples))
                {
                    triples.Add(triple);
                    return true;
                }
            }

            return false;
        }
        
        public static void PointsTripleCheck(int[,] cells, out int crossTripleCount, out int circleTripleCount)
        {
            List<Triple> triples = new List<Triple>();
            crossTripleCount = 0;
            circleTripleCount = 0;
            for(int index1 = 0; index1 < cells.GetLength(0); index1++)
            {
                for(int index2 = 0; index2 < cells.GetLength(0); index2++)
                {
                    if (cells[index2, index1] == 1)
                    {
                        PointTripleCheck(index2, index1, cells, 1, triples, ref crossTripleCount);
                    }
                    else if(cells[index2, index1] == 2)
                    {
                        PointTripleCheck(index2, index1, cells, 2, triples, ref circleTripleCount);
                    }
                }
            }
        }
        
        public static void PointTripleCheck(int x, int y, int[,] cells, int value, List<Triple> triples, ref int counter)
        {
            int length = cells.GetLength(0);
            if(x + 1 < length && x + 2 < length && cells[x + 1, y] == value && cells[x + 2, y] == value)
            {
                Triple triple = new Triple((x, y), (x + 1, y), (x + 2, y), (1, 0), value);
                if (triple.Possible(triples))
                {
                    triples.Add(triple);
                    counter++;
                }
            }

            if (y + 1 < length && y + 2 < length && cells[x, y + 1] == value && cells[x, y + 2] == value)
            {
                Triple triple = new Triple((x, y), (x, y + 1), (x, y + 2), (0, 1), value);
                if (triple.Possible(triples))
                {
                    triples.Add(triple);
                    counter++;
                }
            }

            if (y + 1 < length && y + 2 < length &&
                x - 1 > 0 && x - 2 >= 0 &&
                cells[x - 1, y + 1] == value && cells[x - 2, y + 2] == value)
            {
                Triple triple = new Triple((x, y), (x - 1, y + 1), (x - 2, y + 2), (-1, 1), value);
                if (triple.Possible(triples))
                {
                    triples.Add(triple);
                    counter++;
                }
            }

            if (y + 1 < length && y + 2 < length &&
                x + 1 < length && x + 2 < length &&
                cells[x + 1, y + 1] == value && cells[x + 2, y + 2] == value)
            {
                Triple triple = new Triple((x, y), (x + 1, y + 1), (x + 2, y + 2), (1, 1), value);
                if (triple.Possible(triples))
                {
                    triples.Add(triple);
                    counter++;
                }
            }
        }
    }
}
