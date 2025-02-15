﻿using System;

class Program
{
    static int courseCount = 0;
    static int studentCount = 0;
    const int MaxCourses = 100;
    const int MaxStudents = 1000;

    static int[] courseIds = new int[MaxCourses];
    static string[] courseNames = new string[MaxCourses];
    static int[] maxStudentsPerCourse = new int[MaxCourses];
    static int[,] enrolledStudents = new int[MaxCourses, MaxStudents]; // Массив для хранения ID студентов на курсах
    static int[] enrolledStudentCounts = new int[MaxCourses]; // Количество студентов на каждом курсе

    static int[] studentIds = new int[MaxStudents];
    static string[] studentNames = new string[MaxStudents];
    static int[] studentAges = new int[MaxStudents];

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Добавить курс");
            Console.WriteLine("2. Удалить курс");
            Console.WriteLine("3. Посмотреть список курсов");
            Console.WriteLine("4. Записать студента на курс");
            Console.WriteLine("5. Показать список студентов на курсе");
            Console.WriteLine("6. Удалить студента из курса");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите опцию: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddCourse();
                    break;
                case 2:
                    RemoveCourse();
                    break;
                case 3:
                    ShowCourses();
                    break;
                case 4:
                    EnrollStudent();
                    break;
                case 5:
                    ShowStudents();
                    break;
                case 6:
                    RemoveStudent();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void AddCourse()
    {
        if (courseCount >= MaxCourses)
        {
            Console.WriteLine("Достигнуто максимальное количество курсов.");
            return;
        }

        Console.Write("Введите ID курса: ");
        int courseId = int.Parse(Console.ReadLine());
        Console.Write("Введите название курса: ");
        string courseName = Console.ReadLine();
        Console.Write("Введите максимальное количество студентов: ");
        int maxStudents = int.Parse(Console.ReadLine());

        courseIds[courseCount] = courseId;
        courseNames[courseCount] = courseName;
        maxStudentsPerCourse[courseCount] = maxStudents;
        courseCount++;

        Console.WriteLine("Курс успешно добавлен.");
    }

    static void RemoveCourse()
    {
        Console.Write("Введите ID курса для удаления: ");
        int courseId = int.Parse(Console.ReadLine());

        int courseIndex = Array.IndexOf(courseIds, courseId);
        if (courseIndex != -1)
        {
            // Сдвигаем все курсы после удаляемого на одну позицию влево
            for (int i = courseIndex; i < courseCount - 1; i++)
            {
                courseIds[i] = courseIds[i + 1];
                courseNames[i] = courseNames[i + 1];
                maxStudentsPerCourse[i] = maxStudentsPerCourse[i + 1];
                enrolledStudentCounts[i] = enrolledStudentCounts[i + 1];

                // Копируем студентов с курса
                for (int j = 0; j < enrolledStudentCounts[i]; j++)
                {
                    enrolledStudents[i, j] = enrolledStudents[i + 1, j];
                }
            }

            courseCount--;
            Console.WriteLine("Курс успешно удален.");
        }
        else
        {
            Console.WriteLine("Курс не найден.");
        }
    }

    static void ShowCourses()
    {
        if (courseCount == 0)
        {
            Console.WriteLine("Нет доступных курсов.");
            return;
        }

        Console.WriteLine("Список курсов:");
        for (int i = 0; i < courseCount; i++)
        {
            Console.WriteLine($"ID: {courseIds[i]}, Название: {courseNames[i]}, Макс. студентов: {maxStudentsPerCourse[i]}, Записано студентов: {enrolledStudentCounts[i]}");
        }
    }

    static void EnrollStudent()
    {
        if (studentCount >= MaxStudents)
        {
            Console.WriteLine("Достигнуто максимальное количество студентов.");
            return;
        }

        Console.Write("Введите ID студента: ");
        int studentId = int.Parse(Console.ReadLine());
        Console.Write("Введите имя студента: ");
        string studentName = Console.ReadLine();
        Console.Write("Введите возраст студента: ");
        int studentAge = int.Parse(Console.ReadLine());

        studentIds[studentCount] = studentId;
        studentNames[studentCount] = studentName;
        studentAges[studentCount] = studentAge;
        studentCount++;

        Console.Write("Введите ID курса для записи: ");
        int courseId = int.Parse(Console.ReadLine());

        int courseIndex = Array.IndexOf(courseIds, courseId);
        if (courseIndex != -1)
        {
            if (enrolledStudentCounts[courseIndex] < maxStudentsPerCourse[courseIndex])
            {
                enrolledStudents[courseIndex, enrolledStudentCounts[courseIndex]] = studentId;
                enrolledStudentCounts[courseIndex]++;
                Console.WriteLine("Студент успешно записан на курс.");
            }
            else
            {
                Console.WriteLine("Курс переполнен.");
            }
        }
        else
        {
            Console.WriteLine("Курс не найден.");
        }
    }

    static void ShowStudents()
    {
        Console.Write("Введите ID курса: ");
        int courseId = int.Parse(Console.ReadLine());

        int courseIndex = Array.IndexOf(courseIds, courseId);
        if (courseIndex != -1)
        {
            Console.WriteLine($"Студенты на курсе {courseNames[courseIndex]}:");
            for (int i = 0; i < enrolledStudentCounts[courseIndex]; i++)
            {
                int studentId = enrolledStudents[courseIndex, i];
                int studentIndex = Array.IndexOf(studentIds, studentId);
                if (studentIndex != -1)
                {
                    Console.WriteLine($"ID: {studentIds[studentIndex]}, Имя: {studentNames[studentIndex]}, Возраст: {studentAges[studentIndex]}");
                }
            }
        }
        else
        {
            Console.WriteLine("Курс не найден.");
        }
    }

    static void RemoveStudent()
    {
        Console.Write("Введите ID курса: ");
        int courseId = int.Parse(Console.ReadLine());
        Console.Write("Введите ID студента: ");
        int studentId = int.Parse(Console.ReadLine());

        int courseIndex = Array.IndexOf(courseIds, courseId);
        if (courseIndex != -1)
        {
            int studentIndexInCourse = -1;
            for (int i = 0; i < enrolledStudentCounts[courseIndex]; i++)
            {
                if (enrolledStudents[courseIndex, i] == studentId)
                {
                    studentIndexInCourse = i;
                    break;
                }
            }

            if (studentIndexInCourse != -1)
            {
                for (int i = studentIndexInCourse; i < enrolledStudentCounts[courseIndex] - 1; i++)
                {
                    enrolledStudents[courseIndex, i] = enrolledStudents[courseIndex, i + 1];
                }
                enrolledStudentCounts[courseIndex]--;
                Console.WriteLine("Студент успешно удален из курса.");
            }
            else
            {
                Console.WriteLine("Студент не найден на курсе.");
            }
        }
        else
        {
            Console.WriteLine("Курс не найден.");
        }
    }
}