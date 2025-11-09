using CourseApp.Domain.Common;

namespace CourseApp.Domain.Models;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public string TeacherName { get; set; }
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }

    private static int _counter;
    public Group()
    {
        Id = _counter++;
    }
}
