using CourseApp.ConsoleApp.Controller;
using CourseApp.Domain.Enums;
using CourseApp.Repository.Repositories.Implementations;
using CourseApp.Service.Services.Helpers;
using CourseApp.Service.Services.Implementations;
using System.Media;


GroupRepository groupRepository = new();
GroupService groupService = new(groupRepository);
GroupController groupController = new(groupService);


StudentRepository stuRepository = new();
StudentService stuService = new(stuRepository);
StudentController studentController = new(stuService);
//PlayBackgroundMusic();


while (true)
{

    CustomHelper.WriteLine(ConsoleColor.Cyan, "Select option: ");

    CustomHelper.WriteLine(ConsoleColor.DarkYellow, "Group and Student methods: ");

    CustomHelper.WriteLine(ConsoleColor.Cyan, "1-Create group                8-Create student");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "2-Update group                9-Update student");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "3-Delete group                10-Delete student");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "4-GetAll groups               11-GetAll students");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "5-GetById group               12-GetById student");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "6-GetByTeacherName groups     13-GetByAge students");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "7-GetByRoomNumber groups      14-GetByGroupId students");

    
    CustomHelper.WriteLine(ConsoleColor.Cyan, "                              15-GetByGroupName students");
    CustomHelper.WriteLine(ConsoleColor.Cyan, "                              16-GetStudentNameOrSurname students");

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

            case (int)StudentMethods.Create:
                studentController.Create();
                break;

            case (int)StudentMethods.Update:
                studentController.Update();
                break;

            case (int)StudentMethods.Delete:
                studentController.Delete();
                break;

            case (int)StudentMethods.GetAll:
                studentController.GetAll();
                break;

            case (int)StudentMethods.GetById:
                studentController.GetById();
                break;

            case (int)StudentMethods.GetByAge:
                studentController.GetByAge();
                break;

            case (int)StudentMethods.GetByGroupId:
                studentController.GetByGroupId();
                break;

            case (int)StudentMethods.GetByGroupName:
                studentController.GetByGroupName();
                break;

            case (int)StudentMethods.GetStudentNameOrSurname:
                studentController.GetStudentNameOrSurname();
                break;

            case 0:
                CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Exit");
                return;
            default:
                CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Please select between: (1-16)");
                break;
        }
    }
    else
    {
        CustomHelper.WriteLine(ConsoleColor.DarkRed, "Enter valid type of selection!");
        goto Input;
    }
}

void PlayBackgroundMusic()
{
    string fileName = "AmoungUs.wav";
    string musicPath = Path.Combine(AppContext.BaseDirectory, "Resources", fileName);

    if (!File.Exists(musicPath))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"⚠ Musiqi faylı tapılmadı: {musicPath}");
        Console.ResetColor();
        return;
    }

    SoundPlayer player = new SoundPlayer(musicPath);
    player.PlayLooping();
}


