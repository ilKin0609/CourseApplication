// See https://aka.ms/new-console-template for more information
using CourseApp.Domain.Models;
using CourseApp.Repository.Repositories.Implementations;
using CourseApp.Service.Services.Implementations;

Console.WriteLine("Hello, World!");

StudentRepository studentRepo = new();
StudentService studentService = new(studentRepo);
