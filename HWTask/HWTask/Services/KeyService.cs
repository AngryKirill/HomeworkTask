using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Services
{
    public class KeyService
    {
        public string GetRandomKey(int keyLength)
        {
            const string rusAlphabetUp = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            Random rndGen = new Random();


            char[] letters = rusAlphabetUp.ToCharArray();
            string str = "";
            for (int i = 0; i < keyLength; i++)
            {
                str += letters[rndGen.Next(letters.Length)].ToString();
            }
            return str;
        }
    }
}
