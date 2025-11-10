using CourseApp.Domain.Models;
using CourseApp.Repository.Repositories.Implementations;
using CourseApp.Repository.Repositories.Interfaces;
using CourseApp.Service.Services.Helpers;
using CourseApp.Service.Services.Implementations;

namespace CourseApp.ConsoleApp.Controller;

public class GroupController
{
    private GroupService _groupService { get; set; }
    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }
    public void Create()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group name: ");
    GroupName: string groupName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(groupName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of group name!");
            goto GroupName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's teacher: ");
    TeacherName: string teacher = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(teacher))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of teacher name!");
            goto TeacherName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's room: ");
    Room: string room = Console.ReadLine();
        int roomNum;
        bool isConvert = int.TryParse(room, out roomNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of room number!");
            goto Room;
        }
        if (roomNum <= 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Room number must be greater than 0!");
            goto Room;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's capacity: ");
    Capacity: string capacity = Console.ReadLine();
        int capacityNum;
        bool isConvertCapacity = int.TryParse(capacity, out capacityNum);
        if (!isConvertCapacity)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of capacity!");
            goto Capacity;
        }
        if (capacityNum <= 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Capacity must be greater than 0!");
            goto Capacity;
        }

        Group group = new()
        {
            Name = groupName,
            TeacherName = teacher,
            RoomNumber = roomNum,
            Capacity = capacityNum
        };
        _groupService.Create(group);
        CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Group succesfully created");

    }

    public void Update()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group id: ");
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


        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group name: ");
        string groupName = Console.ReadLine();


        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's teacher: ");
        string teacherName = Console.ReadLine();

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's room: ");
    Room: string room = Console.ReadLine();
        int roomNum;
        bool isConvertRoom = int.TryParse(room, out roomNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of room number!");
            goto Room;
        }
        if (roomNum <= 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Room number must be greater than 0!");
            goto Room;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's capacity: ");
    Capacity: string capacity = Console.ReadLine();
        int capacityNum;
        bool isConvertCapacity = int.TryParse(capacity, out capacityNum);
        if (!isConvertCapacity)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of capacity!");
            goto Capacity;
        }
        if (capacityNum <= 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Capacity must be greater than 0!");
            goto Capacity;
        }

        Group group = new()
        {
            Name = groupName,
            TeacherName = teacherName,
            RoomNumber = roomNum,
            Capacity = capacityNum
        };

        _groupService.Update(idNum, group);
        CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Group succesfully updated");
    }

    public void Delete()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group id: ");
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

        _groupService.Delete(idNum);
        CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Group succesfully deleted");

    }

    public void GetAll()
    {
        var groups = _groupService.GetAll();
        foreach (var group in groups)
        {
            Console.WriteLine($"Id: {group.Id} Name: {group.Name} TeacherName: {group.TeacherName} Room: {group.RoomNumber} Capacity: {group.Capacity}");
        }

    }

    public void GetById()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group id: ");
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

        var result = _groupService.GetById(idNum);
        Console.WriteLine($"Id: {result.Id} Name: {result.Name} TeacherName: {result.TeacherName} Room: {result.RoomNumber} Capacity: {result.Capacity}");
    }

    public void GetByTeacherName()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's teacher: ");
    TeacherName: string teacher = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(teacher))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of teacher name!");
            goto TeacherName;
        }

        List<Group> groups = _groupService.GetAllByTeacher(teacher);

        foreach (var group in groups)
        {
            Console.WriteLine($"Id: {group.Id} Name: {group.Name} TeacherName: {group.TeacherName} Room: {group.RoomNumber} Capacity: {group.Capacity}");
        }
    }

    public void GetByRoomNumber()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group room: ");
    Room: string room = Console.ReadLine();
        int roomNum;
        bool isConvert = int.TryParse(room, out roomNum);
        if (!isConvert)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of room number!");
            goto Room;
        }
        if (roomNum <= 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Room number must be greater than 0!");
            goto Room;
        }

        List<Group> groups = _groupService.GetAllByRoom(roomNum);

        foreach (var group in groups)
        {
            Console.WriteLine($"Id: {group.Id} Name: {group.Name} TeacherName: {group.TeacherName} Room: {group.RoomNumber} Capacity: {group.Capacity}");
        }

    }
}
