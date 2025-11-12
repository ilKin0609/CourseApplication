using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;

namespace CourseApp.Repository.Repositories.Implementations;

public class StudentRepository : IStudentRepository
{
    public void Create(Student entity)
    {
        CourseDbContext.Students.Add(entity);
    }

    public void Delete(int id)
    {
        Student student = CourseDbContext.Students.Find(St => St.Id == id);
        CourseDbContext.Students.Remove(student);
    }

    public List<Student> GetAll(Predicate<Student>? predicate = null)
    {
        List<Student> stuList = new List<Student>();
        if (predicate != null)
        {
            stuList = CourseDbContext.Students.FindAll(predicate);
        }
        else
        {
            stuList = CourseDbContext.Students;
        }
        return stuList;
    }

    public Student GetById(Predicate<Student>? predicate = null)
    {
        Student student = CourseDbContext.Students.Find(predicate);
        return student;
    }

    public void Update(int id, Student entity)
    {

        Student student = CourseDbContext.Students.Find(St => St.Id == id);
        int count = 0;
        if (!string.IsNullOrWhiteSpace(entity.Name))
        {
            student.Name = entity.Name;
            count++;
        }

        if (!string.IsNullOrWhiteSpace(entity.Surname))
        {
            student.Surname = entity.Surname;
            count++;
        }

        if (entity.Age != 0 && entity.Age > 5 && entity.Age < 55)
        {
            student.Age = entity.Age;
            count++;
        }


        if (!string.IsNullOrWhiteSpace(entity.StuGroup.Name))
        {
            Group group = CourseDbContext.Groups.Find(G => G.Name.Trim().ToLower() == entity.StuGroup.Name.Trim().ToLower());

            if (group is not null)
            {
                student.StuGroup = group;
                count++;
            }

        }

        if (count > 0)
        {
            student.UpdatedAt = DateTime.Now;
            count = 0;
        }
    }
}
