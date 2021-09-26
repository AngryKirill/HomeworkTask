using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Model
{
    class Triple
    {   
        public static List<Triple> Triples { get; set; }

        public (int, int)[] Points { get; set; }

        public (int, int) Direction { get; set; }

        public int Value { get; set; }

        public Triple((int, int) firstPont, (int, int) secondPoint, (int, int) thirdPoint, (int, int) direction, int value)
        {
            Points = new (int,int)[] { firstPont, secondPoint, thirdPoint };
            Direction = direction;
            Value = value;
        }

        public bool Possible(List<Triple> triples)
        {
            foreach(Triple triple in triples)
            {
                if (Value == triple.Value && Direction == triple.Direction)
                {
                    foreach ((int, int) point in Points)
                    {
                        if (Array.IndexOf(triple.Points, point) != -1)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
