using System;
using System.Collections.Generic;

class Program
{
    static void Print(LinkedList<LinkedList<int>> matrix)
    {
        foreach (LinkedList<int> row in matrix)
        {
            foreach (int element in row)
            {
                Console.Write(element + " ");
            }

            Console.WriteLine();
        }
    }

    static LinkedList<LinkedList<int>> Umnojenie (LinkedList<LinkedList<int>> a, LinkedList<LinkedList<int>> b)
    {
        int strokA = a.Count;
        int stolbA = a.First.Value.Count;
        int strokB = b.Count;
        int stolbB = b.First.Value.Count;

        if (stolbA != strokB)
        {
            Console.WriteLine("Матрицы нельзя умножить");
            return new LinkedList<LinkedList<int>>();
        }

        LinkedList<LinkedList<int>> result =
            new LinkedList<LinkedList<int>>();

        for (int i = 0; i < strokA; i++)
        {
            LinkedList<int> row = new LinkedList<int>();

            for (int j = 0; j < stolbB; j++)
            {
                int summa = 0;

                for (int k = 0; k < stolbA; k++)
                {
                    int elementA = GetElement(a, i, k);
                    int elementB = GetElement(b, k, j);

                    summa += elementA * elementB;
                }

                row.AddLast(summa);
            }

            result.AddLast(row);
        }

        return result;
    }

    static int GetElement(
        LinkedList<LinkedList<int>> matrix,
        int rowIndex,
        int colIndex)
    {
        LinkedList<int> row = matrix.First.Value;

        for (int i = 0; i < rowIndex; i++)
        {
            row = matrix.Find(row).Next.Value;
        }

        LinkedList<int>.Enumerator enumerator = row.GetEnumerator();

        for (int j = 0; j <= colIndex; j++)
        {
            enumerator.MoveNext();
        }

        return enumerator.Current;
    }

    static void Main()
    {
        LinkedList<LinkedList<int>> a =
            new LinkedList<LinkedList<int>>();

        LinkedList<LinkedList<int>> b =
            new LinkedList<LinkedList<int>>();

        Console.WriteLine("Введите числа первой матрицы 2 на 2:");

        for (int i = 0; i < 2; i++)
        {
            LinkedList<int> row = new LinkedList<int>();

            for (int j = 0; j < 2; j++)
            {
                Console.Write($"Элемент [{i}, {j}]: ");
                row.AddLast(int.Parse(Console.ReadLine()));
            }

            a.AddLast(row);
        }

        Console.WriteLine("Введите числа второй матрицы 2 на 2:");

        for (int i = 0; i < 2; i++)
        {
            LinkedList<int> row = new LinkedList<int>();

            for (int j = 0; j < 2; j++)
            {
                Console.Write($"Элемент [{i}, {j}]: ");
                row.AddLast(int.Parse(Console.ReadLine()));
            }

            b.AddLast(row);
        }

        Console.WriteLine("Первая матрица:");
        Print(a);

        Console.WriteLine("Вторая матрица:");
        Print(b);

        LinkedList<LinkedList<int>> result = Umnojenie(a, b);

        Console.WriteLine("Результат умножения:");
        Print(result);
    }
}
