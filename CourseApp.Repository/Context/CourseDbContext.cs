using CourseApp.Domain.Models;

namespace CourseApp.Repository.Context;

public static class CourseDbContext
{
    public static List<Student> Students { get; set; }
    public static List<Group> Groups { get; set; }

    static CourseDbContext()
    {
        Students = new List<Student>();
        Groups = new List<Group>();
    }
}
