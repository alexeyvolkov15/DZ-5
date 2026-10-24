using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Student
{
    public string Surname;
    public string Name;
    public int Year;
    public string Exam;
    public int Score;

    public Student(string surname, string name, int year, string exam, int score)
    {
        Surname = surname;
        Name = name;
        Year = year;
        Exam = exam;
        Score = score;
    }

    public void Print()
    {
        Console.WriteLine($"{Surname} {Name}, {Year}, {Exam}, {Score} баллов");
    }
}

class Grandma
{
    public string Name;
    public int Age;
    public Dictionary<string, string> Diseases = new Dictionary<string, string>();
    public string HospitalName = "Не распределена";

    public Grandma(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Hospital
{
    public string Name;
    public List<string> Diseases;
    public int Capacity;
    public Stack<Grandma> Patients = new Stack<Grandma>();

    public Hospital(string name, List<string> diseases, int capacity)
    {
        Name = name;
        Diseases = diseases;
        Capacity = capacity;
    }
}

class Program
{
    static async Task Main()
    {
        bool working = true;

        while (working)
        {
            Console.WriteLine("\n===== ЛАБОРАТОРНАЯ РАБОТА =====");
            Console.WriteLine("1. Картинки и перемешивание");
            Console.WriteLine("2. Студенты");
            Console.WriteLine("3. Бабули и больницы");
            Console.WriteLine("4. Обход графа");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите задачу: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await Task1();
                    break;

                case "2":
                    Task2();
                    break;

                case "3":
                    Task3();
                    break;

                case "4":
                    Task4();
                    break;

                case "0":
                    working = false;
                    break;

                default:
                    Console.WriteLine("Нет такой задачи!");
                    break;
            }
        }
    }

    // ЗАДАНИЕ 1. КАРТИНКИ

    static async Task Task1()
    {
        Console.WriteLine("\nЗАДАНИЕ 1. Картинки и перемешивание");

        List<int> pictures = new List<int>();

        try
        {
            using (HttpClient client = new HttpClient())
            {
                Directory.CreateDirectory("Images");

                for (int i = 1; i <= 32; i++)
                {
                    string url = $"https://picsum.photos/seed/pair{i}/200/200";
                    string path = $"Images/image{i}.jpg";

                    byte[] image = await client.GetByteArrayAsync(url);
                    await File.WriteAllBytesAsync(path, image);

                    pictures.Add(i);
                    pictures.Add(i);
                }
            }

            Console.WriteLine("Исходный список:");
            PrintNumbers(pictures);

            Random random = new Random();

            for (int i = pictures.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                int temp = pictures[i];
                pictures[i] = pictures[j];
                pictures[j] = temp;
            }

            Console.WriteLine("\nПеремешанный список:");
            PrintNumbers(pictures);
            Console.WriteLine("\nКартинки сохранены в папке Images.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка скачивания картинок: " + e.Message);
            Console.WriteLine("Проверь подключение к интернету.");
        }
    }

    static void PrintNumbers(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            Console.Write(numbers[i] + " ");

            if ((i + 1) % 16 == 0)
                Console.WriteLine();
        }

        Console.WriteLine();
    }

    // ЗАДАНИЕ 2. СТУДЕНТЫ

    static void Task2()
    {
        Console.WriteLine("\nЗАДАНИЕ 2. Студенты");

        string file = "students.txt";

        if (!File.Exists(file))
        {
            string[] example =
            {
                "Иванов;Иван;2007;Математика;240",
                "Петров;Пётр;2006;Информатика;250",
                "Сидоров;Алексей;2007;Физика;230",
                "Смирнова;Анна;2007;Математика;270",
                "Кузнецов;Дмитрий;2006;Информатика;220",
                "Попова;Мария;2007;Физика;260",
                "Васильев;Артём;2006;Математика;210",
                "Соколова;Елена;2007;Информатика;280",
                "Михайлов;Илья;2007;Физика;245",
                "Фёдорова;Ольга;2006;Математика;255"
            };

            File.WriteAllLines(file, example);
        }

        List<Student> students = new List<Student>();
        Dictionary<string, Student> dictionary =
            new Dictionary<string, Student>();

        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            string[] data = line.Split(';');

            if (data.Length == 5 &&
                int.TryParse(data[2], out int year) &&
                int.TryParse(data[4], out int score))
            {
                students.Add(new Student(
                    data[0], data[1], year, data[3], score));
            }
        }

        UpdateStudents(students, dictionary);

        bool menu = true;

