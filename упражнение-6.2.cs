using System;

class Program
{
    static void Print(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    static int[,] Umnojenie(int[,] a, int[,] b)
    {
        int strokA = a.GetLength(0);
        int stolbA = a.GetLength(1);
        int strokB = b.GetLength(0);
        int stolbB = b.GetLength(1);

        if (a.GetLength(0) != b.GetLength(0))
        {
            Console.WriteLine("Матрицы нельзя умножить");
        }

        int[,] result = new int[strokA, stolbB];

        for (int i = 0; i < strokA; i++)
        {
            for (int j = 0; j < stolbB; j++)
            {
                for (int k = 0; k < strokA; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }

        return result;
    }

    static void Main()
    {
        int[,] a = new int[2, 2];
        int[,] b = new int[2, 2];

        Console.WriteLine("Введите числа первой матрицы 2 на 2:");

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.Write($"Элемент [{i}, {j}]: ");
                a[i, j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Введите числа второй матрицы 2 на 2:");

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.Write($"Элемент [{i}, {j}]: ");
                b[i, j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Первая матрица:");
        Print(a);

        Console.WriteLine("Вторая матрица:");
        Print(b);

        int[,] result = Umnojenie(a, b);

        Console.WriteLine("Результат умножения:");
        Print(result);
    }
}