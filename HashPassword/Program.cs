using System;
using System.Linq;

namespace HashPassword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password = args.ElementAtOrDefault(0);
            string salt = args.ElementAtOrDefault(1);

            if (password is null)
            {
                Console.WriteLine("Password is null.");
                return;
            }
            if (salt is null)
            {
                Console.WriteLine("Salt is null.");
                return;
            }

            GenerateHash(password, salt);
        }

        private static void GenerateHash(string password, string salt)
        {
            //string password = "qa";
            //string salt = "6UOnMNpHE1M=";

            string hash = SecurityHelper.HashPassword(password, salt);
            Console.WriteLine(password);
            Console.WriteLine(salt);
            Console.WriteLine(hash);
        }
    }
}