        while (menu)
        {
            Console.WriteLine("\nКоманды:");
            Console.WriteLine("Новый студент");
            Console.WriteLine("Удалить");
            Console.WriteLine("Сортировать");
            Console.WriteLine("Показать");
            Console.WriteLine("Выход");
            Console.Write("Введите команду: ");

            string command = Console.ReadLine();

            switch (command.ToLower())
            {
                case "новый студент":
                    Console.Write("Фамилия: ");
                    string surname = Console.ReadLine();

                    Console.Write("Имя: ");
                    string name = Console.ReadLine();

                    Console.Write("Год рождения: ");
                    if (!int.TryParse(Console.ReadLine(), out int year))
                    {
                        Console.WriteLine("Неверный год!");
                        break;
                    }

                    Console.Write("Экзамен: ");
                    string exam = Console.ReadLine();

                    Console.Write("Баллы: ");
                    if (!int.TryParse(Console.ReadLine(), out int score))
                    {
                        Console.WriteLine("Неверные баллы!");
                        break;
                    }

                    students.Add(new Student(surname, name, year, exam, score));

                    UpdateStudents(students, dictionary);
                    SaveStudents(students, file);

                    Console.WriteLine("Студент добавлен.");
                    break;

                case "удалить":
                    Console.Write("Фамилия: ");
                    string deleteSurname = Console.ReadLine();

                    Console.Write("Имя: ");
                    string deleteName = Console.ReadLine();

                    int index = students.FindIndex(s =>
                        s.Surname.Equals(deleteSurname,
                            StringComparison.OrdinalIgnoreCase) &&
                        s.Name.Equals(deleteName,
                            StringComparison.OrdinalIgnoreCase));

                    if (index >= 0)
                    {
                        students.RemoveAt(index);
                        UpdateStudents(students, dictionary);
                        SaveStudents(students, file);
                        Console.WriteLine("Студент удалён.");
                    }
                    else
                    {
                        Console.WriteLine("Студент не найден.");
                    }
                    break;

                case "сортировать":
                    students.Sort((a, b) => a.Score.CompareTo(b.Score));
                    UpdateStudents(students, dictionary);

                    foreach (Student s in students)
                        s.Print();

                    break;

                case "показать":
                    foreach (Student s in students)
                        s.Print();

                    Console.WriteLine("Студентов в словаре: " +
                                      dictionary.Count);
                    break;

                case "выход":
                    menu = false;
                    break;

                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }

    static void UpdateStudents(
        List<Student> students,
        Dictionary<string, Student> dictionary)
    {
        dictionary.Clear();

        foreach (Student s in students)
        {
            string key = s.Surname + " " + s.Name;

            if (dictionary.ContainsKey(key))
                key += " (" + s.Year + ")";

            dictionary[key] = s;
        }
    }

    static void SaveStudents(List<Student> students, string file)
    {
        List<string> lines = new List<string>();

        foreach (Student s in students)
        {
            lines.Add($"{s.Surname};{s.Name};{s.Year};{s.Exam};{s.Score}");
        }

        File.WriteAllLines(file, lines);
    }

    // ЗАДАНИЕ 3. БАБУЛИ И БОЛЬНИЦЫ

    static void Task3()
    {
        Console.WriteLine("\nЗАДАНИЕ 3. Бабули и больницы");

        Stack<Hospital> hospitals = new Stack<Hospital>();

        hospitals.Push(new Hospital(
            "Больница №1",
            new List<string> { "Давление", "Сердце" }, 3));

        hospitals.Push(new Hospital(
            "Больница №2",
            new List<string> { "Суставы", "Диабет" }, 2));

        hospitals.Push(new Hospital(
            "Больница №3",
            new List<string> { "Сердце", "Диабет", "Давление" }, 4));

        Queue<Grandma> queue = new Queue<Grandma>();
        List<Grandma> allGrandmas = new List<Grandma>();

        bool menu = true;

        while (menu)
        {
            Console.WriteLine("\n1. Принять бабулю");
            Console.WriteLine("2. Распределить очередь");
            Console.WriteLine("3. Показать бабуль и больницы");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Имя бабули: ");
                    string name = Console.ReadLine();

                    Console.Write("Возраст: ");
                    if (!int.TryParse(Console.ReadLine(), out int age))
                    {
                        Console.WriteLine("Неверный возраст.");
                        break;
                    }

                    Grandma grandma = new Grandma(name, age);

                    Console.Write("Количество болезней: ");
                    if (!int.TryParse(Console.ReadLine(), out int count) ||
                        count < 0)
                    {
                        Console.WriteLine("Неверное количество.");
                        break;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Console.Write("Название болезни: ");
                        string disease = Console.ReadLine();

                        Console.Write("Лекарство: ");
                        string medicine = Console.ReadLine();

                        grandma.Diseases[disease] = medicine;
                    }

                    queue.Enqueue(grandma);
                    allGrandmas.Add(grandma);

                    Console.WriteLine("Бабуля встала в очередь.");
                    break;

                case "2":
                    if (queue.Count == 0)
                    {
                        Console.WriteLine("Очередь пуста.");
                        break;
                    }

                    while (queue.Count > 0)
                    {
                        Grandma g = queue.Dequeue();
                        bool admitted = false;

                        // Больницы проверяем по очереди в порядке стека
                        foreach (Hospital h in hospitals)
                        {
                            if (h.Patients.Count >= h.Capacity)
                                continue;

                            if (g.Diseases.Count == 0)
                            {
                                h.Patients.Push(g);
                                g.HospitalName = h.Name;
                                admitted = true;
                                break;
                            }

                            int matched = 0;

                            foreach (string disease in g.Diseases.Keys)
                            {
                                if (h.Diseases.Contains(disease))
                                    matched++;
                            }

                            if (matched * 2 > g.Diseases.Count)
                            {
                                h.Patients.Push(g);
                                g.HospitalName = h.Name;
                                admitted = true;
                                break;
                            }
                        }

                        if (admitted)
                            Console.WriteLine(g.Name + " направлена: " +
                                              g.HospitalName);
                        else
                            Console.WriteLine(g.Name +
                                " осталась на улице плакать.");
                    }
                    break;

                case "3":
                    Console.WriteLine("\nВсе бабули:");

                    foreach (Grandma g in allGrandmas)
                    {
                        Console.WriteLine(
                            $"{g.Name}, {g.Age} лет, больница: {g.HospitalName}");

                        foreach (var disease in g.Diseases)
                            Console.WriteLine("  " + disease.Key +
                                              ", лекарство: " + disease.Value);
                    }

                    Console.WriteLine("\nБольницы:");

                    foreach (Hospital h in hospitals)
                    {
                        double percent =
                            (double)h.Patients.Count / h.Capacity * 100;

                        Console.WriteLine($"\n{h.Name}");
                        Console.WriteLine("Лечат: " +
                                          string.Join(", ", h.Diseases));
                        Console.WriteLine("Пациентов: " +
                                          h.Patients.Count + "/" + h.Capacity);
                        Console.WriteLine("Заполненность: " + percent + "%");
                    }

                    Console.WriteLine("\nБабуль в очереди: " + queue.Count);
                    break;

                case "0":
                    menu = false;
                    break;

                default:
                    Console.WriteLine("Нет такой команды.");
                    break;
            }
        }
    }

