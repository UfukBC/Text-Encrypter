using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileEncrypter
{
    internal class Class1
    {
        public static void EncryptFile(string inputFile, string outputFile, string password)
        {
            byte[] key, iv; //key and IV variables
            GenerateKeyAndIV(password, out key, out iv); //generates key and IV based on our password

            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))  //Create or overwrite the output file
            using (Aes aes = Aes.Create()) // creates aes object (encryption algorithm)
            {
                aes.Key = key; //gives our key and IV to the encryption algorithm
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write)) //It writes the chiper to our output file
                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open)) //reads our text
                {
                    fsInput.CopyTo(cs); //copy data from the input file to the cryptostream for encryption
                }
            }
            Console.WriteLine($"The file has been encrypted: {outputFile}");


            File.Delete(inputFile);
            Console.WriteLine($"The original file has been deleted: {inputFile}");

            string renamedFilePath = Path.Combine(Path.GetDirectoryName(inputFile), Path.GetFileName(inputFile));
            File.Move(outputFile, renamedFilePath);
        }

        public static void DecryptFile(string inputFile, string outputFile, string password)
        {
            byte[] key, iv;
            GenerateKeyAndIV(password, out key, out iv);

            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateDecryptor(), CryptoStreamMode.Write))
                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                {
                    fsInput.CopyTo(cs);
                }
            }
            Console.WriteLine($"The file has encrypted: {outputFile}");
        }

        private static void GenerateKeyAndIV(string password, out byte[] key, out byte[] iv)
        {
            using (var sha256 = SHA256.Create())
            {
                key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                iv = new byte[16];
                Array.Copy(key, iv, iv.Length);
            }
        }
    }
}
