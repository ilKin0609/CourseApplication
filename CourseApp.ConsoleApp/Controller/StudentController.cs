using CourseApp.Domain.Models;
using CourseApp.Service.Services.Helpers;
using CourseApp.Service.Services.Implementations;
using CourseApp.Service.Services.Interfaces;
using System.Text.RegularExpressions;

namespace CourseApp.ConsoleApp.Controller;

public class StudentController
{
    private StudentService _studentService { get; }
    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    public void Create()
    {
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$");
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student name: ");
    StuName: string studentName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(studentName) || !regex.IsMatch(studentName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student name!");
            goto StuName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student surname: ");
    StuSurname: string surname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname) || !regex.IsMatch(surname))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student surname!");
            goto StuSurname;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student age: ");
    Age: string age = Console.ReadLine();
        int ageNum;
        bool isConvert = int.TryParse(age, out ageNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of age!");
            goto Age;
        }
        if (ageNum <= 5 || ageNum >= 55)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Age must be greater than 5 and less than 55!");
            goto Age;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group name: ");
    GroupName: string group = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(group))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of group name!");
            goto GroupName;
        }


        Student student = new()
        {
            Name = studentName,
            Surname = surname,
            Age = ageNum
        };

        _studentService.Create(student, group);
        CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Student succesfully created");

    }

    public void Update()
    {
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$");
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student id: ");
    Id: string id = Console.ReadLine();
        int idNum;
        bool isConvert = int.TryParse(id, out idNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of id!");
            goto Id;
        }
        if (idNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Id must be positive!");
            goto Id;
        }


        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new student name: ");
    StuName: string studentName = Console.ReadLine();
        if (!regex.IsMatch(studentName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student name!");
            goto StuName;
        }


        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new studet surname: ");
    StuSurname: string studentSurname = Console.ReadLine();
        if (!regex.IsMatch(studentSurname))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student surname!");
            goto StuSurname;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new student age: ");
    Age: string age = Console.ReadLine();
        int ageNum;
        bool isConvertRoom = int.TryParse(age, out ageNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of age!");
            goto Age;
        }
        if (ageNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Age must be positive!");
            goto Age;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's name: ");
        string groupName = Console.ReadLine();



        Student student = new()
        {
            Name = studentName,
            Surname = studentSurname,
            Age = ageNum,
            StuGroup = new Domain.Models.Group() { Name = groupName }
        };

        _studentService.Update(idNum, student);
        CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Student succesfully updated");
    }

    public void Delete()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student id: ");
    Id: string id = Console.ReadLine();
        int idNum;
        bool isConvert = int.TryParse(id, out idNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of id!");
            goto Id;
        }
        if (idNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Id must be positive!");
            goto Id;
        }

        _studentService.Delete(idNum);
        CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Student succesfully deleted");

    }

    public void GetAll()
    {
        var students = _studentService.GetAll();
        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.StuGroup.Name}");
        }

    }

    public void GetById()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student id: ");
    Id: string id = Console.ReadLine();
        int idNum;
        bool isConvert = int.TryParse(id, out idNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of id!");
            goto Id;
        }
        if (idNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Id must be positive!");
            goto Id;
        }

        var result = _studentService.GetStudentById(idNum);
        Console.WriteLine($"Id: {result.Id} Name: {result.Name} Surname: {result.Surname} Age: {result.Age} Group: {result.StuGroup.Name}");
    }

    public void GetByAge()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student age: ");
    Age: string age = Console.ReadLine();
        int ageNum;

        bool isConvert = int.TryParse(age, out ageNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of age!");
            goto Age;
        }

        List<Student> students = _studentService.GetStudentsByAge(ageNum);

        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.StuGroup.Name}");
        }
    }

    public void GetByGroupId()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group id: ");
    Id: string id = Console.ReadLine();
        int idNum;
        bool isConvert = int.TryParse(id, out idNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of group id!");
            goto Id;
        }
        if (idNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Id must be positive");
            goto Id;
        }

        List<Student> students = _studentService.GetStudentsByGroupId(idNum);

        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.Name}");
        }

    }

    public void GetByGroupName()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group name: ");
    GroupName: string groupName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(groupName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of group name!");
            goto GroupName;
        }

        List<Student> students = _studentService.GetStudentsByNameOrSurname(groupName);

        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.Name}");
        }
    }

    public void GetStudentNameOrSurname()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student name: ");
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$");
    StuName: string studentName = Console.ReadLine();
        if (!regex.IsMatch(studentName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student name!");
            goto StuName;
        }
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student surname: ");
    StuSurname: string studentSurname = Console.ReadLine();
        if (!regex.IsMatch(studentSurname))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student surname!");
            goto StuSurname;
        }
        List<Student> students = _studentService.GetStudentsByNameOrSurname(studentName, studentSurname);

        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.Name}");
        }
    }
}
