using System;
using System.Collections.Generic;

public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }
}

public class Person
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public Address HomeAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Person(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        BirthDate = birthDate;
        HomeAddress = homeAddress;
        PhoneNumber = phoneNumber;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Last Name: {LastName}");
        Console.WriteLine($"First Name: {FirstName}");
        Console.WriteLine($"Middle Name: {MiddleName}");
        Console.WriteLine($"Birth Date: {BirthDate.ToShortDateString()}");
        Console.WriteLine($"Home Address: {HomeAddress.City}, {HomeAddress.Street}, {HomeAddress.HouseNumber}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
    }
}

public class Student : Person, IComparable<Student>
{
    public List<int> Grades { get; } = new List<int>();
    public Student(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber)
    : base(lastName, firstName, middleName, birthDate, homeAddress, phoneNumber)
    {
    }

    public Student(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber, List<int> grades)
        : base(lastName, firstName, middleName, birthDate, homeAddress, phoneNumber)
    {
        Grades = grades;
    }

    public void AddGrade(int grade)
    {
        Grades.Add(grade);
    }

    public double CalculateAverageGrade()
    {
        double sum = 0;
        foreach (int grade in Grades)
        {
            sum += grade;
        }
        return sum / Grades.Count;
    }

    public int CompareTo(Student other)
    {
        double avgGrade = CalculateAverageGrade();
        double otherAvgGrade = other.CalculateAverageGrade();

        if (avgGrade > otherAvgGrade)
            return -1; // this student comes before the other student
        else if (avgGrade < otherAvgGrade)
            return 1; // this student comes after the other student
        else
            return 0; // the students have the same average grade
    }

    public new void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Grades: {string.Join(", ", Grades)}");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Address address = new Address
        {
            City = "Odesa",
            Street = "heroes of the defense of Odessa",
            HouseNumber = 22
        };
        Address address1 = new Address
        {
            City = "Odesa",
            Street = "Lysanovka",
            HouseNumber = 8
        };
        Address address2 = new Address
        {
            City = "Odesa",
            Street = "Moldovanka",
            HouseNumber = 221
        };
        Student student1 = new Student("Yakuskin", "Arkadiy", "D.", new DateTime(2006, 1, 1), address, "555-1234");
        student1.AddGrade(4);
        student1.AddGrade(5);
        student1.AddGrade(5);

        Student student2 = new Student("Potapov", "Valeriy", "V.", new DateTime(2006, 5, 10), address1, "555-5678");
        student2.AddGrade(5);
        student2.AddGrade(4);
        student2.AddGrade(3);

        Student student3 = new Student("Timchenko", "Stas", "O.", new DateTime(2005, 3, 20), address2, "555-2468");
        student3.AddGrade(3);
        student3.AddGrade(3);
        student3.AddGrade(3);

        List<Student> students = new List<Student>() { student1, student2, student3 };

        Console.WriteLine("Before sorting:");
        foreach (var student in students)
        {
            student.DisplayInfo();
            Console.WriteLine();
        }

        students.Sort();

        Console.WriteLine("After sorting:");
        foreach (var student in students)
        {
            student.DisplayInfo();
            Console.WriteLine();
        }
    }
}