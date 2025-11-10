using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;

namespace CourseApp.Repository.Repositories.Implementations;

public class GroupRepository : IGroupRepository
{
    public void Create(Group entity)
    {
        CourseDbContext.Groups.Add(entity);
    }

    public void Delete(int id)
    {
        Group group = CourseDbContext.Groups.Find(St => St.Id == id);
        CourseDbContext.Groups.Remove(group);
    }

    public List<Group> GetAll(Predicate<Group>? predicate = null)
    {
        List<Group> groups = new List<Group>();
        if (predicate != null)
        {
            groups = CourseDbContext.Groups.FindAll(predicate);
        }
        else
        {
            groups = CourseDbContext.Groups;
        }
        return groups;
    }

    public Group GetById(Predicate<Group>? predicate = null)
    {
        Group group = CourseDbContext.Groups.Find(predicate);
        return group;
    }

    public void Update(int id, Group entity)
    {
        Group group = CourseDbContext.Groups.Find(St => St.Id == id);
        int count = 0;
        if (!string.IsNullOrWhiteSpace(entity.Name))
        {
            group.Name = entity.Name;
            count++;
        }

        if (!string.IsNullOrWhiteSpace(entity.TeacherName))
        {
            group.TeacherName = entity.TeacherName;
            count++;
        }

        if (entity.RoomNumber != 0)
        {
            group.RoomNumber = entity.RoomNumber;
            count++;
        }

        if (entity.Capacity != 0)
        {
            group.Capacity = entity.Capacity;
            count++;
        }
        if(count > 0)
        {
            group.UpdatedAt = DateTime.Now;
            count = 0;
        }
    }
}
