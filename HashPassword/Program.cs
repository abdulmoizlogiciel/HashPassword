using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace HashPassword
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var bcryptOption = new Option<bool>(new string[] { "--bcrypt", }, "Will generate a Bcrypt hash when this flag is given else it will create an Rfc2898 hash.");
            var passwordOption = new Option<string>(new string[] { "--password", "--pass", "-p", }, "Password which will be hashed.");
            var saltOption = new Option<string>(new string[] { "--salt", }, "Salt through which hash will generated.");
            var hashOption = new Option<string>(new string[] { "--hash", }, "Password Hash to match.");
            var verifyOption = new Option<bool>(new string[] { "--verify", }, "To math the provided password and hash.");

            var rootCommand = new RootCommand("This is some console app description which will be shown when run with -h / --help argument.")
            {
                bcryptOption,
                passwordOption,
                saltOption,
                hashOption,
                verifyOption,
            };
            rootCommand.SetHandler(Handle, bcryptOption, passwordOption, saltOption, hashOption, verifyOption);
            return rootCommand.Invoke(args);
        }

        private static void Handle(bool isBcrypt, string password, string salt, string hash, bool verify)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password is required.");
            }

            if (isBcrypt)
            {
                GenerateBcryptHash(password);
            }
            else
            {
                if (string.IsNullOrEmpty(salt))
                {
                    throw new ArgumentNullException("Salt is required with Rfc2898 hashing.");
                }
                GenerateRfc2898Hash(password, salt);
            }
        }

        private static void GenerateBcryptHash(string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.SaltRevision.Revision2Y);

            Console.WriteLine($"password = {password}");
            Console.WriteLine($"hash = {hash}");
        }

        private static Task<int> VerifyBcryptHash(string password, string hash)
        {
            Console.WriteLine($"password = {password}");
            Console.WriteLine($"hash = {hash}");
            if (BCrypt.Net.BCrypt.Verify(password, hash))
            {
                Console.WriteLine("Provided password and hash matched.");
            }
            else
            {
                Console.WriteLine("Provided password and hash DID NOT match.");
            }
            return Task.FromResult(0);
        }

        private static void GenerateRfc2898Hash(string password, string salt)
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
