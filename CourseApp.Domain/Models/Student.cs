using CourseApp.Domain.Common;

namespace CourseApp.Domain.Models;

public class Student : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public Group StuGroup { get; set; }

    private static int _counter;
    public Student()
    {
        Id = _counter++;
    }
}
