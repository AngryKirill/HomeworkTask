using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HWTask.Services
{
    public class HashService
    {
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
