using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Services
{
    public class EncryptionService
    {
        private const string rusAlphabetUp = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string rusAlphabetLow = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private const string engAlphabetUp = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string engAlphabetLow = "abcdefghijklmnopqrstuvwxyz";

        public string CaesarDecrypt(string str, int key)
        {
            return CaesarEncrypt(str, 26 - key);
        }
        public string CaesarEncrypt(string str, int key)
        {
            string output = "";
            foreach (char symbol in str)
            {
                output += CaesarSymbolEncrypt(symbol, key);
            }
            return output;
        }
        public static char CaesarSymbolEncrypt(char symbol, int key)
        {
            if (!char.IsLetter(symbol))
                return symbol;
            else
            {
                string alphabet;

                if (rusAlphabetLow.Contains(symbol))
                    alphabet = rusAlphabetLow;
                else if (rusAlphabetUp.Contains(symbol))
                    alphabet = rusAlphabetUp;
                else if (engAlphabetLow.Contains(symbol))
                    alphabet = engAlphabetLow;
                else
                    alphabet = engAlphabetUp;

                int number = alphabet.IndexOf(symbol) + key;
                if (number >= alphabet.Length)
                    number -= alphabet.Length;
                if (number < 0)
                    number += alphabet.Length;

                return alphabet[number];
            }
        }
        public string VigenereCrypt(string str, string key, bool decrypt)
        {
            int decryptIndex; ;
            if (decrypt)
                decryptIndex = -1;
            else
                decryptIndex = 1;
            int[] intKey = new int[key.Length];
            for (int index = 0; index < key.Length; intKey[index] = rusAlphabetUp.IndexOf(key.ToUpper()[index]), index++) ;
            string output = "";
            for (int index = 0; index < str.Length; index++)
            {
                int keyIndex = index % key.Length;
                output += CaesarSymbolEncrypt(str[index], decryptIndex * intKey[keyIndex]);
            }
            return output;
        }
    }
}
