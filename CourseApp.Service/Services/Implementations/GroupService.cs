using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;
using CourseApp.Service.Exceptions;
using CourseApp.Service.Services.Helpers;
using CourseApp.Service.Services.Interfaces;

namespace CourseApp.Service.Services.Implementations;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public void Create(Group group)
    {
        try
        {
            Group exist = CourseDbContext.Groups.Find(G => G.Name.Trim().ToLower() == group.Name.Trim().ToLower());
            if (exist is not null)
            {
                throw new DuplicateException("Group already exist");
            }
            if (group is null)
                throw new NullException("Group cannot be null!");

            _groupRepository.Create(group);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete(int id)
    {
        try
        {
            Group group = CourseDbContext.Groups.Find(G => G.Id == id);
            if (group is null)
            {
                throw new NotFoundException("Group not found");
            }

            else
            {
                _groupRepository.Delete(id);
                CustomHelper.WriteLine(ConsoleColor.DarkGreen, "Group succesfully deleted");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public List<Group> GetAll()
    {
        List<Group> groups = _groupRepository.GetAll();
        try
        {
            if (groups.Count is 0)
                throw new NotFoundException("Groups not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return groups;
    }

    public List<Group> GetAllByRoom(int roomNumber)
    {
        List<Group> groups = _groupRepository.GetAll(G => G.RoomNumber == roomNumber);
        try
        {
            if (groups.Count is 0)
                throw new NotFoundException("Groups not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return groups;
    }

    public List<Group> GetAllByTeacher(string teacherName)
    {
        List<Group> groups = _groupRepository.GetAll(G => (G.TeacherName ?? string.Empty).ToLower().Trim().StartsWith(teacherName.ToLower().Trim()));
        try
        {
            if (groups.Count is 0)
                throw new NotFoundException("Groups not found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return groups;
    }

    public Group GetById(int groupId)
    {
        Group group = CourseDbContext.Groups.Find(G => G.Id == groupId);
        try
        {
            if (group is null)
                throw new NotFoundException("Group not found");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return group;
    }

    public void Update(int id, Group group)
    {
        Group exist = CourseDbContext.Groups.Find(G => G.Id == id);
        Group existName = CourseDbContext.Groups.Find(G => G.Name.Trim().ToLower() == group.Name.Trim().ToLower());

        try
        {
            if (exist is null)
                throw new NotFoundException("Group not found");

            if (existName is not null)
                throw new DuplicateException("Group already exist");

            _groupRepository.Update(id, group);
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
