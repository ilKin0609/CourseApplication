using CourseApp.Domain.Models;
using CourseApp.Repository.Context;
using CourseApp.Repository.Repositories.Interfaces;

namespace CourseApp.Repository.Repositories.Implementations;

public class GroupRepository:IGroupRepository
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
        group.Name = entity.Name;
        group.TeacherName = entity.TeacherName;
        group.RoomNumber = entity.RoomNumber;
        group.Capacity = entity.Capacity;
    }
}
