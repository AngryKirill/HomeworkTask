using System;
using System.Collections.Generic;
using System.Text;

namespace KrestikiNoliki.Model
{
    class Field
    {
        public int[,] Cells { get; set; }

        public Field(int size)
        {
            if (size >= 3 && size % 2 != 0)
            {
                Cells = new int[size, size];
            }
            else if (size > 3 && size % 2 == 0)
            {
                Cells = new int[size + 1, size + 1];
            }
            else
            {
                Cells = new int[3, 3];
            }
        }

        public Field()
        {
            Cells = new int[3, 3];
        }
    }
}
