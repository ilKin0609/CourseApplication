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
        Regex regexGroup = new Regex(@"^[a-zA-Z0-9\s]+$");

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student name (or write 'Exit' to cancel): ");
    StuName:
        string studentName = Console.ReadLine();
        if (studentName.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (string.IsNullOrWhiteSpace(studentName) || !regex.IsMatch(studentName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student name!");
            goto StuName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student surname (or write 'Exit' to cancel): ");
    StuSurname:
        string surname = Console.ReadLine();
        if (surname.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (string.IsNullOrWhiteSpace(surname) || !regex.IsMatch(surname))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student surname!");
            goto StuSurname;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student age (or write 'Exit' to cancel): ");
    Age:
        string age = Console.ReadLine();
        if (age.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!int.TryParse(age, out int ageNum))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of age!");
            goto Age;
        }

        if (ageNum <= 5 || ageNum >= 55)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Age must be greater than 5 and less than 55!");
            goto Age;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group name (or write 'Exit' to cancel): ");
    GroupName:
        string group = Console.ReadLine();
        if (group.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (string.IsNullOrWhiteSpace(group) || !regexGroup.IsMatch(group))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of group name!");
            goto GroupName;
        }

        Student student = new()
        {
            Name = studentName.Trim(),
            Surname = surname.Trim(),
            Age = ageNum
        };

        try
        {
            _studentService.Create(student, group.Trim());
            //CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Student successfully created!");
        }
        catch (Exception ex)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, ex.Message);
        }
    }

    public void Update()
    {
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$", RegexOptions.IgnoreCase);
        Regex regexGroup = new Regex(@"^[a-zA-Z0-9\s]*$");

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student id (or write 'Exit' to cancel): ");
    Id:
        string id = Console.ReadLine();
        if (id.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!int.TryParse(id, out int idNum) || idNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of id!");
            goto Id;
        }


        var existStudent = _studentService.GetStudentById(idNum);
        if (existStudent is null)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Student not found!");
            goto Id;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new student name (or write 'Exit' to cancel): ");
    StuName:
        string studentName = Console.ReadLine();
        if (studentName.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!string.IsNullOrWhiteSpace(studentName) && !regex.IsMatch(studentName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student name!");
            goto StuName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new student surname (or write 'Exit' to cancel): ");
    StuSurname:
        string studentSurname = Console.ReadLine();
        if (studentSurname.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!string.IsNullOrWhiteSpace(studentSurname) && !regex.IsMatch(studentSurname))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student surname!");
            goto StuSurname;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new student age (or write 'Exit' to cancel): ");
    Age:
        string age = Console.ReadLine();
        if (age.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        int? ageNum = null;
        if (!string.IsNullOrWhiteSpace(age))
        {
            if (!int.TryParse(age, out int temp) || temp <= 5 || temp >= 55)
            {
                CustomHelper.WriteLine(ConsoleColor.DarkRed, "Age must be a number between 5 and 55!");
                goto Age;
            }
            ageNum = temp;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group name (or write 'Exit' to cancel): ");
    GroupName:
        string groupName = Console.ReadLine();
        if (groupName.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!string.IsNullOrWhiteSpace(groupName) && !regexGroup.IsMatch(groupName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid group name format!");
            goto GroupName;
        }

        Student student = new()
        {
            Name = string.IsNullOrWhiteSpace(studentName) ? null : studentName,
            Surname = string.IsNullOrWhiteSpace(studentSurname) ? null : studentSurname,
            Age = ageNum ?? 0,
            StuGroup = string.IsNullOrWhiteSpace(groupName) ? null : new Domain.Models.Group { Name = groupName }
        };

        try
        {
            _studentService.Update(idNum, student);
            //CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Student successfully updated!");n      
        }
        catch (Exception ex)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, ex.Message);
        }
    }

    public void Delete()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student id: ");
    Id: string id = Console.ReadLine();

        if (id.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

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

        try
        {
            _studentService.Delete(idNum);
            //CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Student successfully deleted!");
        }
        catch (Exception ex)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, ex.Message);
        }

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
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.StuGroup.Name}");
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

        List<Student> students = _studentService.GetStudentsByGroupName(groupName);

        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id} Name: {student.Name} Surname: {student.Surname} Age: {student.Age} Group: {student.StuGroup.Name}");
        }
    }

    public void GetStudentNameOrSurname()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student name: ");
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$");
    StuName: string studentName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(studentName) && !regex.IsMatch(studentName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of student name!");
            goto StuName;
        }
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter student surname: ");
    StuSurname: string studentSurname = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(studentSurname) && !regex.IsMatch(studentSurname))
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
