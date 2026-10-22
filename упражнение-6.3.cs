using System;

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
    static double[] MidTemperature(int[,] temperature)
    {
        double[] mid = new double[12];

        for (int i = 0; i < 12; i++)
        {
            int summa = 0;

            for (int j = 0; j < 30; j++)
            {
                summa += temperature[i, j];
            }

            mid[i] = (double)summa / 30;
        }

        return mid;
    }

    static void Main()
    {
        int[,] temperature = new int[12, 30];
        Random random = new Random();

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                temperature[i, j] = random.Next(-30, 30);
            }
        }

        double[] mid = MidTemperature(temperature);

        Console.WriteLine("Средняя температура по месяцам:");

        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine((month)i + ": " + Math.Round(mid[i], 2));
        }

        Array.Sort(mid);

        Console.WriteLine("Средние температуры по возрастанию:");

        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine(Math.Round(mid[i], 2));
        }
    }
}
