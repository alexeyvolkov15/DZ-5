
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Shet(LinkedList<LinkedList<char>> symbols, out int glasn, out int soglasn)
    {
        glasn = 0;
        soglasn = 0;

        string vseglasn = "аеёиоуыэюя";

        foreach (LinkedList<char> list in symbols)
        {
            foreach (char symbol in list)
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
    }

    static void Main(string[] args)
    {
        string text = File.ReadAllText(args[0]);

        LinkedList<LinkedList<char>> symbols = new LinkedList<LinkedList<char>>();

        foreach (char symbol in text)
        {
            LinkedList<char> list = new LinkedList<char>();
            list.AddLast(symbol);
            symbols.AddLast(list);
        }

        Shet(symbols, out int glasn, out int soglasn);

        Console.WriteLine("Количество гласных: " + glasn);
        Console.WriteLine("Количество согласных: " + soglasn);
    }
}
