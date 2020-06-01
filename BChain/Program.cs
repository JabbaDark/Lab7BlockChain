using System;
using System.Collections;
using System.Security.Cryptography;

namespace BChain
{
    class Program
    {
        static int value = 0, numberOfZeroes = 0;
        static void Main(string[] args)
        {
            while (value < 1)// считывание значения
            {
                Console.WriteLine("Value:");
                int.TryParse(Console.ReadLine(), out value);
            }

            while (numberOfZeroes < 1)// считывание количества нулей
            {
                Console.WriteLine("Zeroes:");
                int.TryParse(Console.ReadLine(), out numberOfZeroes);
            }

            int nonce = 0;

            using (SHA256 sha256Hash = SHA256.Create())// используем алгоритм SHA256
            {
                while (nonce > -1)
                {
                    var bytes = sha256Hash.ComputeHash(sha256Hash.ComputeHash(BitConverter.GetBytes(value | nonce)));// функция для расчета хэша
                    Console.WriteLine("\n");
                    var bits = new BitArray(bytes);// задание массива битов
                    foreach (var bit in bits)
                    {
                        Console.Write((bool)bit ? "1" : "0");// вывод в консоль 0 и 1 для битов из bits
                    }
                    bool isHashAppropriate = true;// проверочное значение для хэша (нужное ли количество нулей)
                    for (int i = 0; i < numberOfZeroes; i++)
                    {
                        if (bits[i])// если обнаружена 1 среди необходимого количества нулей
                        {
                            isHashAppropriate = false;
                            break;
                        }
                    }
                    if (isHashAppropriate)// если необходимая комбинация найдена
                    {
                        Console.WriteLine("\nDone");
                        break;
                    }
                    nonce++; //переход к следующему nonce, если если нужная комбинация не была найдена
                }
            }
            Console.ReadKey();
        }
    }
}
