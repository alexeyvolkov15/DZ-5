using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Shet(List<char> symbols, out int glasn, out int soglasn)
    {
        glasn = 0;
        soglasn = 0;

        string vseglasn = "аеёиоуыэюя";

        foreach (char symbol in symbols)
        {
            char letter = char.ToLower(symbol);

            if (char.IsLetter(letter))
            {
                if (vseglasn.Contains(letter))
                {
                    glasn++;
                }
                else
                {
                    soglasn++;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        string text = File.ReadAllText(args[0]);

        List<char> symbols = new List<char>();

        foreach (char symbol in text)
        {
            symbols.Add(symbol);
        }

        Shet(symbols, out int glasn, out int soglasn);

        Console.WriteLine("Количество гласных: " + glasn);
        Console.WriteLine("Количество согласных: " + soglasn);
    }
}