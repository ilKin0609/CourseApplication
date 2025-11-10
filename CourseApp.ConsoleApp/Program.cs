using CourseApp.ConsoleApp.Controller;
using CourseApp.Domain.Enums;
using CourseApp.Repository.Repositories.Implementations;
using CourseApp.Service.Services.Helpers;
using CourseApp.Service.Services.Implementations;

CustomHelper.WriteLine(ConsoleColor.Cyan, "Select option: ");

CustomHelper.WriteLine(ConsoleColor.DarkYellow, "Group methods: ");

CustomHelper.WriteLine(ConsoleColor.Cyan, "1-Create group");
CustomHelper.WriteLine(ConsoleColor.Cyan, "2-Update group");
CustomHelper.WriteLine(ConsoleColor.Cyan, "3-Delete group");
CustomHelper.WriteLine(ConsoleColor.Cyan, "4-GetAll groups");
CustomHelper.WriteLine(ConsoleColor.Cyan, "5-GetById group");
CustomHelper.WriteLine(ConsoleColor.Cyan, "6-GetByTeacherName groups");
CustomHelper.WriteLine(ConsoleColor.Cyan, "7-GetByRoomNumber groups");

CustomHelper.WriteLine(ConsoleColor.DarkYellow, "Student methods: ");

CustomHelper.WriteLine(ConsoleColor.Cyan, "8-Create student");
CustomHelper.WriteLine(ConsoleColor.Cyan, "9-Update student");
CustomHelper.WriteLine(ConsoleColor.Cyan, "10-Delete student");
CustomHelper.WriteLine(ConsoleColor.Cyan, "11-GetAll students");
CustomHelper.WriteLine(ConsoleColor.Cyan, "12-GetById student");
CustomHelper.WriteLine(ConsoleColor.Cyan, "13-GetByAge students");
CustomHelper.WriteLine(ConsoleColor.Cyan, "14-GetByGroupId students");
CustomHelper.WriteLine(ConsoleColor.Cyan, "15-GetByGroupName students");
CustomHelper.WriteLine(ConsoleColor.Cyan, "16-GetStudentNameOrSurname students");

GroupRepository groupRepository = new();
GroupService groupService = new(groupRepository);
GroupController groupController = new(groupService);

//StudentController studentController = new();

while (true)
{
Input: string input = Console.ReadLine();
    int number;

    bool isConvert = int.TryParse(input, out number);

    if (isConvert)
    {
        switch (number)
        {
            case (int)GroupMethods.Create:
                groupController.Create();
                break;

            case (int)GroupMethods.Update:
                groupController.Update();
                break;

            case (int)GroupMethods.Delete:
                groupController.Delete();
                break;

            case (int)GroupMethods.GetAll:
                groupController.GetAll();
                break;

            case (int)GroupMethods.GetById:
                groupController.GetById();
                break;

            case (int)GroupMethods.GetByTeacherName:
                groupController.GetByTeacherName();
                break;

            case (int)GroupMethods.GetByRoomNumber:
                groupController.GetByRoomNumber();
                break;
        }
    }
    else
        CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of selection!");
}


