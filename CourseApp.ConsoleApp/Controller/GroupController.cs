using CourseApp.Domain.Models;
using CourseApp.Repository.Repositories.Implementations;
using CourseApp.Repository.Repositories.Interfaces;
using CourseApp.Service.Exceptions;
using CourseApp.Service.Services.Helpers;
using CourseApp.Service.Services.Implementations;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CourseApp.ConsoleApp.Controller;

public class GroupController
{
    private GroupService _groupService { get; }
    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }
    public void Create()
    {
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$");
        Regex regex1 = new Regex(@"^[a-zA-Z0-9\s]+$");
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group name (or write 'Exit' to cancel): ");

    GroupName: string groupName = Console.ReadLine();
        if (groupName.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;
        if (string.IsNullOrWhiteSpace(groupName) || !regex1.IsMatch(groupName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of group name!");
            goto GroupName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's teacher (or write 'Exit' to cancel): ");
    TeacherName: string teacher = Console.ReadLine();
        if (teacher.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;
        Regex regexTeacher = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ\\s]+$", RegexOptions.IgnoreCase);
        if (string.IsNullOrWhiteSpace(teacher) || !regexTeacher.IsMatch(teacher))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of teacher name!");
            goto TeacherName;
        }

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's room (or write 'Exit' to cancel): ");
    Room: string room = Console.ReadLine();
        if (room.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;
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

        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's capacity (or write 'Exit' to cancel): ");
    Capacity: string capacity = Console.ReadLine();
        if (capacity.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;
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

        Domain.Models.Group group = new()
        {
            Name = groupName,
            TeacherName = teacher,
            RoomNumber = roomNum,
            Capacity = capacityNum
        };
        _groupService.Create(group);
        //CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Group succesfully created");

        //try
        //{
        //    _groupService.Create(group);
        //    CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Group successfully created!");
        //}
        //catch (Exception ex)
        //{
        //    CustomHelper.WriteLine(ConsoleColor.DarkRed, ex.Message);
        //}

    }

    public void Update()
    {
        CustomHelper.WriteLine(ConsoleColor.DarkCyan, "If you want to exit, write - Exit");

        Regex regexTeacher = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ\\s]+$", RegexOptions.IgnoreCase);
        Regex regexGroup = new Regex(@"^[a-zA-Z0-9\s]*$");

        
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group id: ");
    Id:
        string id = Console.ReadLine();
        if (id.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!int.TryParse(id, out int idNum) || idNum < 0)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid positive id!");
            goto Id;
        }

        var groupExist = _groupService.GetById(idNum);
        if (groupExist is null)
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Group not found!");
            goto Id;
        }

        
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group name (or leave empty to keep old): ");
    GroupName: string groupName = Console.ReadLine();
        if (groupName.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!string.IsNullOrWhiteSpace(groupName) && !regexGroup.IsMatch(groupName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid group name format!");
            goto GroupName;
        }

        
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's teacher (or leave empty to keep old): ");
    Teacher: string teacherName = Console.ReadLine();
        if (teacherName.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        if (!string.IsNullOrWhiteSpace(teacherName) && !regexTeacher.IsMatch(teacherName))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of teacher name!");
            goto Teacher;
        }

        
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's room (or leave empty to keep old): ");
    Room: string room = Console.ReadLine();
        if (room.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        int? roomNum = null;
        if (!string.IsNullOrWhiteSpace(room))
        {
            if (!int.TryParse(room, out int temp) || temp < 0)
            {
                CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid positive room number!");
                goto Room;
            }
            roomNum = temp;
        }

        
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter new group's capacity (or leave empty to keep old): ");
    Capacity: string capacity = Console.ReadLine();
        if (capacity.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            return;

        int? capacityNum = null;
        if (!string.IsNullOrWhiteSpace(capacity))
        {
            if (!int.TryParse(capacity, out int temp) || temp < 0)
            {
                CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid positive capacity!");
                goto Capacity;
            }
            capacityNum = temp;
        }

        
        Domain.Models.Group group = new()
        {
            Name = string.IsNullOrWhiteSpace(groupName) ? null : groupName,
            TeacherName = string.IsNullOrWhiteSpace(teacherName) ? null : teacherName,
            RoomNumber = roomNum ?? 0,
            Capacity = capacityNum ?? 0
        };

        try
        {
            _groupService.Update(idNum, group);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete()
    {
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group id: ");
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
        _groupService.Delete(idNum);

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
        if (result is not null)
            Console.WriteLine($"Id: {result.Id} Name: {result.Name} TeacherName: {result.TeacherName} Room: {result.RoomNumber} Capacity: {result.Capacity}");
    }

    public void GetByTeacherName()
    {
        
        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ ]+$", RegexOptions.IgnoreCase);
        CustomHelper.WriteLine(ConsoleColor.Cyan, "Enter group's teacher: ");
    TeacherName: string teacher = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(teacher) || !regex.IsMatch(teacher))
        {
            CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of teacher name!");
            goto TeacherName;
        }

        List<Domain.Models.Group> groups = _groupService.GetAllByTeacher(teacher.Trim().ToLower());

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

        List<Domain.Models.Group> groups = _groupService.GetAllByRoom(roomNum);

        foreach (var group in groups)
        {
            Console.WriteLine($"Id: {group.Id} Name: {group.Name} TeacherName: {group.TeacherName} Room: {group.RoomNumber} Capacity: {group.Capacity}");
        }

    }
}