    // ЗАДАНИЕ 4. КРАТЧАЙШИЙ ПУТЬ В ГРАФЕ

    static void Task4()
    {
        Console.WriteLine("\nЗАДАНИЕ 4. Кратчайший путь в графе");

        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

        graph[1] = new List<int> { 2, 3 };
        graph[2] = new List<int> { 1, 4, 5 };
        graph[3] = new List<int> { 1, 6 };
        graph[4] = new List<int> { 2, 7 };
        graph[5] = new List<int> { 2, 7 };
        graph[6] = new List<int> { 3, 7 };
        graph[7] = new List<int> { 4, 5, 6 };

        Console.Write("Начальная вершина (1-7): ");
        if (!int.TryParse(Console.ReadLine(), out int start) ||
            !graph.ContainsKey(start))
        {
            Console.WriteLine("Неверная вершина.");
            return;
        }

        Console.Write("Конечная вершина (1-7): ");
        if (!int.TryParse(Console.ReadLine(), out int finish) ||
            !graph.ContainsKey(finish))
        {
            Console.WriteLine("Неверная вершина.");
            return;
        }

        Queue<int> queue = new Queue<int>();
        Dictionary<int, int> previous = new Dictionary<int, int>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == finish)
                break;

            foreach (int next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    previous[next] = current;
                    queue.Enqueue(next);
                }
            }
        }

        if (!visited.Contains(finish))
        {
            Console.WriteLine("Путь не найден.");
            return;
        }

        List<int> path = new List<int>();
        int vertex = finish;

        path.Add(vertex);

        while (vertex != start)
        {
            vertex = previous[vertex];
            path.Add(vertex);
        }

        path.Reverse();

        Console.WriteLine("Кратчайший путь: " + string.Join(" -> ", path));
        Console.WriteLine("Количество рёбер: " + (path.Count - 1));
    }
}
