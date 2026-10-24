using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;

struct Student
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
}


class Grandma
{
    public string Name;
    public int Age;
    public Dictionary<string, string> Diseases;
    public string HospitalName;

    public Grandma(string name, int age)
    {
        Name = name;
        Age = age;
        Diseases = new Dictionary<string, string>();
        HospitalName = "На улице";
    }
}


class Hospital
{
    public string Name;
    public List<string> Diseases;
    public int Capacity;
    public List<Grandma> Patients;

    public Hospital(string name, List<string> diseases, int capacity)
    {
        Name = name;
        Diseases = diseases;
        Capacity = capacity;
        Patients = new List<Grandma>();
    }
}


class Program
{
    static string StudentKey(Student student)
    {
        return student.Surname + " " + student.Name;
    }


    static void SaveStudents(List<Student> students)
    {
        using (StreamWriter writer = new StreamWriter("students.txt"))
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(students[i].Surname + ";" + students[i].Name + ";" + students[i].Year + ";" + students[i].Exam + ";" + students[i].Score);
            }
        }
    }


    static void ShowStudents(List<Student> students)
    {
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine(students[i].Surname + " " + students[i].Name + ", год рождения: " + students[i].Year + ", экзамен: " + students[i].Exam + ", баллы: " + students[i].Score);
        }
    }


    static Grandma CreateGrandma()
    {
        Console.WriteLine("Введите имя бабули:");
        string name = Console.ReadLine();

        Console.WriteLine("Введите возраст:");
        int age = int.Parse(Console.ReadLine());

        Grandma grandma = new Grandma(name, age);

        Console.WriteLine("Введите количество болезней:");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Введите болезнь:");
            string disease = Console.ReadLine();

            Console.WriteLine("Введите лекарство от этой болезни:");
            string medicine = Console.ReadLine();

            grandma.Diseases[disease] = medicine;
        }

        return grandma;
    }


    static void ShowGrandmas(List<Grandma> grandmas)
    {
        Console.WriteLine("Список бабуль:");

        for (int i = 0; i < grandmas.Count; i++)
        {
            Console.WriteLine(grandmas[i].Name + ", возраст: " + grandmas[i].Age + ", больница: " + grandmas[i].HospitalName);

            foreach (KeyValuePair<string, string> disease in grandmas[i].Diseases)
            {
                Console.WriteLine("Болезнь: " + disease.Key + ", лекарство: " + disease.Value);
            }

            if (grandmas[i].Diseases.Count == 0)
            {
                Console.WriteLine("Болезней нет");
            }
        }
    }


    static void ShowHospitals(Stack<Hospital> hospitals)
    {
        Console.WriteLine("Список больниц:");

        foreach (Hospital hospital in hospitals)
        {
            Console.WriteLine("Название: " + hospital.Name);

            Console.WriteLine("Лечат болезни:");

            for (int i = 0; i < hospital.Diseases.Count; i++)
            {
                Console.WriteLine(hospital.Diseases[i]);
            }

            Console.WriteLine("Бабуль в больнице: " + hospital.Patients.Count);

            Console.WriteLine("Вместимость: " + hospital.Capacity);

            double percent = (double)hospital.Patients.Count / hospital.Capacity * 100;

            Console.WriteLine("Заполненность: " + percent + "%");
        }
    }


    static void Main()
    {
        Console.WriteLine("Введите число от 1 до 4:");
        int number = int.Parse(Console.ReadLine());

        switch (number)
        {



            case 1:

                Console.WriteLine("Задание 1");
                Console.WriteLine("Скачивание и перемешивание картинок");

                List<string> pictures = new List<string>();

                Directory.CreateDirectory("Images");

                using (HttpClient client = new HttpClient())
                {
                    for (int i = 1; i <= 32; i++)
                    {
                        try
                        {
                            string path = "Images/" + i + ".jpg";
                            string url = "https://picsum.photos/seed/picture" + i + "/200/200";

                            byte[] image = client.GetByteArrayAsync(url).GetAwaiter().GetResult();

                            File.WriteAllBytes(path, image);

                            pictures.Add(path);
                            pictures.Add(path);
                        }
                        catch
                        {
                            Console.WriteLine("Не удалось скачать картинку " + i);
                        }
                    }
                }

                Console.WriteLine("Исходный список:");

                for (int i = 0; i < pictures.Count; i++)
                {
                    Console.Write(Path.GetFileNameWithoutExtension(pictures[i]) + ", ");
                }

                Random random = new Random();

                for (int i = 0; i < pictures.Count; i++)
                {
                    int j = random.Next(i, pictures.Count);

                    string temp = pictures[i];
                    pictures[i] = pictures[j];
                    pictures[j] = temp;
                }

                Console.WriteLine("Перемешанный список:");

                for (int i = 0; i < pictures.Count; i++)
                {
                    Console.Write( Path.GetFileNameWithoutExtension(pictures[i]) + ", ");
                }

                Console.WriteLine();

                break;



            case 2:

                Console.WriteLine("Задание 2");
                Console.WriteLine("Работа со списком студентов");

                List<Student> students = new List<Student>();

                if (!File.Exists("students.txt"))
                {
                    using (StreamWriter writer = new StreamWriter("students.txt"))
                    {
                        writer.WriteLine("Иванов;Иван;2008;Физика;240");
                        writer.WriteLine("Петров;Пётр;2008;Физика;260");
                        writer.WriteLine("Сидоров;Алексей;2008;Информатика;250");
                        writer.WriteLine("Смирнов;Олег;2008;Физика;280");
                        writer.WriteLine("Кузнецов;Дмитрий;2008;Физика;270");
                        writer.WriteLine("Попов;Андрей;2008;Информатика;260");
                        writer.WriteLine("Васильев;Илья;2008;Информатика;265");
                        writer.WriteLine("Морозов;Егор;2008;Физика;255");
                        writer.WriteLine("Новиков;Максим;2008;Информатика;285");
                        writer.WriteLine("Фёдоров;Кирилл;2008;Информатика;275");
                    }
                }

                string[] lines = File.ReadAllLines("students.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(';');

                    Student student = new Student(parts[0], parts[1], int.Parse(parts[2]), parts[3], int.Parse(parts[4]));

                    students.Add(student);
                }

                Dictionary<string, Student> studentDictionary = new Dictionary<string, Student>();

                for (int i = 0; i < students.Count; i++)
                {
                    studentDictionary[StudentKey(students[i])] = students[i];
                }

                string command = "";

                while (command != "Выход")
                {
                    Console.WriteLine("Введите команду: Новый студент, Удалить, " + "Сортировать, Показать, Выход");

                    command = Console.ReadLine();

                    switch (command)
                    {
                        case "новый студент":

                            Console.WriteLine("Введите фамилию:");
                            string surname = Console.ReadLine();

                            Console.WriteLine("Введите имя:");
                            string name = Console.ReadLine();

                            Console.WriteLine("Введите год рождения:");
                            int year = int.Parse(Console.ReadLine());

                            Console.WriteLine("Введите экзамен:");
                            string exam = Console.ReadLine();

                            Console.WriteLine("Введите баллы:");
                            int score = int.Parse(Console.ReadLine());

                            Student newStudent = new Student(surname, name, year, exam, score);

                            students.Add(newStudent);

                            studentDictionary[StudentKey(newStudent)] = newStudent;

                            SaveStudents(students);

                            Console.WriteLine("Студент добавлен.");

                            break;


                        case "удалить":

                            Console.WriteLine("Введите фамилию:");
                            string deleteSurname = Console.ReadLine();

                            Console.WriteLine("Введите имя:");
                            string deleteName = Console.ReadLine();

                            bool deleted = false;

                            for (int i = 0; i < students.Count; i++)
                            {
                                if (students[i].Surname == deleteSurname && students[i].Name == deleteName)
                                {
                                    students.RemoveAt(i);
                                    deleted = true;
                                    break;
                                }
                            }

                            studentDictionary.Clear();

                            for (int i = 0; i < students.Count; i++)
                            {
                                studentDictionary[StudentKey(students[i])] = students[i];
                            }

                            SaveStudents(students);

                            if (deleted)
                            {
                                Console.WriteLine("Студент удалён.");
                            }
                            else
                            {
                                Console.WriteLine("Студент не найден.");
                            }

                            break;


                        case "сортировать":

                            for (int i = 0; i < students.Count - 1; i++)
                            {
                                for (int j = 0; j < students.Count - 1 - i; j++)
                                {
                                    if (students[j].Score > students[j + 1].Score)
                                    {
                                        Student temp = students[j];
                                        students[j] = students[j + 1];
                                        students[j + 1] = temp;
                                    }
                                }
                            }

                            SaveStudents(students);

                            Console.WriteLine("Студенты отсортированы.");
                            ShowStudents(students);

                            break;


                        case "показать":

                            ShowStudents(students);

                            break;


                        case "выход":

                            command = "Выход";

                            break;


                        default:

                            Console.WriteLine("Неизвестная команда.");

                            break;
                    }
                }

                break;



            case 3:

                Console.WriteLine("Задание 3");
                Console.WriteLine("Распределение бабуль по больницам");

                Stack<Hospital> hospitals = new Stack<Hospital>();

                hospitals.Push(new Hospital("Больница №1", new List<string> { "Грипп", "Диабет" }, 3));

                hospitals.Push(new Hospital("Больница №2", new List<string> { "Давление", "Артрит" }, 2));

                hospitals.Push(new Hospital("Больница №3", new List<string> { "Грипп", "Давление", "Артрит" }, 4));

                Queue<Grandma> queue = new Queue<Grandma>();
                List<Grandma> grandmas = new List<Grandma>();

                string action = "";

                while (action != "выход")
                {
                    Console.WriteLine("Введите команду: Добавить, Распределить, " + "Показать, Выход");

                    action = Console.ReadLine().ToLower();

                    switch (action)
                    {
                        case "добавить":

                            Grandma grandma = CreateGrandma();

                            queue.Enqueue(grandma);
                            grandmas.Add(grandma);

                            Console.WriteLine("Бабуля встала в очередь.");

                            break;


                        case "распределить":

                            while (queue.Count > 0)
                            {
                                Grandma current = queue.Dequeue();
                                Hospital chosenHospital = null;

                                foreach (Hospital hospital in hospitals)
                                {
                                    if (hospital.Patients.Count >= hospital.Capacity)
                                    {
                                        continue;
                                    }

                                    if (current.Diseases.Count == 0)
                                    {
                                        chosenHospital = hospital;
                                        break;
                                    }

                                    int matches = 0;

                                    foreach (string disease in current.Diseases.Keys)
                                    {
                                        if (hospital.Diseases.Contains(disease))
                                        {
                                            matches++;
                                        }
                                    }

                                    double percent = (double)matches / current.Diseases.Count * 100;

                                    if (percent > 50)
                                    {
                                        chosenHospital = hospital;
                                        break;
                                    }
                                }

                                if (chosenHospital != null)
                                {
                                    chosenHospital.Patients.Add(current);
                                    current.HospitalName = chosenHospital.Name;

                                    Console.WriteLine(current.Name + " попала в " + chosenHospital.Name);
                                }
                                else
                                {
                                    current.HospitalName = "На улице";

                                    Console.WriteLine(current.Name + " осталась на улице плакать.");
                                }
                            }

                            break;


                        case "показать":

                            ShowGrandmas(grandmas);
                            ShowHospitals(hospitals);

                            break;


                        case "выход":

                            action = "выход";

                            break;


                        default:

                            Console.WriteLine("Неизвестная команда.");

                            break;
                    }
                }

                break;



            case 4:

                Console.WriteLine("Задание 4");
                Console.WriteLine("Поиск кратчайшего пути в графе");


                List<int>[] graph = new List<int>[7];

                for (int i = 0; i < graph.Length; i++)
                {
                    graph[i] = new List<int>();
                }

                graph[0].Add(1);
                graph[0].Add(2);
                graph[1].Add(0);
                graph[1].Add(3);
                graph[1].Add(4);
                graph[2].Add(0);
                graph[2].Add(4);
                graph[3].Add(1);
                graph[3].Add(5);
                graph[4].Add(1);
                graph[4].Add(2);
                graph[4].Add(5);
                graph[5].Add(3);
                graph[5].Add(4);
                graph[5].Add(6);
                graph[6].Add(5);

                Console.WriteLine("Начальная вершина:");
                int start = int.Parse(Console.ReadLine());

                Console.WriteLine("Конечная вершина:");
                int finish = int.Parse(Console.ReadLine());

                if (start < 0 || start > 6 || finish < 0 || finish > 6)
                {
                    Console.WriteLine("Неправильный номер вершины.");
                    break;
                }

                Queue<int> vertices = new Queue<int>();
                bool[] visited = new bool[7];
                int[] previous = new int[7];

                for (int i = 0; i < previous.Length; i++)
                {
                    previous[i] = -1;
                }

                vertices.Enqueue(start);
                visited[start] = true;

                while (vertices.Count > 0)
                {
                    int current = vertices.Dequeue();

                    for (int i = 0; i < graph[current].Count; i++)
                    {
                        int next = graph[current][i];

                        if (!visited[next])
                        {
                            visited[next] = true;
                            previous[next] = current;
                            vertices.Enqueue(next);
                        }
                    }
                }

                if (!visited[finish])
                {
                    Console.WriteLine("Путь не найден.");
                }
                else
                {
                    List<int> path = new List<int>();

                    int vertex = finish;

                    while (vertex != -1)
                    {
                        path.Add(vertex);
                        vertex = previous[vertex];
                    }

                    path.Reverse();

                    Console.WriteLine("Кратчайший путь:");

                    for (int i = 0; i < path.Count; i++)
                    {
                        Console.Write(path[i]);

                        if (i < path.Count - 1)
                        {
                            Console.Write(" -> ");
                        }
                    }

                    Console.WriteLine();
                }

                break;


            default:

                Console.WriteLine("Неправильное число");

                break;
        }
    }
}