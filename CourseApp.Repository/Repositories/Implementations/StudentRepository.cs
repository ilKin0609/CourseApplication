using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;
using System;

namespace CourseApp.Repository.Repositories.Implementations;

public class StudentRepository : IStudentRepository
{
    public void Create(Student entity)
    {
        CourseDbContext.Students.Add(entity);
    }

    public void Delete(int id)
    {
        Student student = CourseDbContext.Students.Find(St=>St.Id == id);
        CourseDbContext.Students.Remove(student);
    }

    public List<Student> GetAll(Predicate<Student>? predicate = null)
    {
        List<Student> stuList = new List<Student>();
        if(predicate != null)
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
        Student student = CourseDbContext.Students.Find(St=>St.Id==id);
        student.Name = entity.Name;
        student.Surname = entity.Surname;
        student.Age = entity.Age;
        student.StuGroup = entity.StuGroup;
    }
}
