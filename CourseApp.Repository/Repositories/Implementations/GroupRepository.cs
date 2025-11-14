using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;

namespace CourseApp.Repository.Repositories.Implementations;

public class GroupRepository : IGroupRepository
{
    public void Create(Group entity)
    {
        CourseDbContext.Groups.Add(entity);
        entity.CreatedAt = DateTime.UtcNow;
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
        if (group is null) return;

        bool updated = false;

        if (!string.IsNullOrWhiteSpace(entity.Name))
        {
            group.Name = entity.Name;
            updated = true;
        }

        if (!string.IsNullOrWhiteSpace(entity.TeacherName))
        {
            group.TeacherName = entity.TeacherName;
            updated = true;
        }

        if (entity.RoomNumber != 0)
        {
            group.RoomNumber = entity.RoomNumber;
            updated = true;
        }

        if (entity.Capacity != 0)
        {
            group.Capacity = entity.Capacity;
            updated = true;
        }

        if (updated)
        {
            group.UpdatedAt = DateTime.Now;
        }
    }
}
