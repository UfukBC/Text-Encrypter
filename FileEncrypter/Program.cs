using System.Reflection;

namespace FileEncrypter
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Select 1 for encrypt, Select 2 for decrypt ");
            string choice = Console.ReadLine();

            Console.Write("Enter the file path ");
            string inputFile = Console.ReadLine();

            Console.Write("Çıkış dosyasını kaydetmek için yol girin: ");
            string outputFile = Console.ReadLine();

            Console.Write("Enter the password ");
            string password = Console.ReadLine();

            try
            {
                if (choice == "1")
                    Class1.EncryptFile(inputFile, outputFile, password);
                else if (choice == "2")
                    Class1.DecryptFile(inputFile, outputFile, password);
                else
                    Console.WriteLine("Geçersiz seçim!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}


