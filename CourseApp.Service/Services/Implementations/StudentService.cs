using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;
using CourseApp.Service.Exceptions;
using CourseApp.Service.Services.Interfaces;

namespace CourseApp.Service.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public void Create(Student student, string GroupName)
    {
        try
        {
            if (student is null)
                throw new NullException("Student cannot be null!");

            Group group = CourseDbContext.Groups.Find(G => G.Name.Trim().ToLower() == GroupName.Trim().ToLower());
            if (group is null)
                throw new NotFoundException("Group not found!");


            if (group.Capacity <= 0)
                throw new ValidTypeException("Group is full!");

            student.StuGroup = group;
            _studentRepository.Create(student);
            group.Capacity--;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public void Delete(int id)
    {
        try
        {
            Student student = CourseDbContext.Students.Find(St => St.Id == id);
            if (student is null)
                throw new NotFoundException("Student not found");

            _studentRepository.Delete(id);
            student.StuGroup.Capacity++;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public List<Student> GetAll()
    {
        List<Student> students = _studentRepository.GetAll();
        try
        {
            if (students.Count is 0)
                throw new NotFoundException("Students not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return students;
    }

    public Student GetStudentById(int id)
    {
        Student student = CourseDbContext.Students.Find(St => St.Id == id);
        try
        {
            if (student is null)
                throw new NotFoundException("Student not found");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return student;
    }

    public List<Student> GetStudentsByAge(int age)
    {
        List<Student> students = _studentRepository.GetAll(St => St.Age == age);
        try
        {
            if (students.Count is 0)
                throw new NotFoundException("Students not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return students;
    }

    public List<Student> GetStudentsByGroupId(int groupId)
    {
        List<Student> students = _studentRepository.GetAll(St => St.StuGroup.Id == groupId);
        try
        {
            if (students.Count is 0)
                throw new NotFoundException("Students not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return students;
    }

    public List<Student> GetStudentsByGroupName(string groupName)
    {
        List<Student> students = _studentRepository.GetAll(St => St.StuGroup.Name.ToLower().Trim().StartsWith(groupName.Trim().ToLower()));
        try
        {
            if (students.Count is 0)
                throw new NotFoundException("Students not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return students;
    }

    public List<Student> GetStudentsByNameOrSurname(string? name = null, string? surname =null)
    {
        List<Student> students = new List<Student>();
        try
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname))
            {
                students = _studentRepository.GetAll(St => St.Name.Trim().ToLower() == name.Trim()?.ToLower() &&
                St.Surname.Trim().ToLower() == surname.Trim()?.ToLower());


            }
            else if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(surname))
            {
                students = _studentRepository.GetAll(St => St.Name.ToLower().Trim().StartsWith(name.Trim().ToLower()));


            }
            else if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname))
            {
                students = _studentRepository.GetAll(St => St.Surname.ToLower().Trim().StartsWith(surname.Trim().ToLower()));
            }
            else
            {
                students = _studentRepository.GetAll();
            }

            if (students.Count is 0)
                throw new NotFoundException("Student not found");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return students;
    }

    public void Update(int id, Student student)
    {
        Student exist = CourseDbContext.Students.Find(St => St.Id == id);
        try
        {
            if (exist is null)
                throw new NotFoundException("Student not found");

            if (student is null)
                throw new NullException("Student cannot be null");

            _studentRepository.Update(id, student);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
