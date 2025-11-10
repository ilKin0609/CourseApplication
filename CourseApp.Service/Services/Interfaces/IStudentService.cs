using CourseApp.Domain.Models;

namespace CourseApp.Service.Services.Interfaces;

public interface IStudentService
{
    void Create(Student student,string GroupName);
    void Update(int id, Student student);
    void Delete(int id);

    Student GetStudentById(int id);
    List<Student> GetStudentsByAge(int age);
    List<Student> GetStudentsByGroupId(int groupId);
    List<Student> GetStudentsByGroupName(string groupName);
    List<Student> GetStudentsByNameOrSurname(string? name=null, string? surname = null);
    List<Student> GetAll();
}
