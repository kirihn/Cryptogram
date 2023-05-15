using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Cryptogram.Model
{
    public static class Cryptoger
    {
        public static string EncryptData(long Key, string StrokaToEncrypt)
        {
            if (Key > 999_999_999_999_999_999)
            {
                //MessageBox.show("ENCRYPT ERROR: Key non value");
                return "ENCRYPT ERROR";
            }

            long HashKey = Key.ToString().GetHashCode();
            HashKey = Key;

            if (HashKey < 0) HashKey *= -1;

            string Encrypt = "";
            long SymbolCode;
            string SymbolCodeStr;


            foreach (var item in StrokaToEncrypt)
            {
                SymbolCode = (long)item ^ (long)HashKey;
                SymbolCodeStr = SymbolCode.ToString();

                if (SymbolCodeStr.Length < 10)
                {
                    Encrypt += "0" + SymbolCodeStr.Length.ToString() + SymbolCodeStr;
                }
                else
                {
                    Encrypt += SymbolCodeStr.Length.ToString() + SymbolCodeStr;
                }
            }

            return Encrypt;
        }

        public static string DecryptData(long Key, string StrokaToDecrypt)
        {
            if (Key > 999_999_999_999_999_999)
            {
                //MessageBox.show("ENCRYPT ERROR: Key non value");
                return "DECRYPT ERROR";
            }

            long HashKey = Key.ToString().GetHashCode();
            HashKey = Key;

            if (HashKey < 0) HashKey *= -1;

            string Decrypt = "";
            long SymbolCode;
            string SymbolCodeStr;

            long Codelenght = StrokaToDecrypt.Length;
            int CountCode;

            while (StrokaToDecrypt.Length > 0)
            {
                CountCode = int.Parse(StrokaToDecrypt.Substring(0, 2));

                StrokaToDecrypt = StrokaToDecrypt.Remove(0, 2);

                SymbolCodeStr = StrokaToDecrypt.Substring(0, CountCode);

                StrokaToDecrypt = StrokaToDecrypt.Remove(0, CountCode);

                //

                SymbolCode = long.Parse(SymbolCodeStr) ^ HashKey;

                if (SymbolCode < 0 || SymbolCode > 65535)
                {
                    SymbolCode = (new Random()).Next(255);
                }

                Decrypt += Convert.ToChar(SymbolCode);
            }
            return Decrypt;
        }

        public static string HashString(string inputString)
        {
            using (SHA256 sha256Hash = SHA256.Create()) 
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
