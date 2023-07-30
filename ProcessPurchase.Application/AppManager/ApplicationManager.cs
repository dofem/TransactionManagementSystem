using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPurchase.Application.AppManager
{
    public class ApplicationManager
    {
        public static string GenerateTransactionReferenceNumber()
        {
            string today = DateTime.Now.ToString("ddMMyyyyHHmmss");
            int random = new Random().Next(10000, 99999);
            return today + random.ToString();
        }


        public static string GenerateWalletId()
        {
            var random = new Random();
            int randomNumber = random.Next(1, 100000); 

            // Format the random number with leading zeros (up to 5 digits)
            string formattedRandomNumber = randomNumber.ToString("D5");

            string walletId = $"SUPER{formattedRandomNumber}";

            return walletId;
        }

        public static void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, storedHash);
            }
        }
    }
}
