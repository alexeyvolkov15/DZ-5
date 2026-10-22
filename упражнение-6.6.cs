using System;
using System.Collections.Generic;

enum month
{
    Январь,
    Февраль,
    Март,
    Апрель,
    Май,
    Июнь,
    Июль,
    Август,
    Сентябрь,
    Октябрь,
    Ноябрь,
    Декабрь
}

class Program
{
    static Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();

    static Dictionary<string, double> MidTemperature()
    {
        Dictionary<string, double> mid = new Dictionary<string, double>();

        foreach (var item in temperature)
        {
            int summa = 0;

            for (int j = 0; j < item.Value.Length; j++)
            {
                summa += item.Value[j];
            }

            mid.Add(item.Key, (double)summa / item.Value.Length);
        }

        return mid;
    }

    static void Main()
    {
        Random random = new Random();

        for (int i = 0; i < 12; i++)
        {
            string name = ((month)i).ToString();
            int[] days = new int[30];

            for (int j = 0; j < 30; j++)
            {
                days[j] = random.Next(-30, 30);
            }

            temperature.Add(name, days);
        }

        Dictionary<string, double> mid = MidTemperature();

        Console.WriteLine("Средняя температура по месяцам:");

        foreach (var item in mid)
        {
            Console.WriteLine(item.Key + ": " + Math.Round(item.Value, 2));
        }

        List<KeyValuePair<string, double>> sorted = new List<KeyValuePair<string, double>>(mid);

        sorted.Sort((a, b) => a.Value.CompareTo(b.Value));

        Console.WriteLine("Средние температуры по возрастанию:");

        foreach (var item in sorted)
        {
            Console.WriteLine(item.Key + ": " + Math.Round(item.Value, 2));
        }
    }
}
